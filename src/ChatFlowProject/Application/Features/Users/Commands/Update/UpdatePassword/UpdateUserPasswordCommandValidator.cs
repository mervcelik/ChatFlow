using FluentValidation;

namespace Application.Features.Users.Commands.Update.UpdatePassword;

public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{

    public UpdateUserPasswordCommandValidator()
    {
        RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Eski Şifre zorunludur.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Şifre zorunludur.")
            .MinimumLength(8).WithMessage("En az 8 karakter olmalıdır.")
            .Matches("[A-Z]").WithMessage("En az bir büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("En az bir küçük harf içermelidir.")
            .Matches("[0-9]").WithMessage("En az bir rakam içermelidir.");

        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifrenizi doğrulamak zorunludur.");
    }
}