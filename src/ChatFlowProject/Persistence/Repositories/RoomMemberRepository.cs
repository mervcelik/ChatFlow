using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using MongoDB.Driver;

namespace Persistence.Repositories;

public class RoomMemberRepository:BaseRepository<RoomMember>,IRoomMemberRepository
{
    public RoomMemberRepository(IMongoDatabase database) : base(database)
    {
        
    }
}
