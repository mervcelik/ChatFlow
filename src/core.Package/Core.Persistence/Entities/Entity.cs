using MongoDB.Bson.Serialization.Attributes;

namespace Core.Persistence.Entities;

public class Entity : IEntity
{
    [BsonId]                                                  
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}