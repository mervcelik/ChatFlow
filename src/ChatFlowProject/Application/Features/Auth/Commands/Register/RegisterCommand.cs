using Application.Features.Auth.BusinessRules;
using Application.Features.RefreshTokens.Commands;
using Application.Features.RefreshTokens.Commands.Create;
using Application.Repositories;
using AutoMapper;
using Core.Security.JWT;
using Core.Security.Services.PasswordService;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisterResponse>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PasswordConfirm { get; set; } = string.Empty;
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly IPasswordService _passwordService;
    private readonly ITokenHelper _tokenHelper;
    private readonly IMediator _mediator;
    public RegisterCommandHandler(IMapper mapper, IUserRepository userRepository, AuthBusinessRules authBusinessRules, IPasswordService passwordService, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository, IMediator mediator)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _authBusinessRules = authBusinessRules;
        _passwordService = passwordService;
        _tokenHelper = tokenHelper;
        _mediator = mediator;
    }
    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _authBusinessRules.RegisterEmailIsUniqueAsync(request.Email);
        await _authBusinessRules.RegisterUsernameIsUniqueAsync(request.Username);

        var user = _mapper.Map<User>(request);
        user.PasswordHash = _passwordService.Hash(request.Password);

        await _userRepository.AddAsync(user);

        var response = _mapper.Map<RegisterResponse>(user);

        var userDto = _mapper.Map<UserDto>(user);
        var tokenDto = _tokenHelper.GenerateAccessToken(userDto);
        response.AccessToken = tokenDto.AccessToken;
        response.RefreshToken = tokenDto.RefreshToken;
        response.AccessTokenExpiry = tokenDto.AccessTokenExpiry;

        var tokendto = new CreateRefreshTokenCommand
        {
            UserId = user.Id,
            Token = tokenDto.RefreshToken,
            ExpiryDate = tokenDto.AccessTokenExpiry.AddDays(7) // Refresh token expires in 7 days
        };

        await _mediator.Send(tokendto);

        return response;
    }
}
