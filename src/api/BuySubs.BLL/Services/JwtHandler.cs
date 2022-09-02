using BuySubs.BLL.Interfaces;
using BuySubs.Common.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BuySubs.BLL.Services;

internal sealed class JwtHandler : IJwtHandler
{
    private readonly JwtOptions _options;

    public JwtHandler(IOptions<JwtOptions> options)
        => _options = options.Value;

    public string GetAccessToken(string email)
        => new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: new Claim[] {
                        new Claim(
                            ClaimTypes.NameIdentifier,
                            email
                        ),
                        new Claim(
                            JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString()
                        )
                    },
                    notBefore: _options.NotBefore,
                    expires: _options.Expiration,
                    signingCredentials: _options.SigningCredentials
            ));
}