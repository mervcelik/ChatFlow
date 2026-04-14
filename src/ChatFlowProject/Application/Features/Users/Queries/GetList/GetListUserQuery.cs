using Application.Repositories;
using AutoMapper;
using Core.Application.Dtos;
using Core.CrossCuttingConcerns.Extensions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQuery : IRequest<GetListResponse<GetListUserResponse>>
{
    public string? EmailOrUserName { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; } = 10;
}
public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListResponse<GetListUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListUserResponse>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>>? predicate = x => x.Id !=null;
        
        if (request.EmailOrUserName != null)
        {
            predicate = predicate.And(x =>x.UserName.Contains(request.EmailOrUserName, StringComparison.OrdinalIgnoreCase)
            || x.Email.Contains(request.EmailOrUserName, StringComparison.OrdinalIgnoreCase));
        }
        var results = await _userRepository.GetListAsync(request.Page, request.PageSize, predicate, false, cancellationToken);

        return _mapper.Map<GetListResponse<GetListUserResponse>>(results);
    }
}
