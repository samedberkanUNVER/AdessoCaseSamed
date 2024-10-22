using Core.Application.Responses;

namespace Application.Features.Groups.Commands.Create;

public class CreatedDrawResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string MatchName { get; set; }
}