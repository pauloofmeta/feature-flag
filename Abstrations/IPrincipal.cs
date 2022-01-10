namespace Carguero.FeatureFlag.Abstrations;

public interface IPrincipal
{
    public long? TenantId { get; }
}