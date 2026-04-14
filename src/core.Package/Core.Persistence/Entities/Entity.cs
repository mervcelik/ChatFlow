using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Persistence.Entities;

public class Entity : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }

    /// Soft Delete: Entity silinmiş mi
    public bool IsDeleted { get; set; } = false;

    /// Entity silindiği zaman (Soft Delete için)
    public DateTime? DeletedAt { get; set; }
}
