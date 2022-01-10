using Carguero.FeatureFlag.Abstrations;
using Carguero.FeatureFlag.Entities;
using Microsoft.FeatureManagement;

namespace Carguero.FeatureFlag.Services;

public class MyFeatures: IFeatureDefinitionProvider
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPrincipal _principal;

    public MyFeatures(IServiceProvider serviceProvider, IPrincipal principal)
    {
        _serviceProvider = serviceProvider;
        _principal = principal;
    }
    
    public async Task<FeatureDefinition?> GetFeatureDefinitionAsync(string featureName)
    {
        var feature = await GetFeatureAsync(featureName);
        return feature == default ? default : ToDefinition(feature);
    }

    public async IAsyncEnumerable<FeatureDefinition> GetAllFeatureDefinitionsAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IRepository<Feature>>();
        var features = service.GetAllAsync(x =>
                x.Tenants!.Any(t => t.TenantId == _principal.TenantId));

        await foreach (var feature in features)
            yield return ToDefinition(feature);
    }
    
    private static FeatureDefinition ToDefinition(Feature feature) =>
        new()
        { 
            Name = feature.Name, 
            EnabledFor = new []
            {
                new FeatureFilterConfiguration { Name = "AlwaysOn"}
            }
        };

    private async Task<Feature?> GetFeatureAsync(string featureName)
    {
        using var scope = _serviceProvider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IRepository<Feature>>();
        return await service.GetAsync(x =>
            x.Name == featureName && x.Tenants!.Any(t => t.TenantId == _principal.TenantId));
    }
}