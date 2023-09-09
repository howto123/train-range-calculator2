
namespace web.Properties;

public class RequiredSecrets
{
    public SecretTokenManager? TokenManager { get; set; }
    public string? loginpassword { get; set; }

    public class SecretTokenManager
    {
        public string? SecretKey { get; set; }
    }
}