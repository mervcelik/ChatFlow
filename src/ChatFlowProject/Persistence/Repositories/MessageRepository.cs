using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

public class MessageRepository:BaseRepository<Message>,IMessageRepository
{
    public MessageRepository(IMongoDatabase database) : base(database)
    {
        
    }
}
