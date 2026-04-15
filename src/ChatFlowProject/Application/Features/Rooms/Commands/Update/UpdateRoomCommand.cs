using Application.Repositories;
using Application.Services.RoomAuthorizationService;
using AutoMapper;
using Core.Authorization.Enums;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Rooms.Commands.Update;

public class UpdateRoomCommand : IRequest<UpdatedRoomResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }
}

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, UpdatedRoomResponse>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IRoomAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public UpdateRoomCommandHandler(
        IRoomRepository roomRepository,
        IRoomAuthorizationService authorizationService,
        IMapper mapper)
    {
        _roomRepository = roomRepository;
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task<UpdatedRoomResponse> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        await _authorizationService.RequirePermissionAsync(request.UserId, request.Id, Permission.UpdateRoomInfo);

        var room = await _roomRepository.GetAsync(x => x.Id == request.Id, false, cancellationToken);

        if (room == null)
        {
            throw new NotFoundException("Room", request.Id);
        }

        room = _mapper.Map(request,room);

        await _roomRepository.UpdateAsync(room);

        var response = _mapper.Map<UpdatedRoomResponse>(room);
        return response;
    }
}
