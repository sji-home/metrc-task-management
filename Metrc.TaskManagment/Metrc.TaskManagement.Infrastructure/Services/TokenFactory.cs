using Metrc.TaskManagement.Application.Contracts.Infrastructure;
using Metrc.TaskManagement.Application.DTOs.Authentication;
using Metrc.TaskManagement.Domain.DTOs;
using Metrc.TaskManagement.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Metrc.TaskManagement.Infrastructure.Services;

public class TokenFactory : ITokenFactory
{
    private readonly TokenOptions _tokenOptions;

    public TokenFactory(IOptions<TokenOptions> tokenOptionsSnapshot)
    {
        _tokenOptions = tokenOptionsSnapshot.Value;
    }
    public TokenResponse CreateAccessToken(UserDTO user)
    {
        return BuildAccessToken(user);
    }

    private TokenResponse BuildAccessToken(UserDTO user)
    {
        var tokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpirationMins);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken
        (
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: GetClaims(user),
                expires: tokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: creds
        );

        var handler = new JwtSecurityTokenHandler();
        var accessToken = handler.WriteToken(securityToken);

        return new(accessToken, tokenExpiration.Ticks);
    }

    private IEnumerable<Claim> GetClaims(UserDTO user)
    {
        var claims = new List<Claim>
                    {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email)
                    };

        foreach (var userRole in user.AppUserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.RoleName));
        }

        return claims;
    }
}
