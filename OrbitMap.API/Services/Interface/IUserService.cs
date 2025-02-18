using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Location;
using OrbitMap.API.Payload.Response.User;
using LoginRequest = OrbitMap.API.Payload.Request.User.LoginRequest;

namespace OrbitMap.API.Services.Interface;

public interface IUserService
{
    public Task<LoginResponse> Login(LoginRequest loginRequest);

    public Task<LoginResponse> Register(RegisterRequest registerRequest);

    public Task<UserDto> UpdateProfile(string username, UpdateUserRequest updateUserRequest);

    public Task<bool> UpdateRank(string username, UpdateRankRequest updateRankRequest);

    public Task<MemberDto> GetProfile(string username);

    public Task<MemberDto> ChangePassword(string username, ChangePasswordRequest changePasswordRequest);

    public Task<MemberDto> ForgetPassword(ForgetPasswordRequest forgetPasswordRequest);

    public Task<List<LocationDto>> GetLocations(string username);
}