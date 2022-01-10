using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace Carguero.FeatureFlag.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly IFeatureManager _featureManager;

    public ProductsController(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    public async Task<IActionResult> Get()
    {
        if (await _featureManager.IsEnabledAsync("FeatureA"))
            return Ok(new { featureA = "Listando funcionalidade A" });

        return Ok(new { featureOrigin = "Listando funcionalidade original" });
    }
}