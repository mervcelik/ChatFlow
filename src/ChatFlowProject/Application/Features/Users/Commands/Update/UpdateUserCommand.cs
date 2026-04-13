using Application.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Services.PasswordService;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommand : IRequest<UpdatedUserResponse>
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;
    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordService = passwordService;
    }

    public async Task<UpdatedUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetAsync(x => x.Id == request.Id);
        if (entity == null)
        {
            throw new NotFoundException("Kullanıcı", request.Id);
        }
        
        entity= _mapper.Map(request, entity);
        await _userRepository.UpdateAsync(entity);
        var response = _mapper.Map<UpdatedUserResponse>(entity);
        return response;
    }
}
