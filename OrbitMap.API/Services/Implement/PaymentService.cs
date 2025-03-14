using System.Security.Authentication;
using AutoMapper;
using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Types;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.Utils;
using OrbitMap.Domain.Configurations;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Domain.Utils;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;
using Transaction = OrbitMap.Domain.Entities.Transaction;

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
            throw new AuthenticationException("Xác thực không thành công");
        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (member == null)
            throw new AuthenticationException("Xác thực không thành công");
        if (member.IsPremium)
            throw new BadHttpRequestException("Bạn đã đăng ký gói");
        var payOs = new PayOS(_settings.ClientId, _settings.ApiKey, _settings.ChecksumKey);
        var orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
        var itemData = new List<ItemData>
        {
            new("Đăng ký gói hội viên Premium", 1, 39000)
        };
        var paymentData = new PaymentData(
            orderCode,
            10000,
            "Thanh toán đơn hàng",
            itemData,
            "https://orbitmap.vn/cancel",
            "https://orbitmap.vn/success",
            buyerName: member.DisplayName,
            buyerPhone: member.PhoneNumber,
            expiredAt: ((DateTimeOffset)TimeUtil.GetCurrentSEATime().AddMinutes(10)).ToUnixTimeSeconds()
        );
        // "https://stemlabs.store/cancel",
        // "https://stemlabs.store/success",
        try
        {
            var createPayment = await payOs.createPaymentLink(paymentData);
            var result = createPayment.checkoutUrl;
            if (result != null)
            {
                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    OrderCode = createPayment.orderCode,
                    Amount = 10000,
                    MemberId = member.Id,
                    Status = ETransactionStatus.Pending,
                    Description = "Đăng ký gói hội viên Gold"
                };
                await _unitOfWork.GetRepository<Transaction>().InsertAsync(transaction);
                var isSuccess = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccess)
                {
                    return null;
                }

                return result;
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.Error($"Failed to create payment: {ex.Message}");
            throw new Exception("Failed to create payment");
        }
    }

    public async Task<string> CheckoutBusiness(string username, EBusinessService businessServiceEnum)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Xác thực không thành công");
        var business = await _unitOfWork.GetRepository<Business>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (business == null)
        {
            throw new BadHttpRequestException("Không tìm thấy doanh nghiệp");
        }

        var businessService = await _unitOfWork.GetRepository<Domain.Entities.BusinessService>().SingleOrDefaultAsync(
            predicate: x => x.BusinessServiceType.Equals(businessServiceEnum)
        );
        if (businessService == null)
        {
            throw new BadHttpRequestException("Không tìm thấy dịch vụ của doanh nghiệp");
        }

        var payOs = new PayOS(_settings.ClientId, _settings.ApiKey, _settings.ChecksumKey);
        var orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
        var itemData = new List<ItemData>
        {
            new($"Đăng ký dịch vụ {businessServiceEnum.ToString()}", 1, businessService.Price)
        };
        var paymentData = new PaymentData(
            orderCode,
            businessService.Price,
            "Thanh toán đơn hàng",
            itemData,
            "https://stemlabs.store/cancel",
            "https://stemlabs.store/success",
            buyerName: business.DisplayName,
            buyerPhone: business.PhoneNumber,
            expiredAt: ((DateTimeOffset)TimeUtil.GetCurrentSEATime().AddMinutes(10)).ToUnixTimeSeconds()
        );
        try
        {
            var createPayment = await payOs.createPaymentLink(paymentData);
            var result = createPayment.checkoutUrl;
            if (result != null)
            {
                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    OrderCode = createPayment.orderCode,
                    Amount = businessService.Price,
                    Status = ETransactionStatus.Pending,
                    Description = $"Đăng ký dịch vụ {businessServiceEnum.ToString()}",
                };
                await _unitOfWork.GetRepository<Transaction>().InsertAsync(transaction);
                var isSuccess = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccess)
                {
                    return null;
                }

                return result;
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.Error($"Failed to create payment: {ex.Message}");
            throw new Exception("Failed to create payment");
        }
    }
}