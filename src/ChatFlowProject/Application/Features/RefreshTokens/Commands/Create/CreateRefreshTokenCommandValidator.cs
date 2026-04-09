using FluentValidation;

namespace Application.Features.RefreshTokens.Commands.Create;

public class CreateRefreshTokenCommandValidator:AbstractValidator<CreateRefreshTokenCommand>
{
    public CreateRefreshTokenCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage("Token alanı boş olamaz.");
        RuleFor(x => x.ExpiryDate).GreaterThan(DateTime.UtcNow).WithMessage("ExpiryDate, şu anki tarihten büyük olmalıdır.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId alanı boş olamaz.");
    }
}