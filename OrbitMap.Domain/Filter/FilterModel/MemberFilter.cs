using System.Linq.Expressions;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Filter.FilterModel;

public class MemberFilter : IFilter<Member>
{
    public string? Username { get; set; }
    public string? PhoneNumber { get; set; }

    public bool? IsPremium { get; set; }

    public Expression<Func<Member, bool>> ToExpression()
    {
        return member =>
            (string.IsNullOrEmpty(Username) || member.Username.Contains(Username)) &&
            (string.IsNullOrEmpty(PhoneNumber) || member.PhoneNumber.Contains(PhoneNumber))
            && (!IsPremium.HasValue || member.IsPremium == IsPremium);
    }
}