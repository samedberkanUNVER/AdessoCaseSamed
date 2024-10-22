using Application.Features.Draws.Commands.Create;
using Application.Features.Draws.Rules;
using Application.Features.Groups.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Draws;

public class DrawsManager : IDrawsService
{
    private readonly IDrawRepository _drawRepository;
    private readonly DrawBusinessRules _drawBusinessRules;
    private readonly IMapper _mapper;
    private readonly GroupBusinessRules _groupBusinessRules;
    private readonly IGroupRepository _groupRepository;
    private readonly IGroupTeamRepository _groupTeamRepository;
    private readonly ITeamRepository _teamRepository;

    public DrawsManager(IDrawRepository drawRepository, DrawBusinessRules drawBusinessRules, IMapper mapper, GroupBusinessRules groupBusinessRules, IGroupRepository groupRepository, IGroupTeamRepository groupTeamRepository, ITeamRepository teamRepository)
    {
        _drawRepository = drawRepository;
        _drawBusinessRules = drawBusinessRules;
        _mapper = mapper;
        _groupBusinessRules = groupBusinessRules;
        _groupRepository = groupRepository;
        _groupTeamRepository = groupTeamRepository;
        _teamRepository = teamRepository;
    }

    public async Task<Draw?> GetAsync(
        Expression<Func<Draw, bool>> predicate,
        Func<IQueryable<Draw>, IIncludableQueryable<Draw, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Draw? draw = await _drawRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return draw;
    }

    public async Task<IPaginate<Draw>?> GetListAsync(
        Expression<Func<Draw, bool>>? predicate = null,
        Func<IQueryable<Draw>, IOrderedQueryable<Draw>>? orderBy = null,
        Func<IQueryable<Draw>, IIncludableQueryable<Draw, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Draw> drawList = await _drawRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return drawList;
    }

    public async Task<Draw> AddAsync(Draw draw)
    {
        Draw addedDraw = await _drawRepository.AddAsync(draw);

        return addedDraw;
    }

    public async Task<Draw> UpdateAsync(Draw draw)
    {
        Draw updatedDraw = await _drawRepository.UpdateAsync(draw);

        return updatedDraw;
    }

    public async Task<Draw> DeleteAsync(Draw draw, bool permanent = false)
    {
        Draw deletedDraw = await _drawRepository.DeleteAsync(draw);

        return deletedDraw;
    }

    public async Task<CreatedRealDrawResponse> CreateRealDraw(CreateRealDrawCommand request, CancellationToken cancellationToken)
    {
        await _groupBusinessRules.NumberOfGroupsMustFourOrEight(request.GroupCount);

        var groupResults = await _groupRepository.GetListAsync(size: request.GroupCount);
        Random random = new Random();

        var teamsResult = await _teamRepository.GetListAsync(size: 32);
        var shuffledTeams = teamsResult.Items.OrderBy(t => random.Next()).ToList();


        Draw draw = _mapper.Map<Draw>(request);

        await _drawRepository.AddAsync(draw);

        var groupTeamList = new List<GroupTeam>();
        var index = 0;

        var groups = new List<List<Team>>(request.GroupCount);

        for (int i = 0; i < request.GroupCount; i++)
        {
            groups.Add(new List<Team>());
        }

        int teamsPerGroup = shuffledTeams.Count / request.GroupCount;
        int remainingTeams = shuffledTeams.Count % request.GroupCount;

        foreach (var team in shuffledTeams)
        {
            bool teamAdded = false;

            foreach (var group in groups.OrderBy(g => g.Count))
            {
                if (!group.Any(t => t.CountryId == team.CountryId) && group.Count < teamsPerGroup + (remainingTeams > 0 ? 1 : 0))
                {
                    group.Add(team);
                    teamAdded = true;

                    if (group.Count == teamsPerGroup + 1)
                    {
                        remainingTeams--;
                    }
                    break;
                }
            }

            if (!teamAdded)
            {
                var fallbackGroup = groups.OrderBy(g => g.Count).First();
                fallbackGroup.Add(team);

                if (fallbackGroup.Count == teamsPerGroup + 1)
                {
                    remainingTeams--;
                }
            }
        }

        for (int i = 0; i < groups.Count; i++)
        {
            Console.WriteLine($"Group {i + 1}:");
            foreach (var team in groups[i])
            {
                Console.WriteLine($"Team {team.Name} - CountryId {team.CountryId}");
                groupTeamList.Add(new GroupTeam()
                {
                    TeamId = team.Id,
                    GroupId = i + 1,
                    DrawId = draw.Id
                });
            }
        }

        await _groupTeamRepository.AddRangeAsync(groupTeamList);

        var response = groupTeamList
         .GroupBy(t => t.GroupId)
         .Select(g => new GroupResponse()
         {
             GroupName = groupResults.Items.FirstOrDefault(t => t.Id == g.Key)?.Name,
             Teams = g.Select(t => new TeamResponse
             {
                 Name = teamsResult.Items.FirstOrDefault(s => s.Id == t.TeamId)?.Name
             }).ToList()
         }).ToList();

        return new CreatedRealDrawResponse() { Group = response };
    }
}
