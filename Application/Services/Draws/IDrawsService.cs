using Application.Features.Draws.Commands.Create;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Draws;

public interface IDrawsService : IServiceRepositoryBase<Draw>
{
    Task<CreatedRealDrawResponse> CreateRealDraw(CreateRealDrawCommand request, CancellationToken cancellationToken);
}
