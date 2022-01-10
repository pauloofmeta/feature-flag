using Carguero.FeatureFlag.Abstrations;
using Carguero.FeatureFlag.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;

namespace Carguero.FeatureFlag.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FeaturesController : ControllerBase
{
    private readonly IFeatureManager _featureManage;
    private readonly IRepository<Feature> _repository;
    private readonly IPrincipal _principal;

    public FeaturesController(IFeatureManager featureManage,
        IRepository<Feature> repository,
        IPrincipal principal)
    {
        _featureManage = featureManage;
        _repository = repository;
        _principal = principal;
    }

    [HttpGet]
    public IAsyncEnumerable<string> Get() => 
        _featureManage.GetFeatureNamesAsync();

    [HttpPut("toogle/{featureName:alpha}")]
    public async Task<IActionResult> ToogleFeature(string featureName)
    {
        var feature = await _repository.GetQueryable()
            .Include(x => x.Tenants)
            .SingleOrDefaultAsync(x => x.Name == featureName);
        if (feature == default)
            return BadRequest(new { error = "Feature não encontrada" });

        var tenats = feature.Tenants?.ToList() ?? new List<TenantFeature>();
        if (tenats.Any(x => x.TenantId == _principal.TenantId))
            tenats.RemoveAll(x => x.TenantId == _principal.TenantId);
        else
            tenats.Add(new TenantFeature { TenantId  = _principal.TenantId.Value });
        feature.Tenants = tenats;
        _repository.Update(feature);
        await _repository.CommitAsync();
        return Ok();
    }
}
