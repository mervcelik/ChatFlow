using Domain.Enums;

namespace Application.Features.Messages.Queries.GetListByRoom;

public class GetListMessageResponse
{
    public Guid Id { get; set; }

    public Guid RoomId { get; set; }

    public Guid SenderId { get; set; }

    public string Content { get; set; } = string.Empty;

    public MessageType Type { get; set; }

    public Guid? ReplyToId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? EditedAt { get; set; }

    public int ReadByCount { get; set; }

    public List<ReadReceiptDto> ReadBy { get; set; } = new();
}

public class ReadReceiptDto
{
    public Guid UserId { get; set; }

    public DateTime ReadAt { get; set; }
}
