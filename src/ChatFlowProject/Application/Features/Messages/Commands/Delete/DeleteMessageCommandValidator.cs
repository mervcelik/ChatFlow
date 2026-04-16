using FluentValidation;

namespace Application.Features.Messages.Commands.Delete;

public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
{
    public DeleteMessageCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Mesaj ID'si boş olamaz.");
    }
}
