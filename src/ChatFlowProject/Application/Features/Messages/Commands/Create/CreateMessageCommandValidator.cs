using FluentValidation;

namespace Application.Features.Messages.Commands.Create;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("Oda ID'si boş olamaz.");

        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("Gönderici ID'si boş olamaz.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Mesaj içeriği boş olamaz.")
            .MaximumLength(5000).WithMessage("Mesaj uzunluğu 5000 karakteri geçemez.");
    }
}
