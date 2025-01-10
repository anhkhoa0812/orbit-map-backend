using AutoMapper;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class StoryCleanupService : BaseService<StoryCleanupService>
{
    public StoryCleanupService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task RemoveExpiredStories()
    {
        var expiredStories = await _unitOfWork.GetRepository<Story>().GetListAsync(
            predicate: s => s.ExpirationDate <= DateTime.UtcNow
        );
        foreach (var story in expiredStories)
        {
            story.IsDisabled = true;
        }
        _unitOfWork.GetRepository<Story>().UpdateRange(expiredStories);
        await _unitOfWork.CommitAsync();
    }
}