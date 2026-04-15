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

public class GetListRoomQuery : IRequest<List<GetListRoomResponse>>
{
    public Guid UserId { get; set; }
}
public class GetListRoomQueryHandler : IRequestHandler<GetListRoomQuery, List<GetListRoomResponse>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;
    public GetListRoomQueryHandler(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }
    public async Task<List<GetListRoomResponse>> Handle(GetListRoomQuery request, CancellationToken cancellationToken)
    {
        var results = await _roomRepository.GetListRoomWithUnreadAsync(request.UserId);
        return _mapper.Map<List<GetListRoomResponse>>(results);
    }
}
