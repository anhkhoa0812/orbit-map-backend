using System.Linq.Expressions;
using System.Security.Authentication;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Net.payOS;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Location;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.Utils;
using OrbitMap.Domain.Configurations;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;
using LoginRequest = OrbitMap.API.Payload.Request.User.LoginRequest;
using RegisterRequest = OrbitMap.API.Payload.Request.User.RegisterRequest;

namespace OrbitMap.API.Services.Implement;

public class UserService : BaseService<UserService>, IUserService
{
    private readonly PayOSSettings _payOsSettings;
    private readonly IRedisService _redisService;
    private readonly IUploadService _uploadService;

    public UserService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IRedisService redisService, IUploadService uploadService,
        IOptions<PayOSSettings> options) : base(
        unitOfWork, logger, mapper, httpContextAccessor)
    {
        _redisService = redisService;
        _uploadService = uploadService;
        _payOsSettings = options.Value;
    }

    public async Task<LoginResponse> Login(LoginRequest loginRequest)
    {
        Expression<Func<User, bool>> searchFilter = p =>
            (p.Username.Equals(loginRequest.Username) || p.PhoneNumber.Equals(loginRequest.Username)) &&
            p.PasswordHash.Equals(PasswordUtil.HashPassword(loginRequest.Password));
        var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
            predicate: searchFilter,
            include: x => x.Include(x => x.Role)
        );
        if (user == null) throw new BadHttpRequestException("Invalid username or password");

        var guidClaim = new Tuple<string, Guid>("userId", user.Id);
        var result = _mapper.Map<LoginResponse>(user);
        var token = JwtUtil.GenerateJwtToken(user, guidClaim);
        if (user.Role.Name.Equals(ERoleEnum.Member.ToString()))
        {
            var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
                predicate: x => x.Id.Equals(user.Id)
            );
            result.IsPremium = member.IsPremium;
            result.Birthday = member.Birthday;
        }

        result.Token = token;
        return result;
    }

    public async Task<LoginResponse> Register(RegisterRequest registerRequest)
    {
        var userList = await _unitOfWork.GetRepository<Member>().GetListAsync();
        if (userList.Any(x => x.Username.Equals(registerRequest.Username)))
            throw new BadHttpRequestException("Username is already taken");
        if (userList.Any(x => x.PhoneNumber.Equals(registerRequest.PhoneNumber)))
            throw new BadHttpRequestException("Phone number is already taken");

        var user = _mapper.Map<Member>(registerRequest);

        var key = registerRequest.PhoneNumber;
        var existingOtp = await _redisService.GetStringAsync(key);

        if (string.IsNullOrEmpty(existingOtp))
            throw new BadHttpRequestException("Can not find OTP code");
        if (!existingOtp.Equals(registerRequest.Otp))
            throw new BadHttpRequestException("Invalid OTP code");
        user.PasswordHash = PasswordUtil.HashPassword(registerRequest.Password);
        var role = await _unitOfWork.GetRepository<Role>().SingleOrDefaultAsync(
            predicate: x => x.Name.Equals(ERoleEnum.Member.ToString())
        );
        user.RoleId = role.Id;
        user.Id = Guid.NewGuid();
        user.IsPremium = false;

        await _unitOfWork.GetRepository<Member>().InsertAsync(user);

        var isSuccess = await _unitOfWork.CommitAsync() > 0;

        if (!isSuccess) throw new Exception("Register failed");

        var guidClaim = new Tuple<string, Guid>("userId", user.Id);
        var result = _mapper.Map<LoginResponse>(user);
        var userFromDb = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: u => u.Username == user.Username,
            include: u => u.Include(u => u.Role)
        );
        var token = JwtUtil.GenerateJwtToken(userFromDb, guidClaim);
        result.Token = token;
        return result;
    }

    public async Task<UserDto> UpdateProfile(string username, UpdateUserRequest updateUserRequest)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Unauthorized");
        var userList = await _unitOfWork.GetRepository<Member>().GetListAsync();
        if (!string.IsNullOrEmpty(updateUserRequest.Username) &&
            userList.Any(x => x.Username.Equals(updateUserRequest.Username)))
            throw new BadHttpRequestException("Username is already taken");
        var currentUser = userList.SingleOrDefault(x => x.Username.Equals(username));
        if (currentUser == null) throw new BadHttpRequestException("User not found");
        var updatedUser = _mapper.Map(updateUserRequest, currentUser);
        if (updateUserRequest.ImageFile != null)
        {
            var imageUrl = await _uploadService.UploadImageAsync(updateUserRequest.ImageFile);
            updatedUser.AvatarUrl = imageUrl;
        }

        _unitOfWork.GetRepository<Member>().UpdateAsync(updatedUser);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<UserDto>(updatedUser);
    }

    public async Task<bool> UpdateRank(string username, UpdateRankRequest updateRankRequest)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Unauthorized");
        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (member == null) throw new BadHttpRequestException("Unauthorized");
        var transaction = await _unitOfWork.GetRepository<Transaction>().SingleOrDefaultAsync(
            predicate: x => x.OrderCode.Equals(updateRankRequest.OrderCode)
        );
        if (transaction == null) throw new BadHttpRequestException("Transaction không tồn tại");
        var payOs = new PayOS(_payOsSettings.ClientId, _payOsSettings.ApiKey, _payOsSettings.ChecksumKey);
        var paymentLinkInformation = await payOs.getPaymentLinkInformation(updateRankRequest.OrderCode);
        if (paymentLinkInformation == null)
            throw new BadHttpRequestException("Không thể tìm thấy thông tin link thanh toán");
        switch (EnumUtil.ParseEnum<EPayOsStatus>(paymentLinkInformation.status))
        {
            case EPayOsStatus.PAID:
                member.IsPremium = true;
                member.ExpiredRankDate = DateTime.UtcNow.AddMonths(1);
                transaction.Status = ETransactionStatus.Success;
                _unitOfWork.GetRepository<Member>().UpdateAsync(member);
                _unitOfWork.GetRepository<Transaction>().UpdateAsync(transaction);
                var isSuccess = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccess) throw new Exception("Update rank failed");
                break;
            case EPayOsStatus.EXPIRED:
            case EPayOsStatus.CANCELLED:
                transaction.Status = ETransactionStatus.Failed;
                _unitOfWork.GetRepository<Transaction>().UpdateAsync(transaction);
                var isUpdateSuccess = await _unitOfWork.CommitAsync() > 0;
                if (!isUpdateSuccess) throw new Exception("Update transaction failed");
                break;
            default:
                throw new Exception("Update rank failed");
        }

        return false;
    }

    public async Task<MemberDto> GetProfile(string username)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Xác thực không thành công");
        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (member == null)
            throw new AuthenticationException("Xác thực không thành công");
        var friendships = await _unitOfWork.GetRepository<Friendship>().GetListAsync(
            selector: f => new Friendship()
            {
                Id = f.Id,
                Status = f.Status,
                Addressee = f.Addressee,
                CreatedDate = f.CreatedDate,
                RequesterId = f.RequesterId,
                AddresseeId = f.AddresseeId,
                Requester = f.Requester,
                LastModifiedDate = f.LastModifiedDate
            },
            predicate:
            f => (f.Requester.Username == username || f.Addressee.Username == username)
                 && f.Status == EFriendshipStatus.Accepted,
            include:
            f => f.Include(f => f.Requester).Include(f => f.Addressee),
            orderBy:
            x => x.OrderBy(x => x.LastModifiedDate ?? x.CreatedDate)
        );
        var friends =
            friendships.Select(f => f.Requester.Username == username ? f.Addressee : f.Requester);
        var friendsDto = _mapper.Map<List<MemberDto>>(friends);
        var result = _mapper.Map<MemberDto>(member);
        result.Friends = friendsDto;
        return result;
    }

    public async Task<MemberDto> ChangePassword(string username, ChangePasswordRequest changePasswordRequest)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Unauthorized");
        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (member == null) throw new BadHttpRequestException("User not found");
        if (!PasswordUtil.HashPassword(changePasswordRequest.OldPassword).Equals(member.PasswordHash))
            throw new BadHttpRequestException("Mật khẩu cũ không chính xác");
        member.PasswordHash = PasswordUtil.HashPassword(changePasswordRequest.NewPassword);
        _unitOfWork.GetRepository<Member>().UpdateAsync(member);
        var isSuccess = await _unitOfWork.CommitAsync() > 0;
        if (!isSuccess)
            throw new Exception("Change password failed");
        return _mapper.Map<MemberDto>(member);
    }

    public async Task<MemberDto> ForgetPassword(ForgetPasswordRequest forgetPasswordRequest)
    {
        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.PhoneNumber.Equals(forgetPasswordRequest.PhoneNumber)
        );
        if (member == null) throw new BadHttpRequestException("Không tìm thấy người dùng");
        var key = member.PhoneNumber;
        var existingOtp = await _redisService.GetStringAsync(key);
        if (string.IsNullOrEmpty(existingOtp))
            throw new BadHttpRequestException("Không tìm thấy mã OTP");
        if (!existingOtp.Equals(forgetPasswordRequest.Otp))
            throw new BadHttpRequestException("Mã OTP không chính xác");
        member.PasswordHash = PasswordUtil.HashPassword(forgetPasswordRequest.NewPassword);
        _unitOfWork.GetRepository<Member>().UpdateAsync(member);
        var isSuccess = await _unitOfWork.CommitAsync() > 0;
        if (!isSuccess)
            throw new Exception("Quên mật khẩu thất bại");
        return _mapper.Map<MemberDto>(member);
    }

    public async Task<List<LocationDto>> GetLocations(string username)
    {
        if (string.IsNullOrEmpty(username))
            throw new BadHttpRequestException("Không tìm thấy người dùng");

        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username),
            include: x => x.Include(x => x.MemberLocations).ThenInclude(x => x.Location)
        );
        if (member == null) throw new BadHttpRequestException("Không tìm thấy người dùng");

        var locations = member.MemberLocations?.Select(x => x.Location);

        if (locations == null) return new List<LocationDto>();

        return _mapper.Map<List<LocationDto>>(locations);
    }

    public async Task<bool> DeleteUser(string username, DeleteUserRequest request)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new BadHttpRequestException("Không tìm thấy người dùng");
        }

        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (member == null)
            throw new BadHttpRequestException("Không tìm thấy người dùng");

        if (!PasswordUtil.HashPassword(request.Password).Equals(member.PasswordHash))
            throw new BadHttpRequestException("Mật khẩu không chính xác");

        var stories = await _unitOfWork.GetRepository<Story>().GetListAsync(
            predicate: x => x.UserId.Equals(member.Id)
        );
        if (stories.Any())
        {
            _unitOfWork.GetRepository<Story>().DeleteRangeAsync(stories);
        }

        var friends = await _unitOfWork.GetRepository<Friendship>().GetListAsync(
            predicate: x => x.AddresseeId.Equals(member.Id) || x.RequesterId.Equals(member.Id)
        );
        if (friends.Any())
        {
            _unitOfWork.GetRepository<Friendship>().DeleteRangeAsync(friends);
        }

        var subscriptionIds = await _unitOfWork.GetRepository<SubscriptionIds>().GetListAsync(
            predicate: x => x.Username.Equals(member.Username)
        );
        if (subscriptionIds.Any())
        {
            _unitOfWork.GetRepository<SubscriptionIds>().DeleteRangeAsync(subscriptionIds);
        }

        var newsReactions = await _unitOfWork.GetRepository<NewsReaction>().GetListAsync(
            predicate: x => x.Username.Equals(member.Username)
        );
        if (newsReactions.Any())
        {
            _unitOfWork.GetRepository<NewsReaction>().DeleteRangeAsync(newsReactions);
        }

        var transactions = await _unitOfWork.GetRepository<Transaction>().GetListAsync(
            predicate: x => x.MemberId.Equals(member.Id)
        );
        if (transactions.Any())
        {
            _unitOfWork.GetRepository<Transaction>().DeleteRangeAsync(transactions);
        }

        var memberLocations = await _unitOfWork.GetRepository<MemberLocation>().GetListAsync(
            predicate: x => x.MemberId.Equals(member.Id)
        );
        if (memberLocations.Any())
        {
            _unitOfWork.GetRepository<MemberLocation>().DeleteRangeAsync(memberLocations);
        }

        var messages = await _unitOfWork.GetRepository<Message>().GetListAsync(
            predicate: x =>
                x.MessageDocument.RecipientUsername.Equals(member.Username) ||
                x.MessageDocument.SenderUsername.Equals(member.Username)
        );
        if (messages.Any())
        {
            _unitOfWork.GetRepository<Message>().DeleteRangeAsync(messages);
        }

        var lastMessageChats = await _unitOfWork.GetRepository<LastMessageChat>().GetListAsync(
            predicate: x => x.LastMessageChatDocument.RecipientUsername.Equals(member.Username) ||
                            x.LastMessageChatDocument.SenderUsername.Equals(member.Username)
        );
        if (lastMessageChats.Any())
        {
            _unitOfWork.GetRepository<LastMessageChat>().DeleteRangeAsync(lastMessageChats);
        }

        _unitOfWork.GetRepository<Member>().DeleteAsync(member);
        var isSuccess = await _unitOfWork.CommitAsync() > 0;
        if (!isSuccess)
            throw new Exception("Xóa người dùng thất bại");
        return true;
    }
}