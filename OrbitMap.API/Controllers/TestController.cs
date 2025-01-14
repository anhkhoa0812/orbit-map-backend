using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Helper;
using OrbitMap.API.Services.Interface;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route("/api/v1/passport")]
public class TestController : BaseController<TestController>
{
    private readonly ICraftMyPdfService _craftMyPdfService;

    public TestController(ILogger logger, ICraftMyPdfService craftMyPdfService) :
        base(logger)
    {
        _craftMyPdfService = craftMyPdfService;
    }

    [HttpGet]
    public async Task<IActionResult> GeneratePassport()
    {
        var result = await _craftMyPdfService.GeneratePassport(User.GetUsername());
        return Ok(result);
    }
}