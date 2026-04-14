namespace Application.Features.Rooms.Queries.GetList;

public class GetListRoomResponse
{
    public Guid RoomId { get; set; }
    public string RoomName { get; set; }

    public DateTime? LastReadAt { get; set; }

    public int UnreadMessageCount { get; set; }
}