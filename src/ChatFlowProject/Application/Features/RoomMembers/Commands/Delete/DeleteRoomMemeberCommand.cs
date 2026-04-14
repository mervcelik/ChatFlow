using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoomMembers.Commands.Delete;

public class DeleteRoomMemeberCommand:IRequest
{
    public Guid Id { get; set; }
}
public class DeleteRoomMemeberCommandHandler : IRequestHandler<DeleteRoomMemeberCommand>
{
    private readonly IRoomMemberRepository _roomMemberRepository;

    public DeleteRoomMemeberCommandHandler(IRoomMemberRepository roomMemberRepository)
    {
        _roomMemberRepository = roomMemberRepository;
    }

    public async Task Handle(DeleteRoomMemeberCommand request, CancellationToken cancellationToken)
    {
        var entity =await _roomMemberRepository.GetAsync(x=>x.Id==request.Id);
        if (entity != null)
        {
            await _roomMemberRepository.DeleteAsync(entity);
        }
    }
}