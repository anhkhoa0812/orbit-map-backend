namespace OrbitMap.API.Utils;

public class JwtUtil
{
    // private JwtUtil()
    // {
    // }
    //
    // public static string GenerateJwtToken(Account account, Tuple<string, Guid> guidClaim)
    // {
    //     JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
    //     SymmetricSecurityKey secrectKey =
    //         new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PosSystemNumberOne"));
    //     var credentials = new SigningCredentials(secrectKey, SecurityAlgorithms.HmacSha256Signature);
    //     List<Claim> claims = new List<Claim>()
    //     {
    //         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //         new Claim(JwtRegisteredClaimNames.Sub, account.Username),
    //         new Claim(ClaimTypes.Role, account.Role.Name),
    //     };
    //     if (guidClaim != null) claims.Add(new Claim(guidClaim.Item1, guidClaim.Item2.ToString()));
    //     var expires = account.Role.Name.Equals(RoleEnum.Staff.GetDescriptionFromEnum())
    //         ? DateTime.Now.AddDays(15)
    //         : DateTime.Now.AddDays(30);
    //     var token = new JwtSecurityToken("PosSystem", null, claims, notBefore: DateTime.Now, expires, credentials);
    //     return jwtHandler.WriteToken(token);
    // }
}