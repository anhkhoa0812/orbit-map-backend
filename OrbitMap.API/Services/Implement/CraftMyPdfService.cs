using System.Security.Authentication;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Configurations;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class CraftMyPdfService : BaseService<CraftMyPdfService>, ICraftMyPdfService
{
    private readonly CraftMyPdfSettings _settings;

    public CraftMyPdfService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IOptions<CraftMyPdfSettings> options) : base(unitOfWork, logger,
        mapper, httpContextAccessor)
    {
        _settings = options.Value;
    }


    public async Task<string> GeneratePassport(string username)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Authentication failed");
        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (member == null)
            throw new AuthenticationException("Authentication failed");
        var data = new
        {
            name = member.DisplayName,
            id = member.Username,
            gender = "Nam",
            birthday = member.Birthday.ToString(),
            profile_picture = member.AvatarUrl
        };
        var jsonPayload = new
        {
            data = JsonConvert.SerializeObject(data),
            output_file = "output.jpg",
            export_type = "json",
            expiration = 10,
            template_id = _settings.TemplateId
        };
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("X-API-KEY", _settings.ApiKey);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(jsonPayload), Encoding.UTF8,
                "application/json");
            try
            {
                var response = await client.PostAsync(_settings.RequestUrl, jsonContent);
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch (Exception e)
            {
                _logger.Error($"Error when generate passport: {e.Message}");
                throw new Exception("Error when generate passport");
            }
        }
    }

    // public async Task<string> CreatePassport()
    // {
    //     var apiKey = "5d1aMTY1OTE6MTY2Nzc6UEIyUXhQVG9YT0t3QTZNTQ=";
    //     var data = new
    //     {
    //         name = "Anh Khoa",
    //         id = "123456789",
    //         gender = "Nam",
    //         birthday = "12-08-2003"
    //     };
    //     var jsonPayload = new
    //     {
    //         data = JsonConvert.SerializeObject(data),
    //         output_file = "output.jpg",
    //         export_type = "json",
    //         expiration = 10,
    //         template_id = "c9977b23bd6c01b4"
    //     };
    //
    //     using (var client = new HttpClient())
    //     {
    //         client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
    //         var jsonContent = new StringContent(JsonConvert.SerializeObject(jsonPayload), Encoding.UTF8,
    //             "application/json");
    //         try
    //         {
    //             var response = await client.PostAsync("https://api.craftmypdf.com/v1/create-image", jsonContent);
    //             var responseString = await response.Content.ReadAsStringAsync();
    //             return responseString;
    //         }
    //         catch (Exception e)
    //         {
    //             Console.WriteLine(e);
    //             throw;
    //         }
    //     }
    // }
}