using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Payload.Response.LastMessageChat;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Paginate.Interfaces;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class LastMessageChatService : BaseService<LastMessageChatService>, ILastMessageChatService
{
    public LastMessageChatService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IPaginate<LastMessageChatResponse>> GetLastMessageChat(int page, int size, string currentUsername)
    {
        var lastMassageChatPaging = await _unitOfWork.GetRepository<LastMessageChat>().GetPagingListAsync(
            predicate: x => x.LastMessageChatDocument.GroupName.Contains(currentUsername),
            page: page,
            size: size,
            orderBy: x => x.OrderByDescending(x => x.LastMessageChatDocument.MessageLastDate)
        );
        var result = _mapper.Map<IPaginate<LastMessageChatResponse>>(lastMassageChatPaging);
        return result;
    }
}