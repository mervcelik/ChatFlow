using Application.Features.Rooms.Commands.Create;
using Application.Features.Rooms.Commands.Update;
using Application.Features.Rooms.Queries.Get;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rooms.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Room, CreateRoomCommand>().ReverseMap();
        CreateMap<Room, CreatedRoomResponse>().ReverseMap();
        CreateMap<Room, GetRoomResponse>().ReverseMap();
        CreateMap<Room, UpdateRoomCommand>().ReverseMap();
        CreateMap<Room, UpdatedRoomResponse>().ReverseMap();
    }
}
