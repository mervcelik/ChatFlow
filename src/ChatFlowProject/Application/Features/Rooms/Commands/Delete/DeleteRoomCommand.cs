using Application.Repositories;
using Application.Services.RoomAuthorizationService;
using Core.Authorization.Enums;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;

namespace Application.Features.Rooms.Commands.Delete;

public class DeleteRoomCommand : IRequest
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
}

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IRoomAuthorizationService _authorizationService;

    public DeleteRoomCommandHandler(
        IRoomRepository roomRepository,
        IRoomAuthorizationService authorizationService)
    {
        _roomRepository = roomRepository;
        _authorizationService = authorizationService;
    }

    public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        // Kullanıcının oda silme izni var mı kontrol et (sadece Owner)
        await _authorizationService.RequirePermissionAsync(request.UserId, request.RoomId, Permission.DeleteRoom);

        var room = await _roomRepository.GetAsync(x => x.Id == request.RoomId, includeDeleted: false, cancellationToken);

        if (room == null)
        {
            throw new NotFoundException("Room", request.RoomId);
        }

        // Soft delete: İçeriği silme yerine IsDeleted true yap ve DeletedAt zamanını kaydet
        await _roomRepository.SoftDeleteAsync(room, cancellationToken);
    }
}
