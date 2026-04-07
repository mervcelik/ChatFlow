using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using MongoDB.Driver;

namespace Persistence.Repositories;

public class NotificationRepository:BaseRepository<Notification>,INotificationRepository
{
    public NotificationRepository(IMongoDatabase database) : base(database)
    {
        
    }
}
