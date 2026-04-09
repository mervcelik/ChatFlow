using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using MongoDB.Driver;

namespace Persistence.Repositories;

public class RefreshTokenRepository:BaseRepository<RefreshToken>,IRefreshTokenRepository
{
    public RefreshTokenRepository(IMongoDatabase database) : base(database)
    {
        
    }
}
