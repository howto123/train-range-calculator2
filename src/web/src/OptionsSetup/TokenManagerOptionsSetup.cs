using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using web.jwt;

namespace web.OptionsSetup;

public class TokenManagerOptionsSetup : IConfigureOptions<TokenManagerOptions>
{
    private const string SectionName = "TokenManager";
    private readonly IConfiguration _configuration;

    public TokenManagerOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(TokenManagerOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
