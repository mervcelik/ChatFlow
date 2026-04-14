using FluentValidation;

namespace Application.Features.Rooms.Commands.Create;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş olamaz.");
    }
}