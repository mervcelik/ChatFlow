using Application.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using MediatR;

namespace Application.Features.Messages.Commands.Update;

public class UpdateMessageCommand : IRequest<UpdatedMessageResponse>
{
    public Guid Id { get; set; }

    public string Content { get; set; } = string.Empty;
}

public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, UpdatedMessageResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public UpdateMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<UpdatedMessageResponse> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.GetAsync(x => x.Id == request.Id, false, cancellationToken);

        if (message == null)
            throw new NotFoundException("Mesaj", request.Id);

        message.Content = request.Content;
        message.EditedAt = DateTime.UtcNow;

        await _messageRepository.UpdateAsync(message);

        return _mapper.Map<UpdatedMessageResponse>(message);
    }
}
