using Application.Repositories;
using AutoMapper;
using Core.Security.Services.Hash;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RefreshTokens.Queries.Get;

public class GetHashedRefreshTokenQuery : IRequest<GetHashedRefreshTokenResponse>
{
    public string Token { get; set; }
}
public class GetHashedRefreshTokenQueryHandler : IRequestHandler<GetHashedRefreshTokenQuery, GetHashedRefreshTokenResponse>
{
    private readonly IHashService _hashService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;
    public GetHashedRefreshTokenQueryHandler(IHashService hashService, IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
    {
        _hashService = hashService;
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
    }

    public async Task<GetHashedRefreshTokenResponse> Handle(GetHashedRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var hashedToken = _hashService.Hash(request.Token);

        var result = await _refreshTokenRepository.GetAsync(x => x.Token == hashedToken);

        var dto= _mapper.Map<GetHashedRefreshTokenResponse>(result);
        return dto;
    }
}