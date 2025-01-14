using System.Security.Authentication;
using AutoMapper;
using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Types;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Configurations;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class PaymentService : BaseService<PaymentService>, IPaymentService
{
    private readonly PayOSSettings _settings;

    public PaymentService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IOptions<PayOSSettings> options) : base(
        unitOfWork, logger, mapper,
        httpContextAccessor)
    {
        _settings = options.Value;
    }

    public async Task<string> Checkout(string username)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Authentication failed");
        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (member == null)
            throw new AuthenticationException("Authentication failed");
        var payOs = new PayOS(_settings.ClientId, _settings.ApiKey, _settings.ChecksumKey);
        var orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
        var itemData = new List<ItemData>
        {
            new("Đăng ký gói hội viên Premium", 1, 39000)
        };
        var paymentData = new PaymentData(
            orderCode,
            39000,
            "Thanh toán đơn hàng",
            itemData,
            "https://stemlabs.store/cancel",
            "https://stemlabs.store/success",
            buyerName: member.DisplayName,
            buyerPhone: member.PhoneNumber,
            expiredAt: ((DateTimeOffset)DateTime.UtcNow.AddMinutes(10)).ToUnixTimeSeconds()
        );
        try
        {
            var createPayment = await payOs.createPaymentLink(paymentData);
            var result = createPayment.checkoutUrl;
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"Failed to create payment: {ex.Message}");
            throw new Exception("Failed to create payment");
        }
    }
}