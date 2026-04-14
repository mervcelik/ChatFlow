using Domain.Enums;

namespace Application.Features.Rooms.Queries.Get;

public class GetRoomResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }
    public RoomType Type { get; set; }
}
