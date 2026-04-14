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
    public Guid RoomId { get; set; }
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
        // Kullanıcının oda güncelleme izni var mı kontrol et
        await _authorizationService.RequirePermissionAsync(request.UserId, request.RoomId, Permission.UpdateRoomInfo);

        var room = await _roomRepository.GetAsync(x => x.Id == request.RoomId, false, cancellationToken);

        if (room == null)
        {
            throw new NotFoundException("Room", request.RoomId);
        }

        room.Name = request.Name;
        room.Description = request.Description;
        room.AvatarUrl = request.AvatarUrl;

        await _roomRepository.UpdateAsync(room);

        var response = _mapper.Map<UpdatedRoomResponse>(room);
        return response;
    }
}
