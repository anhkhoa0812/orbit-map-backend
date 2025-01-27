using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Services.Interface;

public interface IPaymentService
{
    Task<string> Checkout(string username);
    Task<string> CheckoutBusiness(string username, EBusinessService businessService);
}