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

[BsonCollection("Rooms")]
public class Room:Entity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? AvatarUrl { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public RoomType Type { get; set; } = RoomType.Group;
}
