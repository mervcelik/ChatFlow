using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.Get;
using Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Application.Dtos;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<User, GetUserResponse>().ReverseMap();
        CreateMap<User, GetListUserResponse>().ReverseMap();
        CreateMap<Paginate<User>, GetListResponse<GetListUserResponse>>().ReverseMap();
        CreateMap<User, UpdatedUserResponse>().ReverseMap();

    }
}
