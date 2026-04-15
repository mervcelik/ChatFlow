using Application.Repositories;
using Core.Persistence.Attributes;
using Core.Persistence.Repositories;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections;
using System.Reflection;

namespace Persistence.Repositories;

public class RoomRepository:BaseRepository<Room>,IRoomRepository
{
    protected readonly IMongoCollection<RoomMember> _roomMemberCollection;
    protected readonly IMongoCollection<Message> _messageCollection;
    public RoomRepository(IMongoDatabase database) : base(database)
    {
        var roomMemberCollectionName = typeof(RoomMember)
            .GetCustomAttribute<BsonCollectionAttribute>()
            ?.CollectionName ?? typeof(RoomMember).Name.ToLower();

        _roomMemberCollection= database.GetCollection<RoomMember>(roomMemberCollectionName);

        var messageCollectionName = typeof(Message  )
            .GetCustomAttribute<BsonCollectionAttribute>()
            ?.CollectionName ?? typeof(Message).Name.ToLower();

        _messageCollection= database.GetCollection<Message>(messageCollectionName);

    }

    public async Task<List<RoomWithUnreadDto>> GetListRoomWithUnreadAsync(Guid userId)
    {
        var pipeline = new[]
        {
        // 1. Kullanıcının üye olduğu odaları bul
        new BsonDocument("$match", new BsonDocument("userId", userId.ToString())),

        // 2. Room bilgisini join et
        new BsonDocument("$lookup", new BsonDocument
        {
            { "from", "rooms" },
            { "localField", "roomId" },
            { "foreignField", "_id" },
            { "as", "room" }
        }),
        new BsonDocument("$unwind", "$room"),

        // 3. Okunmamış mesajları say (lastRead'den sonrakiler)
        new BsonDocument("$lookup", new BsonDocument
        {
            { "from", "messages" },
            { "let", new BsonDocument
                {
                    { "roomId", "$roomId" },
                    { "lastRead", "$lastRead" }
                }
            },
            { "pipeline", new BsonArray
                {
                    new BsonDocument("$match", new BsonDocument("$expr", new BsonDocument("$and", new BsonArray
                    {
                        new BsonDocument("$eq", new BsonArray { "$roomId", "$$roomId" }),
                        new BsonDocument("$gt", new BsonArray { "$createdAt", "$$lastRead" })
                    })))
                }
            },
            { "as", "unreadMessages" }
        }),

        // 4. Sonucu şekillendir
        new BsonDocument("$project", new BsonDocument
        {
            { "roomId", 1 },
            { "roomName", "$room.name" },
            { "lastReadAt", "$lastRead" },
            { "unreadMessageCount", new BsonDocument("$size", "$unreadMessages") }
        })
    };

        var result = await _roomMemberCollection
            .Aggregate<BsonDocument>(pipeline)
            .ToListAsync();

        return result.Select(doc => new RoomWithUnreadDto
        {
            RoomId = doc["roomId"].AsGuid,
            RoomName = doc["roomName"].AsString,
            LastReadAt = doc["lastReadAt"] != BsonNull.Value
                ? doc["lastReadAt"].ToUniversalTime()
                : null,
            UnreadMessageCount = doc["unreadMessageCount"].AsInt32
        }).ToList();
    }
}
[BsonIgnoreExtraElements]
public class RoomLookupModel
{
    [BsonId]
    public Guid Id { get; set; }  // RoomMember'ın kendi _id'si

    public Guid UserId { get; set; }
    public Guid RoomId { get; set; }
    public DateTime? LastReadAt { get; set; }

    public Room Room { get; set; }
    public List<Message> Messages { get; set; }
}