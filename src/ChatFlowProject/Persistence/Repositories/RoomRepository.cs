using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using MongoDB.Driver;

namespace Persistence.Repositories;

public class RoomRepository:BaseRepository<Room>,IRoomRepository
{
    public RoomRepository(IMongoDatabase database) : base(database)
    {
        
    }
}
