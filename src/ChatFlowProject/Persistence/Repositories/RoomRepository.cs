using Application.Repositories;
using Core.Persistence.Attributes;
using Core.Persistence.Repositories;
using Domain.Entities;
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
        var result = await _roomMemberCollection.Aggregate()
            .Match(roomMember => roomMember.UserId == userId)
            .Lookup<RoomMember, Room, RoomLookupModel>(
            _collection,
            roomMember => roomMember.RoomId,
            room => room.Id,
            roomLookup => roomLookup.Room)
            .Lookup<RoomLookupModel, Message, RoomLookupModel>(
            _messageCollection,
                roomLookup => roomLookup.Room.Id,
                message => message.RoomId,
                roomLookup => roomLookup.Messages)

           .ToListAsync();

        var list = from item in result
                   select new RoomWithUnreadDto
                   {
                       RoomId = item.Room.Id,
                       RoomName = item.Room.Name,
                       LastReadAt = item.LastReadAt,
                       UnreadMessageCount = item.Messages.Count(m => m.CreatedDate > (item.LastReadAt ?? DateTime.MinValue))
                   };
        return list.ToList();
    }
}
public class RoomLookupModel
{
    public Room Room { get; set; }
    public List<Message> Messages { get; set; }
    public DateTime? LastReadAt { get; set; }
}
