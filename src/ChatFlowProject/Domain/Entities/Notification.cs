using Core.Persistence.Attributes;
using Core.Persistence.Entities;
using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[BsonCollection("Notifications")]
public class Notification : Entity
{
    /// Bildirimi alacak kullanıcı.
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid UserId { get; set; }
    [BsonIgnore]
    public User User { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid RoomId { get; set; }
    [BsonIgnore]
    public Room Room { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid? MessageId { get; set; }

    /// Bildirimi tetikleyen kullanıcı.
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid TriggeredBy { get; set; }
    [BsonIgnore]
    public User TriggeredUser { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public NotificationType Type { get; set; }

    public bool IsRead { get; set; } = false;

    public DateTime? ReadAt { get; set; }
}