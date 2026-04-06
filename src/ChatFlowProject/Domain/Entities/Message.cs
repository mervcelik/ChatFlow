using Core.Persistence.Attributes;
using Core.Persistence.Entities;
using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

[BsonCollection("Messages")]
public class Message : Entity
{
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid RoomId { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid SenderId { get; set; }

    public string Content { get; set; } = string.Empty;

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public MessageType Type { get; set; } = MessageType.Text;

    /// Mesajı okuyan kullanıcılar ve okuma zamanları.
    public List<ReadReceipt> ReadBy { get; set; } = new();

    /// Yanıt verilen mesajın ID'si. Null ise bu mesaj bir reply değildir.
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid? ReplyToId { get; set; }

    /// Dosya veya resim mesajlarında ek bilgiler.
    public MessageAttachment? Attachment { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? EditedAt { get; set; }
}
public class ReadReceipt
{
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid UserId { get; set; }

    public DateTime ReadAt { get; set; } = DateTime.UtcNow;
}

public class MessageAttachment
{
    public string Url { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public long FileSizeBytes { get; set; }

    public string MimeType { get; set; } = string.Empty;
}