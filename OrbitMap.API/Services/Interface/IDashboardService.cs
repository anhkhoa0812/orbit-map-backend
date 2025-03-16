using OrbitMap.API.Payload.Response.Dashboard;

namespace OrbitMap.API.Services.Interface;

public interface IDashboardService
{
    Task<DashboardResponseByDay> GetDashboardByDayAsync(DateTime date);
}