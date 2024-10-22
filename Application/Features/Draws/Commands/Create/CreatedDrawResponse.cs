using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Draws.Commands.Create;

public class CreatedDrawResponse : IResponse
{
    public int Id { get; set; }
    public string DrawName { get; set; }
    public int PickerId { get; set; }
    public int GroupCount { get; set; }
}
