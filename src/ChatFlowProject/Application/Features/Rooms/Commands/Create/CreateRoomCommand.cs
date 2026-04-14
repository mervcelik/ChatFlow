using Application.Repositories;
using AutoMapper;
using Core.Authorization.Enums;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rooms.Commands.Create;

public class CreateRoomCommand : IRequest<CreatedRoomResponse>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? AvatarUrl { get; set; }
    public RoomType Type { get; set; } = RoomType.Group;

    public Guid CreateUserId { get; set; }
    public List<Guid> UserIds { get; set; }
}
public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, CreatedRoomResponse>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IRoomMemberRepository _memberRepository;
    private readonly IMapper _mapper;
    public CreateRoomCommandHandler(IRoomRepository roomRepository, IMapper mapper, IRoomMemberRepository memberRepository)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
        _memberRepository = memberRepository;
    }
    public async Task<CreatedRoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = _mapper.Map<Room>(request);
        await _roomRepository.AddAsync(room);

        var createMember = new RoomMember
        {
            RoomId = room.Id,
            UserId = request.CreateUserId,
            Role = MemberRole.Admin,
            LastReadAt = DateTime.Now
        };
        await _memberRepository.AddAsync(createMember);

       foreach (var memberId in request.UserIds)
        {
            var member = new RoomMember
            {
                RoomId = room.Id,
                UserId = memberId,
                Role = request.Type == RoomType.Direct ? MemberRole.Admin : MemberRole.Member,
                LastReadAt = DateTime.Now
            };

            await _memberRepository.AddAsync(member);
        }
        return _mapper.Map<CreatedRoomResponse>(room);
    }
}