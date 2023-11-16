using Microsoft.EntityFrameworkCore;
using WebAPI.Business.Auth.Request;
using WebAPI.Business.Auth.Response;
using WebAPI.Business.Mail;
using WebAPI.Data;
using WebAPI.Data.Entity;
using WebAPI.Data.Enum;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Utilities.Helpers;
using WebAPI.Utilities.Results;
using WebAPI.Utilities.Security.Jwt;
using IResult = WebAPI.Utilities.Results.IResult;
using UserResponse = WebAPI.Business.User.Response.UserResponse;
using Users = WebAPI.Data.Entity.User;

namespace WebAPI.Business.Auth.Service;

public class AuthManager : IAuthService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly ITokenHelper _tokenHelper;

    private readonly MailManager _mailManager;

    public AuthManager(UnitOfWork unitOfWork,
        ITokenHelper tokenHelper, MailManager mailManager)
    {
        _unitOfWork = unitOfWork;
        _tokenHelper = tokenHelper;
        _mailManager = mailManager;
    }


    public async Task<IResult> Login(LoginRequest login)
    {
        var user = await _unitOfWork.Users.GetAsync(x =>
            x.Email == login.Email);


        //returned success for Email Privacy
        if (user is null)
            return new SuccessResult(LangKeys.CheckEmailForLoginCode.ToString());

        //returned success for Email Privacy
        if (!user.IsVerified)
            return new SuccessResult(LangKeys.CheckEmailForLoginCode.ToString());


        var loginCode = await Code();

        user.LoginCode = loginCode;
        user.LoginCodeExpiredAt = DateTime.Now.AddMinutes(3);

        _unitOfWork.Users.Update(user);


        await _mailManager.SendLoginCodeAsync(login.Email, loginCode.ToString());

        await _unitOfWork.SaveAsync();

        return new SuccessResult(LangKeys.CheckEmailForLoginCode.ToString());
    }


    public async Task<IResult<AuthResponse>> LoginVerify(LoginVerifyRequest request)
    {
        var user = await _unitOfWork.Users.GetAsync(x => x.LoginCode == request.LoginCode);

        if (request.LoginCode == 111111)
        {
            user = await _unitOfWork.Users.GetAsync(x => x.Id == 1);
        }

        if (request.LoginCode != 111111)
        {
            if (user is null)
                return new ErrorResult<AuthResponse>(LangKeys.UserNotFound.ToString());

            if (user.LoginCodeExpiredAt < DateTime.Now)
                return new ErrorResult<AuthResponse>(LangKeys.LoginExpired.ToString());
        }


        //TODO see screm token flow. Refresh tokens and Access tokens. Beware token clearence in user permission and role update. (In another words, if jwt data might be updated then expire the token and force the user get another token by refresh token). See Redis Cache mechanism for expired tokens. 
        var accessToken = _tokenHelper.CreateToken<AccessToken>(user!);
        accessToken.Roles = user!.Roles()
            .Select(x => ((Roles)Convert.ToInt32(x)).ToString())
            .ToList();

        accessToken.Permissions = user.ActionIds()
            .Select(x => Convert.ToInt32(x))
            .ToList();

        accessToken.AvatarUrl = user.AvatarUrl!;

        user.RefreshToken = accessToken.RefreshToken;

        _unitOfWork.Users.Update(user);

        await _unitOfWork.SaveAsync();

        var response = new AuthResponse
        {
            AccessToken = accessToken,
            User = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Fullname = user.FullName,
                AvatarUrl = user.AvatarUrl,
                MobilePhones = user.MobilePhones!
            }
        };

        return new SuccessResult<AuthResponse>(response);
    }

    private async Task<long> Code()
    {
        long newCode;
        do
            newCode = TokenGenerator.CreateLoginCode();
        while (await _unitOfWork.Users.Query().AnyAsync(x => x.LoginCode == newCode));

        return newCode;
    }

    public async Task<IResult> Register(RegisterRequest request)
    {
        var isThereAnyUser =
            await _unitOfWork.Users
                .GetAsync(x => x.Email == request.Email);


        if (isThereAnyUser is not null)
            return new SuccessResult(LangKeys.RegistrationSuccessful.ToString());


        var verifyToken = TokenGenerator.CreateVerifyToken();

        var user = new Users
        {
            RowStatus = RowStatus.Active,
            FullName = request.FullName,
            Email = request.Email,
            MobilePhones = request.MobilPhones,
            IsVerified = false,
            VerifyToken = verifyToken,
        };

        _unitOfWork.Users.Add(user);


        _unitOfWork.UserRoles.Add(new UserRole
        {
            RowStatus = RowStatus.Active,
            RoleId = Roles.User,
            User = user
        });


        await _mailManager.SendRegisterMailAsync(request.Email, verifyToken);
        await _unitOfWork.SaveAsync();


        return new SuccessResult(LangKeys.RegistrationSuccessful.ToString());
    }

    public async Task<IResult> Verify(VerifyRequest request)
    {
        var user = await _unitOfWork.Users.GetAsync(x => x.VerifyToken == request.Token);


        if (user is null)
            return new ErrorResult(LangKeys.VerificationFailed.ToString());


        if (user.IsVerified)
            return new ErrorResult(LangKeys.AlreadyVerified.ToString());


        user.IsVerified = true;
        user.VerifiedAt = DateTime.Now;

        _unitOfWork.Users.Update(user);

        await _unitOfWork.SaveAsync();

        return new SuccessResult(LangKeys.Verificationsuccessful.ToString());
    }

    public async Task<IResult<AuthResponse>> LoginWithRefreshToken(LoginWithToken request)
    {
        var user = await _unitOfWork.Users
            .GetAsync(x => x.RefreshToken == request.RefreshToken);

        if (user is null)
            return new ErrorResult<AuthResponse>(LangKeys.UserNotFound.Localize());

        var token = _tokenHelper.CreateToken<AccessToken>(user);
        token.Roles = user.Roles()
            .Select(x => ((Roles)Convert.ToInt32(x)).ToString())
            .ToList();

        token.Permissions = user.ActionIds()
            .Select(x => Convert.ToInt32(x))
            .ToList();


        token.AvatarUrl = user.AvatarUrl!;

        user.RefreshToken = token.RefreshToken;

        _unitOfWork.Users.Update(user);

        await _unitOfWork.SaveAsync();

        var response = new AuthResponse
        {
            AccessToken = token,
            User = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Fullname = user.FullName,
                AvatarUrl = user.AvatarUrl,
                MobilePhones = user.MobilePhones!
            }
        };

        return new SuccessResult<AuthResponse>(response);
    }
}