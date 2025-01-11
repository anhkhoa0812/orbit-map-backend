using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Payload.Response.OneSignal;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using RestSharp;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class OneSignalService : BaseService<OneSignalService>, IOneSignalService
{
    private readonly IConfiguration _config;

    public OneSignalService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(unitOfWork, logger, mapper,
        httpContextAccessor)
    {
        _config = config;
    }

    public Task<ResultOneSignal> SendNotification(object body)
    {
        var json = JsonSerializer.Serialize(body);
        var client = new RestClient("https://onesignal.com/api/v1/notifications");
        var request = new RestRequest("", Method.Post);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Authorization", $"Basic {_config["OneSignal:RestAPI"]}");
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", json, ParameterType.RequestBody);
        var response = client.Execute(request);

        return Task.FromResult(new ResultOneSignal((int)response.StatusCode, response.Content!));
    }

    public async Task<PlayerIdsDto> AddPlayerId(string playerId, string userName)
    {
        if (!string.IsNullOrEmpty(playerId) || playerId != "null")
        {
            var existPlayer = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
                predicate: x => x.Username.Equals(userName),
                include: u => u.Include(u => u.PlayerIds)
            );
            if (existPlayer != null)
            {
                var isExist = existPlayer.PlayerIds.SingleOrDefault(x => x.PlayerId == playerId);

                if (isExist == null)
                {
                    var currentUser = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
                        predicate: x => x.Username.Equals(userName)
                    );
                    var player = new PlayerIds
                    {
                        Id = Guid.NewGuid(),
                        PlayerId = playerId,
                        Member = currentUser,
                        Username = currentUser.Username
                    };
                    await _unitOfWork.GetRepository<PlayerIds>().InsertAsync(player);
                    var isSuccess = await _unitOfWork.CommitAsync() > 0;
                    if (!isSuccess) throw new Exception("Can not add player id");
                    return _mapper.Map<PlayerIdsDto>(player);
                }
            }
        }

        throw new BadHttpRequestException("PlayerId is null or empty");
    }
}