namespace OrbitMap.API.Services.Interface;

public interface ICraftMyPdfService
{
    Task<string> GeneratePassport(string username);
}