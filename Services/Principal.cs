using Carguero.FeatureFlag.Abstrations;

namespace Carguero.FeatureFlag.Services;

public class Principal: IPrincipal
{
    private readonly IHttpContextAccessor _contextAccessor;
    private long? _tenantId;

    public Principal(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public long? TenantId => _tenantId ??= GetClaim<long>("tenant");

    private T? GetClaim<T>(string claimType)
    {
        var claim = _contextAccessor
            .HttpContext?
            .User
            .Claims
            .FirstOrDefault(x => x.Type == claimType)?
            .Value;
        if (string.IsNullOrEmpty(claim))
            return default;

        return (T)Convert.ChangeType(claim, typeof(T));
    }
}