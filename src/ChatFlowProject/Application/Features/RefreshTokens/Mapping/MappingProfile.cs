using Application.Features.RefreshTokens.Queries.Get;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RefreshTokens.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.RefreshToken, GetHashedRefreshTokenResponse>().ReverseMap();
    }
}
