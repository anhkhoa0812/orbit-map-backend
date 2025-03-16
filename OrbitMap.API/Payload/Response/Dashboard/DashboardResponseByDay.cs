namespace OrbitMap.API.Payload.Response.Dashboard;

public class DashboardResponseByDay
{
    public int MemberCount { get; set; }
    public double MemberCountPercentage { get; set; }
    public int NewMemberCount { get; set; }
    public double NewMemberCountPercentage { get; set; }
    public int NewsCount { get; set; }
    public double NewsCountPercentage { get; set; }
    public int TravelPlanCount { get; set; }
    public int PremiumMemberCount { get; set; }
    public int FreeMemberCount { get; set; }
    public decimal MoneyEarnedFromPremiumPack { get; set; }
    public List<ProvinceCheckinCount> ProvinceCheckinCounts { get; set; }
}

public class ProvinceCheckinCount()
{
    public string ProvinceName { get; set; }
    public int CheckinCount { get; set; }
}