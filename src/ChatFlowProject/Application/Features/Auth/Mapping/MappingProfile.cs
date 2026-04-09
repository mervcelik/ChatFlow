using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Refresh;
using Application.Features.Auth.Commands.Register;
using AutoMapper;
using Core.Security.JWT;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterCommand, RegisterResponse>().ReverseMap();
        CreateMap<RegisterCommand, User>().ReverseMap();
        CreateMap<User, RegisterResponse>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<TokenDto, LoginResponse>().ReverseMap();
        CreateMap<TokenDto, RefreshResponse>().ReverseMap();
        
    }
}
