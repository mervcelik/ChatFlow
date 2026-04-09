using Application.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Services.PasswordService;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.BusinessRules;

public class AuthBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    public AuthBusinessRules(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }


    public async Task RegisterEmailIsUniqueAsync(string email)
    {
        var result = await _userRepository.GetAsync(x => x.Email == email);
        if (result!=null)
            throw new BusinessException("Bu e-posta adresi zaten kullanılıyor.");
    }

    public async Task RegisterUsernameIsUniqueAsync(string username)
    {
        var result = await _userRepository.GetAsync(x => x.UserName == username);
        if (result != null)
            throw new BusinessException("Bu kullanıcı adı zaten alınmış.");
    }

    public async Task<User> LoginUserExistsAsync(string userName)
    {
        var result = await _userRepository.GetAsync(x => x.UserName == userName);
        if (result == null)
            throw new BusinessException("Bu kullanıcı adına sahip bir kullanıcı bulunamadı.");
        return result;
    }

    public void LoginPasswordIsCorrect(string password, string passwordHash)
    {
        if (!_passwordService.Verify(password, passwordHash))
            throw new BusinessException("Parola yanlış.");
    }
}
