using BuySubs.BLL.Interfaces;
using BuySubs.Common.Options;
using BuySubs.Common.Security;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BuySubs.BLL.Services;

public sealed class TokenService : ITokenService
{
    private readonly JwtOptions _options;

    public TokenService(IOptions<JwtOptions> options)
        => _options = options.Value;

    public string GenerateAccessToken(Guid userId)
        => new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken(
                    _options.Issuer,
                    _options.Audience,
                    new Claim[] {
                        new(
                            ClaimTypes.NameIdentifier,
                            userId.ToString()
                        ),
                        new(
                            JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString()
                        )
                    },
                    _options.NotBefore,
                    _options.Expiration,
                    _options.SigningCredentials
        ));

    public string GenerateRefreshToken()
        => Convert.ToBase64String(SecurityHelper.GetRandomBytes());
}