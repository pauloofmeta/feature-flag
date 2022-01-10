namespace Carguero.FeatureFlag.Entities;

public class Tenant
{
    public long Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<User>? Users { get; set; }
    public IEnumerable<TenantFeature>? Features { get; set; }
}