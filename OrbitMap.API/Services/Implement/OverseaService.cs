using System.Globalization;
using System.Text.Json;
using AutoMapper;
using OrbitMap.API.Payload.Response.Hotel;
using OrbitMap.API.Payload.Response.Oversea;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class OverseaService : BaseService<OverseaService>, IOverseaService
{
    private readonly IVietMapService _vietMapService;

    public OverseaService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IVietMapService vietMapService) : base(unitOfWork, logger, mapper,
        httpContextAccessor)
    {
        _vietMapService = vietMapService;
    }

    public async Task<List<HotelResponse>> GetNearestHotelFromOversea(double lat, double lng)
    {
        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "C# OverpassClient/1.0");
        var url =
            $"https://overpass-api.de/api/interpreter";
        string latStr = lat.ToString(CultureInfo.InvariantCulture);
        string lonStr = lng.ToString(CultureInfo.InvariantCulture);
        var formData = new Dictionary<string, string>()
        {
            {
                "data",
                $"[out:json];(node[\"tourism\"=\"hotel\"](around:10000,{latStr},{lonStr});" +
                $"way[\"tourism\"=\"hotel\"](around:10000,{latStr},{lonStr});" +
                $"relation[\"tourism\"=\"hotel\"](around:10000,{latStr},{lonStr}););" +
                $"out center;"
            }
        };
        try
        {
            using (var content = new FormUrlEncodedContent(formData))
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                string responseBody = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonSerializer.Deserialize<OverpassResponse>(responseBody);
                var elememts = apiResponse.Elements.Take(10).ToList();
                var hotelTask = elememts.Select(async element =>
                {
                    string name = element.Tags != null && element.Tags.TryGetValue("name", out var tagName)
                        ? tagName
                        : element.Id.ToString();
                    string address = await _vietMapService.GetAddressByLocation(element.Lat, element.Lon);

                    return new HotelResponse()
                    {
                        Name = name,
                        Address = address,
                        Avatar = null
                    };
                });
                return (await Task.WhenAll(hotelTask)).ToList();
            }
        }
        catch (Exception e)
        {
            _logger.Error("Lỗi khi lấy thông tin khách sạn gần nhất:" + e.Message);
            throw new Exception("Lỗi khi lấy thông tin khách sạn gần nhất");
        }
    }
}