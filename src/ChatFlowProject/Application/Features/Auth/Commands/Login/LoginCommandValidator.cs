using FluentValidation;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş olamaz.");
    }
}