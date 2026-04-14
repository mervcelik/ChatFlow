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

namespace Application.Features.Rooms.Queries.GetList;

public class GetListRoomQuery:IRequest<GetListResponse<GetListRoomResponse>>
{
    public Guid UserId { get; set; }
}
public class GetListRoomQueryHandler : IRequestHandler<GetListRoomQuery, GetListResponse<GetListRoomResponse>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;
    public GetListRoomQueryHandler(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }
    public async Task<GetListResponse<GetListRoomResponse>> Handle(GetListRoomQuery request, CancellationToken cancellationToken)
    {
        var results = await _roomRepository.GetListRoomWithUnreadAsync(request.UserId);
        return _mapper.Map<GetListResponse<GetListRoomResponse>>(results);
    }
}
