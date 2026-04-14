using Application.Repositories;
using Application.Services.RoomAuthorizationService;
using AutoMapper;
using Core.Authorization.Enums;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rooms.Queries.Get;

public class GetRoomQuery : IRequest<GetRoomResponse>
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
}

public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, GetRoomResponse>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IRoomAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public GetRoomQueryHandler(
        IRoomRepository roomRepository,
        IRoomAuthorizationService authorizationService,
        IMapper mapper)
    {
        _roomRepository = roomRepository;
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task<GetRoomResponse> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        // Kullanıcının odayı görüntüleme izni var mı kontrol et
        await _authorizationService.RequirePermissionAsync(request.UserId, request.RoomId, Permission.ViewRoom);

        var room = await _roomRepository.GetAsync(
            x => x.Id == request.RoomId, 
            includeDeleted: false, 
            cancellationToken);

        if (room == null)
        {
            throw new NotFoundException("Room", request.RoomId);
        }

        var response = _mapper.Map<GetRoomResponse>(room);
        return response;
    }
}
