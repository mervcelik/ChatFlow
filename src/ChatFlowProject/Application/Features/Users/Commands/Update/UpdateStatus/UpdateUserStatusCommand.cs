using Application.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Update.UpdateStatus;

public class UpdateUserStatusCommand : IRequest
{
    public Guid Id { get; set; }
    public UserStatus UserStatus { get; set; }
}

public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand>
{
    private readonly IUserRepository _userRepository;
    public UpdateUserStatusCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException("Kullanıcı", request.Id);
        }

        entity.Status = request.UserStatus;

        await _userRepository.UpdateAsync(entity, cancellationToken);
    }
}