using Application.Features.Users.Rules;
using Application.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Services.PasswordService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Update.UpdatePassword;

public class UpdateUserPasswordCommand : IRequest<UpdatedUserPasswordResponse>
{
    public Guid Id { get; set; }
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;


}

public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, UpdatedUserPasswordResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;
    private readonly UserBusinessRules _userBusinessRules;
    public UpdateUserPasswordCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordService = passwordService;
    }

    public async Task<UpdatedUserPasswordResponse> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetAsync(u => u.Id == request.Id);
        if (entity == null)
        {
            throw new NotFoundException("Kullanıcı", request.Id);
        }

        _userBusinessRules.ConfirmOldPassword(request.OldPassword, entity.PasswordHash);
        _userBusinessRules.ConfirmNewPassword(request.NewPassword, request.ConfirmPassword);

        entity.PasswordHash = _passwordService.Hash(request.NewPassword);
        await _userRepository.UpdateAsync(entity);
        return new UpdatedUserPasswordResponse { Id = request.Id };
    }
}
