using Application.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;

namespace Application.Features.Messages.Commands.Delete;

public class DeleteMessageCommand : IRequest<DeletedMessageResponse>
{
    public Guid Id { get; set; }
}

public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, DeletedMessageResponse>
{
    private readonly IMessageRepository _messageRepository;

    public DeleteMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<DeletedMessageResponse> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.GetAsync(x => x.Id == request.Id, false, cancellationToken);

        if (message == null)
            throw new NotFoundException("Mesaj", request.Id);

        await _messageRepository.DeleteAsync(message);

        return new DeletedMessageResponse { Id = message.Id };
    }
}
