using Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(sp =>
 new MongoClient(configuration["MongoDbSettings:ConnectionString"]));

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
        });

        services.AddTransient<IMessageRepository,MessageRepository>();
        services.AddTransient<INotificationRepository,NotificationRepository>();
        services.AddTransient<IRoomMemberRepository,RoomMemberRepository>();
        services.AddTransient<IRoomRepository,RoomRepository>();
        services.AddTransient<IUserRepository,UserRepository>();
        services.AddTransient<IRefreshTokenRepository,RefreshTokenRepository>();

        return services;
    }
}
