using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Services.Interface;

public interface ICraftMyPdfService
{
    Task<string> GeneratePassport(Member member, Location location);
}