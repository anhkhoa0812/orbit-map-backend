using System.Globalization;
using AutoMapper;
using OrbitMap.API.Payload.Response.Restaurant;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OrbitMap.API.Services.Implement;

public class FoodyService : BaseService<FoodyService>, IFoodyService
{
    public FoodyService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<List<RestaurantItemDto>> GetNearestRestaurant(double latitude, double longitude)
    {
        using HttpClient client = new HttpClient();

        // client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
        client.DefaultRequestHeaders.Add("X-Foody-Access-Token", "");
        client.DefaultRequestHeaders.Add("X-Foody-Api-Version", "1");
        client.DefaultRequestHeaders.Add("X-Foody-App-Type", "1004");
        client.DefaultRequestHeaders.Add("X-Foody-Client-Id", "");
        client.DefaultRequestHeaders.Add("X-Foody-Client-Language", "vi");
        client.DefaultRequestHeaders.Add("X-Foody-Client-Type", "1");
        client.DefaultRequestHeaders.Add("X-Foody-Client-Version", "3.0.0");
        client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
        client.DefaultRequestHeaders.Add("Cookie", "flg=vn; floc=217");
        // client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                                                       "AppleWebKit/537.36 (KHTML, like Gecko) " +
                                                       "Chrome/98.0.4758.102 Safari/537.36");
        string latStr = latitude.ToString(CultureInfo.InvariantCulture);
        string lonStr = longitude.ToString(CultureInfo.InvariantCulture);
        var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var url =
            $"https://www.foody.vn/__get/Place/GetListRestaurantNearBy?t={timestamp}&ResId=11349&Lat={latStr}&Lon={lonStr}";

        try
        {
            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.Error($"API returned status {response.StatusCode}: {responseBody}");
                throw new Exception($"API error: {response.StatusCode} - {responseBody}");
            }

            var apiResponse = JsonSerializer.Deserialize<RestaurantDataDto>(responseBody);

            return apiResponse.Items;
        }
        catch (Exception e)
        {
            _logger.Error("Lỗi khi lấy gợi ý nhà hàng gần nhất");
            throw new Exception("Lỗi khi lấy gợi ý nhà hàng gần nhất");
        }
    }
}