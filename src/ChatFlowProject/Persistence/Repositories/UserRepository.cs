using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using MongoDB.Driver;

namespace Persistence.Repositories;

public class UserRepository:BaseRepository<User>,IUserRepository
{
    public UserRepository(IMongoDatabase database) : base(database)
    {
        
    }
}
