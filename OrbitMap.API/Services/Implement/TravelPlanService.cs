using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Payload.Request.TravelPlan;
using OrbitMap.API.Payload.Response.TravelPlan;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class TravelPlanService : BaseService<TravelPlanService>, ITravelPlanService
{
    private readonly IUploadService _uploadService;

    public TravelPlanService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IUploadService uploadService) : base(unitOfWork, logger, mapper,
        httpContextAccessor)
    {
        _uploadService = uploadService;
    }

    public async Task<TravelPlanResponse> CreateTravelPlanAsync(CreateTravelPlanRequest request)
    {
        var location = await _unitOfWork.GetRepository<Location>().SingleOrDefaultAsync(
            predicate: x => x.Name.Equals(request.LocationName),
            include: x => x.Include(x => x.TravelPlans)
        );
        if (location == null)
            throw new BadHttpRequestException("Không tìm thấy địa điểm");
        if (location.TravelPlans.Any(x => x.Type.Equals(request.Type)))
        {
            throw new BadHttpRequestException("Kế hoạch du lịch đã tồn tại");
        }

        var travelPlan = new TravelPlan
        {
            Id = Guid.NewGuid(),
            LocationId = location.Id,
            Type = request.Type,
            TravelPlanDays = await Task.WhenAll(request.TravelPlanDays.Select(async x => new TravelPlanDay
            {
                Id = Guid.NewGuid(),
                Day = x.Day,
                TravelPlanItems = await Task.WhenAll(x.TravelPlanItems.Select(async y => new TravelPlanItem
                {
                    Id = Guid.NewGuid(),
                    Time = y.Time,
                    Name = y.Name,
                    Address = y.Address,
                    ImageUrl = y.Image != null ? await _uploadService.UploadImageAsync(y.Image) : null
                }).ToList())
            }).ToList())
        };
        await _unitOfWork.GetRepository<TravelPlan>().InsertAsync(travelPlan);
        var isSuccess = await _unitOfWork.CommitAsync() > 0;
        if (!isSuccess)
            throw new Exception("Lỗi khi tạo kế hoạch du lịch");
        return _mapper.Map<TravelPlanResponse>(travelPlan);
    }

    public async Task<List<TravelPlanResponse>?> GetTravelPlanAsync(string locationName)
    {
        var location = await _unitOfWork.GetRepository<Location>().SingleOrDefaultAsync(
            predicate: x => x.Name.Equals(locationName),
            include: x => x.Include(x => x.TravelPlans)
                .ThenInclude(x => x.TravelPlanDays)
                .ThenInclude(x => x.TravelPlanItems)
        );
        if (location == null)
        {
            throw new BadHttpRequestException("Không tìm thấy địa điểm");
        }

        if (location.TravelPlans.Any())
        {
            foreach (var travelPlan in location.TravelPlans)
            {
                travelPlan.TravelPlanDays = travelPlan.TravelPlanDays
                    .OrderBy(x => x.Day)
                    .Select(x =>
                    {
                        x.TravelPlanItems = x.TravelPlanItems
                            .OrderBy(item => item.Time)
                            .ToList();
                        return x;
                    }).ToList();
            }

            return _mapper.Map<List<TravelPlanResponse>>(location.TravelPlans);
        }

        return null;
    }
}