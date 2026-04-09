using Application.Features.Auth.BusinessRules;
using Application.Features.RefreshTokens.Commands;
using Application.Features.RefreshTokens.Commands.Create;
using Application.Repositories;
using AutoMapper;
using Core.Security.JWT;
using Core.Security.Services.PasswordService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public string UserName { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly IMediator _mediator;
    public LoginCommandHandler(IMapper mapper, IUserRepository userRepository, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules, IRefreshTokenRepository refreshTokenRepository, IMediator mediator)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _tokenHelper = tokenHelper;
        _authBusinessRules = authBusinessRules;
        _mediator = mediator;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _authBusinessRules.LoginUserExistsAsync(request.UserName);
        _authBusinessRules.LoginPasswordIsCorrect(request.Password, user.PasswordHash);

        var userDto = _mapper.Map<UserDto>(user);
        var tokenDto =  _tokenHelper.GenerateAccessToken(userDto);

       var tokendto = new CreateRefreshTokenCommand
       {
            UserId = user.Id,
            Token = tokenDto.RefreshToken,
            ExpiryDate = tokenDto.AccessTokenExpiry.AddDays(7) // Refresh token expires in 7 days
        };

        await _mediator.Send(tokendto);

        var response = _mapper.Map<LoginResponse>(tokenDto);
        return response;
    }
}