using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Helper;
using OrbitMap.API.Services.Interface;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route(ApiEndPointConstant.Payment.PaymentEndpoint)]
public class PaymentController : BaseController<PaymentController>
{
    private readonly IPaymentService _paymentService;

    public PaymentController(ILogger logger, IPaymentService paymentService) : base(logger)
    {
        _paymentService = paymentService;
    }

    [HttpGet(ApiEndPointConstant.Payment.PaymentEndpoint)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Checkout()
    {
        var username = User.GetUsername();
        var result = await _paymentService.Checkout(username);
        return Ok(result);
    }
}