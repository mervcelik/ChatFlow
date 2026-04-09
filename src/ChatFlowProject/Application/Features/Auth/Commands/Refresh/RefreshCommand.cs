using Application.Features.RefreshTokens.Commands;
using Application.Features.RefreshTokens.Commands.Create;
using Application.Features.RefreshTokens.Queries.Get;
using Application.Repositories;
using AutoMapper;
using Core.Security.JWT;
using Core.Security.Services.Hash;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Refresh;

public class RefreshCommand : IRequest<RefreshResponse>
{
    public string RefreshToken { get; set; }=string.Empty;
}
public class RefreshCommandHandler : IRequestHandler<RefreshCommand, RefreshResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenHelper _tokenhelper;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
   private readonly IHashService _hashService;
    public RefreshCommandHandler(IUserRepository userRepository, ITokenHelper tokenhelper, IMapper mapper, IMediator mediator, IRefreshTokenRepository refreshTokenRepository, IHashService hashService)
    {
        _userRepository = userRepository;
        _tokenhelper = tokenhelper;
        _mapper = mapper;
        _mediator = mediator;
        _refreshTokenRepository = refreshTokenRepository;
        _hashService = hashService;
    }

    public async Task<RefreshResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var storedToken = await _mediator.Send(new GetHashedRefreshTokenQuery { Token = request.RefreshToken });

        if (storedToken == null ||
            storedToken.IsRevoked ||
            storedToken.ExpiryDate < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Geçersiz refresh token");
        }

        var user = await _userRepository.GetAsync(x=>x.Id==storedToken.UserId);
        var userDto= _mapper.Map<UserDto>(user);
        var newAccessToken = _tokenhelper.GenerateAccessToken(userDto);

        var newRefreshToken = _tokenhelper.GenerateRefreshToken();
        var newRefreshTokenHash = _hashService.Hash(newRefreshToken);

        // 🔁 Rotation
        storedToken.IsRevoked = true;
        storedToken.RevokedAt = DateTime.UtcNow;
        storedToken.ReplacedByToken = newRefreshTokenHash;

        var storedTokenEntity = _mapper.Map<RefreshToken>(storedToken);
        await _refreshTokenRepository.UpdateAsync(storedTokenEntity);

        var tokendto = new CreateRefreshTokenCommand
        {
            UserId = user.Id,
            Token = newAccessToken.RefreshToken,
            ExpiryDate = newAccessToken.AccessTokenExpiry.AddDays(7) // Refresh token expires in 7 days
        };

        await _mediator.Send(tokendto);

       var returnDto= _mapper.Map<RefreshResponse>(newAccessToken);
        return returnDto;   
    }


}