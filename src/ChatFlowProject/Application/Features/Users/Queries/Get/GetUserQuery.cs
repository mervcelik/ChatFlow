using Application.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.Get;

public class GetUserQuery : IRequest<GetUserResponse>
{
    public Guid Id { get; set; }
}
public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    async Task<GetUserResponse> IRequestHandler<GetUserQuery, GetUserResponse>.Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(x => x.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("User", request.Id);
        }

        var response = _mapper.Map<GetUserResponse>(user);
        return response;
    }
}
