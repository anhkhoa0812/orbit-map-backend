using Hangfire.Dashboard;

namespace OrbitMap.API.Helper;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}