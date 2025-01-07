using Microsoft.AspNetCore.Authorization;
using OrbitMap.API.Utils;
using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Validators;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public CustomAuthorizeAttribute(params ERoleEnum[] roleEnums)
    {
        var allowedRolesAsString = roleEnums.Select(x => x.GetDescriptionFromEnum());
        Roles = string.Join(",", allowedRolesAsString);
    }
}