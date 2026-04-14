using Domain.Enums;

namespace Application.Features.Rooms.Commands.Update;

public class UpdatedRoomResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }
    public RoomType Type { get; set; }
}
