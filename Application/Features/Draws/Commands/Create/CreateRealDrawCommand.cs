using Application.Features.Draws.Rules;
using Application.Features.Groups.Rules;
using Application.Services.Draws;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Draws.Commands.Create;

public class CreateRealDrawCommand : IRequest<CreatedRealDrawResponse>
{
    public string DrawName { get; set; }
    public int PickerId { get; set; }
    public int GroupCount { get; set; }

    public class CreateRealDrawCommandHandler : IRequestHandler<CreateRealDrawCommand, CreatedRealDrawResponse>
    {       
        private readonly IDrawsService _drawsService;

        public CreateRealDrawCommandHandler(IDrawsService drawsService)
        {          
            _drawsService = drawsService;
        }

        public async Task<CreatedRealDrawResponse> Handle(CreateRealDrawCommand request, CancellationToken cancellationToken)
        {            
            return await _drawsService.CreateRealDraw(request, cancellationToken);
        }
    }
}
