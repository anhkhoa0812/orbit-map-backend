using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Utils;

namespace OrbitMap.API.Utils;

public class JwtUtil
{
    private JwtUtil()
    {
    }

    public static string GenerateJwtToken(User user, Tuple<string, Guid> guidClaim)
    {
        JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey secrectKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("taolabocuachungmaychungmaylaconcuatao"));
        var credentials = new SigningCredentials(secrectKey, SecurityAlgorithms.HmacSha256Signature);
        List<Claim> claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(ClaimTypes.Role, user.Role.Name),
        };
        if (guidClaim != null) claims.Add(new Claim(guidClaim.Item1, guidClaim.Item2.ToString()));
        var expires = user.Role.Name.Equals(ERoleEnum.Admin.GetDescriptionFromEnum())
            ? TimeUtil.GetCurrentSEATime().AddDays(15)
            : TimeUtil.GetCurrentSEATime().AddDays(30);
        var token = new JwtSecurityToken("OrbitMap", null, claims, notBefore: TimeUtil.GetCurrentSEATime(), expires,
            credentials);
        return jwtHandler.WriteToken(token);
    }
}