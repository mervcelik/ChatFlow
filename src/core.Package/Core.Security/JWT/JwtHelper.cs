using Core.Security.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.JWT;

public class JwtHelper:ITokenHelper
{
    private readonly TokenOptions _tokenOptions;
    public JwtHelper(IConfiguration configuration)
    {
        _tokenOptions =
           configuration.GetSection("TokenOptions").Get<TokenOptions>()
           ?? throw new NullReferenceException($"\"{"TokenOptions"}\" section cannot found in configuration.");
    }

    public TokenDto GenerateAccessToken(UserDto user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddName(user.UserName);
        claims.AddAccessTokenExpirationDate(GetAccessTokenExpiry());

        var token = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: claims,
            expires: GetAccessTokenExpiry(),
            signingCredentials: credentials);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        return  new TokenDto
        {
            AccessToken = accessToken,
            RefreshToken = GenerateRefreshToken(),
            AccessTokenExpiry = GetAccessTokenExpiry()
        };
    }

    public string GenerateRefreshToken()
    {
        // Kriptografik olarak güvenli rastgele 64 byte — URL-safe base64
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

        var validationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _tokenOptions.Issuer,
            ValidAudience = _tokenOptions.Audience,
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.Zero // expire tam vaktinde olsun
        };

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(token, validationParams, out _);
            return principal;
        }
        catch
        {
            return null;
        }
    }

    public DateTime GetAccessTokenExpiry()
        => DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
}
