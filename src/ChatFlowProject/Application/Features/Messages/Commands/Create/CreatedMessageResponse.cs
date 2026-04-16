using Domain.Enums;

namespace Application.Features.Messages.Commands.Create;

public class CreatedMessageResponse
{
    public Guid Id { get; set; }

    public Guid RoomId { get; set; }

    public Guid SenderId { get; set; }

    public string Content { get; set; } = string.Empty;

    public MessageType Type { get; set; }

    public Guid? ReplyToId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? EditedAt { get; set; }
}
