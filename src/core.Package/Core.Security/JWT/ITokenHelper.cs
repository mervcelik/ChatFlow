using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    TokenDto GenerateAccessToken(UserDto user);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateToken(string token);
}
