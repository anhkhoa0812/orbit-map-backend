using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.User;

namespace OrbitMap.API.Services.Interface;

public interface IBusinessService
{
    public Task<BusinessResponse> CreateBusinessAsync(CreateBusinessRequest request);
}