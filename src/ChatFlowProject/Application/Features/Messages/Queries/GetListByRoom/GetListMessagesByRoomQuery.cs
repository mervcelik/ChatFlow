using Application.Repositories;
using AutoMapper;
using Core.Application.Dtos;
using Core.CrossCuttingConcerns.Extensions;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.Messages.Queries.GetListByRoom;

public class GetListMessagesByRoomQuery : IRequest<GetListResponse<GetListMessageResponse>>
{
    public Guid? RoomId { get; set; }

    public int PageIndex { get; set; } = 0;

    public int PageSize { get; set; } = 20;
}

public class GetListMessagesByRoomQueryHandler : IRequestHandler<GetListMessagesByRoomQuery, GetListResponse<GetListMessageResponse>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public GetListMessagesByRoomQueryHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListMessageResponse>> Handle(GetListMessagesByRoomQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Message, bool>>? predicate = x => x.Id != null;

        if (request.RoomId != null)
        {
            predicate = predicate.And(x => x.RoomId == request.RoomId);
        }

        var messages = await _messageRepository.GetListAsync(
            request.PageIndex,
            request.PageSize,
            predicate,
            ct: cancellationToken
        );

        var response = _mapper.Map<GetListResponse<GetListMessageResponse>>(messages);
        return response;
    }
}
