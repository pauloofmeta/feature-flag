namespace Carguero.FeatureFlag.Entities;

public class User
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long TenantId { get; set; }
    public Tenant? Tenant { get; set; }
}