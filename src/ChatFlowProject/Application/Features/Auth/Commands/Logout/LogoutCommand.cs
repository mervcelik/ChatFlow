using Application.Features.RefreshTokens.Queries.Get;
using Application.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Services.Hash;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Logout;

public class LogoutCommand : IRequest<LogoutResponse>
{
    public string RefreshToken { get; set; }
}
public class LogoutCommandHandler : IRequestHandler<LogoutCommand, LogoutResponse>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository, IMediator mediator, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    async Task<LogoutResponse> IRequestHandler<LogoutCommand, LogoutResponse>.Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var token = await _mediator.Send(new GetHashedRefreshTokenQuery { Token = request.RefreshToken });

        if (token == null || token.IsRevoked)
            throw new BusinessException("Çıkış işlemi sırasında bir hata oluştu.");

        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;
        var update = _mapper.Map<RefreshToken>(token);
        await _refreshTokenRepository.UpdateAsync(update);

        return new LogoutResponse();
    }
}