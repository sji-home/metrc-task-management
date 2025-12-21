namespace Metrc.TaskManagement.Infrastructure.Authentication;

public class TokenOptions
{
    public string Audience { get; set; } = String.Empty;
    public string Issuer { get; set; } = String.Empty;
    public long AccessTokenExpirationMins { get; set; }
    public string Secret { get; set; } = String.Empty;
}
