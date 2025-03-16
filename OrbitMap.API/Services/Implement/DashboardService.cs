using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Payload.Response.Dashboard;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class DashboardService : BaseService<DashboardService>, IDashboardService
{
    public DashboardService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<DashboardResponseByDay> GetDashboardByDayAsync(DateTime date)
    {
        var members = await _unitOfWork.GetRepository<Member>().GetListAsync();

        var memberCount = members.Count;
        var memberCountLastMonth = members.Count(x => x.CreatedDate.Month == date.AddMonths(-1).Month);
        var memberCountPercentage = GetPercentage(memberCount, memberCountLastMonth);

        var newMemberCount = members.Count(x => x.CreatedDate.Date == date.Date);
        var newMemberCountYesterday = members.Count(x => x.CreatedDate.Date == date.AddDays(-1).Date);
        var newMemberCountPercentage = GetPercentage(newMemberCount, newMemberCountYesterday);


        var premiumMemberCount = members.Count(x => x.IsPremium);
        var freeMemberCount = members.Count(x => !x.IsPremium);

        var news = await _unitOfWork.GetRepository<News>().GetListAsync();
        var newsCountToday = news.Count(x => x.CreatedDate.Date == date.Date);
        var newsCountYesterday = news.Count(x => x.CreatedDate.Date == date.AddDays(-1).Date);
        var newsCountPercentage = GetPercentage(newsCountToday, newsCountYesterday);

        var travelPlan = await _unitOfWork.GetRepository<TravelPlan>().GetListAsync();
        var travelPlanCount = travelPlan.Count;

        var transactions = await _unitOfWork.GetRepository<Transaction>().GetListAsync(
            predicate: x => x.CreatedDate.Date == date.Date && x.Status == ETransactionStatus.Success
        );
        var moneyEarnedFromPremiumPack = transactions.Sum(x => x.Amount);

        var memberLocations = await _unitOfWork.GetRepository<MemberLocation>().GetListAsync(
            include: source => source.Include(x => x.Location)
        );
        var provinceCounts = memberLocations
            .GroupBy(x => x.Location.Name)
            .OrderByDescending(x => x.Count())
            .Select(x => new ProvinceCheckinCount()
            {
                ProvinceName = x.Key,
                CheckinCount = x.Count()
            }).ToList();
        return new DashboardResponseByDay()
        {
            MemberCount = memberCount,
            MemberCountPercentage = memberCountPercentage,
            NewMemberCount = newMemberCount,
            NewMemberCountPercentage = newMemberCountPercentage,
            PremiumMemberCount = premiumMemberCount,
            FreeMemberCount = freeMemberCount,
            NewsCount = newsCountToday,
            NewsCountPercentage = newsCountPercentage,
            TravelPlanCount = travelPlanCount,
            MoneyEarnedFromPremiumPack = moneyEarnedFromPremiumPack,
            ProvinceCheckinCounts = provinceCounts
        };
    }

    private double GetPercentage(int target, int previous)
    {
        if (previous == 0)
        {
            return target > 0 ? 100 : 0;
        }

        double percentageChange = ((double)(target - previous) / previous) * 100;
        return percentageChange;
    }
}