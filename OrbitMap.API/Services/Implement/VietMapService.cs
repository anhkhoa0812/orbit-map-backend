using System.Globalization;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Options;
using OrbitMap.API.Payload.Response.VietMap;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Configurations;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class VietMapService : BaseService<VietMapService>, IVietMapService
{
    private readonly VietMapSettings _settings;

    public VietMapService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IOptions<VietMapSettings> options) : base(unitOfWork, logger, mapper,
        httpContextAccessor)
    {
        _settings = options.Value;
    }

    public async Task<string?> GetAddressByLocation(double lat, double lng)
    {
        if (lat == null || lng == null)
            return null;
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                                                       "AppleWebKit/537.36 (KHTML, like Gecko) " +
                                                       "Chrome/98.0.4758.102 Safari/537.36");
        string latStr = lat.ToString(CultureInfo.InvariantCulture);
        string lngStr = lng.ToString(CultureInfo.InvariantCulture);
        var url = $"https://maps.vietmap.vn/api/reverse/v3?apikey={_settings.ApiKey}&lng={lngStr}&lat={latStr}";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<List<VietMapResponse>>(responseBody);
            return apiResponse![0].Display;
        }
        catch (Exception e)
        {
            _logger.Error("Lỗi khi lấy thông tin địa chỉ từ tọa độ:" + e.Message);
            return null;
        }
    }
}