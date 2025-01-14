namespace OrbitMap.API.Services.Interface;

public interface IPaymentService
{
    Task<string> Checkout(string username);
}