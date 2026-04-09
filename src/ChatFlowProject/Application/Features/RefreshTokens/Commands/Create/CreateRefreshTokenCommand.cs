using Application.Repositories;
using Core.Security.Services.Hash;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RefreshTokens.Commands.Create;

public class CreateRefreshTokenCommand:IRequest<CreatedRefreshTokenResponse>
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public Guid UserId { get; set; }
}
public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, CreatedRefreshTokenResponse>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IHashService _hashService;

    public CreateRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IHashService hashService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _hashService = hashService;
    }

    public async Task<CreatedRefreshTokenResponse> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = new Domain.Entities.RefreshToken
        {
            Token = _hashService.Hash(request.Token),
            ExpiryDate = request.ExpiryDate,
            UserId = request.UserId
        };
        await _refreshTokenRepository.AddAsync(refreshToken);

        return new CreatedRefreshTokenResponse();
    }
}