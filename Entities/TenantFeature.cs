namespace Carguero.FeatureFlag.Entities;

public class TenantFeature
{
    public long Id { get; set; }
    public long FeatureId { get; set; }
    public long TenantId { get; set; }
    public Feature? Feature { get; set; }
    public Tenant? Tenant { get; set; }
}