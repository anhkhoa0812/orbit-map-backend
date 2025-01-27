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
        var client = new RestClient("https://onesignal.com/api/v1/notifications?c=push");
        var request = new RestRequest("", Method.Post);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Authorization", $"Key {_config["OneSignal:RestAPI"]}");
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", json, ParameterType.RequestBody);
        var response = client.Execute(request);

        return Task.FromResult(new ResultOneSignal((int)response.StatusCode, response.Content!));
    }

    public async Task<SubscriptionIdsDto> AddSubscriptionIdAsync(string subscriptionId, string userName)
    {
        if (string.IsNullOrEmpty(subscriptionId) || subscriptionId == "null")
        {
            throw new ArgumentException("Subscription ID is null, empty, or invalid", nameof(subscriptionId));
        }

        var existPlayer = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(userName),
            include: u => u.Include(u => u.SubscriptionIds)
        );
        if (existPlayer == null)
            throw new BadHttpRequestException("Subscription ID is null or empty");
        var isExist = existPlayer.SubscriptionIds.SingleOrDefault(x => x.SubscriptionId == subscriptionId);
        if (isExist != null)
        {
            throw new Exception($"Subscription ID '{subscriptionId}' already exists for user '{userName}'");
        }

        var player = new SubscriptionIds
        {
            Id = Guid.NewGuid(),
            SubscriptionId = subscriptionId,
            MemberId = existPlayer.Id,
            Username = userName
        };
        await _unitOfWork.GetRepository<SubscriptionIds>().InsertAsync(player);
        var isSuccess = await _unitOfWork.CommitAsync() > 0;
        if (!isSuccess) throw new Exception("Can not add Subscription ID");
        return _mapper.Map<SubscriptionIdsDto>(player);
    }

    public async Task<SubscriptionIdsDto> RemoveSubscriptionIdAsync(string subscriptionId, string username)
    {
        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (member == null)
            throw new BadHttpRequestException("Không tìm thấy người dùng");
        var player = await _unitOfWork.GetRepository<SubscriptionIds>().SingleOrDefaultAsync(
            predicate: x => x.SubscriptionId.Equals(subscriptionId) && x.Username.Equals(username)
        );
        if (player == null)
            throw new BadHttpRequestException("Không tìm thấy Subscription ID");
        _unitOfWork.GetRepository<SubscriptionIds>().DeleteAsync(player);
        var isSuccess = await _unitOfWork.CommitAsync() > 0;
        if (!isSuccess) throw new Exception("Can not remove player id");
        return _mapper.Map<SubscriptionIdsDto>(player);
    }
}