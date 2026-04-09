using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {      
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
            .MinimumLength(3).WithMessage("En az 3 karakter olmalıdır.")
            .MaximumLength(32).WithMessage("En fazla 32 karakter olabilir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre zorunludur.")
            .MinimumLength(8).WithMessage("En az 8 karakter olmalıdır.")
            .Matches("[A-Z]").WithMessage("En az bir büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("En az bir küçük harf içermelidir.")
            .Matches("[0-9]").WithMessage("En az bir rakam içermelidir.");

        RuleFor(x => x.PasswordConfirm)
            .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor.");
    }
}