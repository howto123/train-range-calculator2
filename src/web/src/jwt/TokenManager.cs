



using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace web.jwt;

public class TokenManager
{
    private readonly SigningCredentials _credentials;
    private readonly JwtSecurityTokenHandler _handler;
    private readonly TokenManagerOptions _options;

    public TokenManager(IOptions<TokenManagerOptions> options)
    {
        _options = options.Value;
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        // default algorithm set here
        _credentials=new(key, _options.SecurityAlgorithm ?? SecurityAlgorithms.HmacSha256);
        _handler = new();
    }

    public string Create(IEnumerable<KeyValuePair<string, string>> claims)
    {
        var claimsAsClaims = GetClaimsFromPairs(claims);
        return _handler.WriteToken(CreateJWT(claimsAsClaims));
    }

    private JwtSecurityToken CreateJWT(IEnumerable<Claim> claims)
    {
        
        return new JwtSecurityToken
        (
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            _credentials
        );
    }

    private IEnumerable<Claim> GetClaimsFromPairs(IEnumerable<KeyValuePair<string, string>> pairs)
    {
        List<Claim> claims = new();
        foreach (var pair in pairs)
        {
            claims.Add(new Claim(pair.Key, pair.Value));
        }
        return claims;
    }
}