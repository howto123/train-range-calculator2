
namespace web.jwt;

public class TokenManagerOptions
{
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string SecretKey { get; init; } = default!;
    public string SecurityAlgorithm { get; set; } = default!;
}
