using Application.Features.Auth.BusinessRules;
using Application.Features.Users.Rules;
using Application.Services.RoomAuthorizationService;
using Core.Security.JWT;
using Core.Security.Services.Hash;
using Core.Security.Services.PasswordService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });


        services.AddTransient<AuthBusinessRules>();
        services.AddTransient<UserBusinessRules>();

        services.AddTransient<ITokenHelper, JwtHelper>();
        services.AddTransient<IPasswordService, BcryptPasswordService>();
        services.AddTransient<IHashService, HashService>();

        services.AddTransient<IRoomAuthorizationService, RoomAuthorizationManager>();
        services.AddTransient<RoomService>();
        return services;
    }
}
