using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[Route(ApiEndPointConstant.ApiEndpoint)]
[ApiController]
public class BaseController<T> : ControllerBase where T : BaseController<T>
{
    protected ILogger _logger;

    public BaseController(ILogger logger)
    {
        _logger = logger;
    }
}