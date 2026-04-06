using Core.Persistence.Attributes;
using Core.Persistence.Entities;
using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

[BsonCollection("RoomMembers")]
public class RoomMember : Entity
{
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid RoomId { get; set; }
    [BsonIgnore]
    public Room Room { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid UserId { get; set; }
    [BsonIgnore]
    public User User { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public MemberRole Role { get; set; } = MemberRole.Member;

    /// Kullanıcının bu odada en son okuduğu mesajın zamanı.
    /// Okunmamış mesaj sayısını hesaplamak için kullanılır.
    public DateTime? LastReadAt { get; set; }
}