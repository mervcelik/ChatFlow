using Core.Persistence.Attributes;
using Core.Persistence.Entities;
using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities;

[BsonCollection("Users")]
public class User:Entity
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    [BsonRepresentation(BsonType.String)]
    public UserStatus Status { get; set; } = UserStatus.Offline;
    
}
