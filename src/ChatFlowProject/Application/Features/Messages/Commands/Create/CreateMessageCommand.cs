using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Messages.Commands.Create;

public class CreateMessageCommand : IRequest<CreatedMessageResponse>
{
    public Guid RoomId { get; set; }

    public Guid SenderId { get; set; }

    public string Content { get; set; } = string.Empty;

    public MessageType Type { get; set; } = MessageType.Text;

    public Guid? ReplyToId { get; set; }

    public MessageAttachment? Attachment { get; set; }
}

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CreatedMessageResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public CreateMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<CreatedMessageResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = _mapper.Map<Message>(request);
        await _messageRepository.AddAsync(message);

        return _mapper.Map<CreatedMessageResponse>(message);
    }
}
