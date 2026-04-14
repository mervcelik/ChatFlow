using Application.Repositories;
using Application.Services.RoomAuthorizationService;
using Core.Authorization.Enums;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;

namespace Application.Features.Rooms.Commands.PermanentDelete;

public class PermanentDeleteRoomCommand : IRequest
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
}

public class PermanentDeleteRoomCommandHandler : IRequestHandler<PermanentDeleteRoomCommand>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IRoomAuthorizationService _authorizationService;

    public PermanentDeleteRoomCommandHandler(
        IRoomRepository roomRepository,
        IRoomAuthorizationService authorizationService)
    {
        _roomRepository = roomRepository;
        _authorizationService = authorizationService;
    }

    public async Task Handle(PermanentDeleteRoomCommand request, CancellationToken cancellationToken)
    {
        // Kullanıcının oda silme izni var mı kontrol et
        await _authorizationService.RequirePermissionAsync(request.UserId, request.RoomId, Permission.DeleteRoom);

        // Silinmiş veya aktif odayı getir (includeDeleted: true)
        var room = await _roomRepository.GetAsync(
            x => x.Id == request.RoomId, 
            includeDeleted: true, 
            cancellationToken);

        if (room == null)
        {
            throw new NotFoundException("Room", request.RoomId);
        }

        // Tamamen sil
        await _roomRepository.DeleteAsync(room, cancellationToken);
    }
}
