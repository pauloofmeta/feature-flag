namespace Carguero.FeatureFlag.Entities;

public class Feature
{
    public long Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<TenantFeature>? Tenants { get; set; }
}