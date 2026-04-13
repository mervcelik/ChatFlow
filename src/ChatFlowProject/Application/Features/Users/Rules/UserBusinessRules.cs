using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Services.PasswordService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules;

public class UserBusinessRules
{
    private readonly IPasswordService _passwordService;
    public UserBusinessRules(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }

    public void ConfirmOldPassword(string oldPassword,string PaswordHash)
    {
        var confirm = _passwordService.Verify(oldPassword, PaswordHash);
        if (!confirm)
        {
            throw new BusinessException("Eski şifrenizi yanlış girdiniz.");
        }
    }

    public void ConfirmNewPassword(string newPassword,string confirmPassword)
    {
        if (newPassword!=confirmPassword)
        {
            throw new BusinessException("Şifreler eşleşmiyor. Lütfen tekrar kontrol edin.");
        }
    }
}
