using FluentValidation;

namespace Application.Features.Messages.Commands.Update;

public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
{
    public UpdateMessageCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Mesaj ID'si boş olamaz.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Mesaj içeriği boş olamaz.")
            .MaximumLength(5000).WithMessage("Mesaj uzunluğu 5000 karakteri geçemez.");
    }
}
