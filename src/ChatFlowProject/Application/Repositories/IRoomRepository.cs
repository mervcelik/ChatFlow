using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Repositories;

public interface IRoomRepository: IRepository<Room>
{
    Task<List<RoomWithUnreadDto>> GetListRoomWithUnreadAsync(Guid userId);
}
public class RoomWithUnreadDto
{
    public Guid RoomId { get; set; }
    public string RoomName { get; set; }

    public DateTime? LastReadAt { get; set; }

    public int UnreadMessageCount { get; set; }
}
