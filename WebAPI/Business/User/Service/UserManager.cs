using System.Linq.Expressions;
using AutoMapper;
using WebAPI.Business.Auth.Response;
using WebAPI.Business.User.Request;
using WebAPI.Business.User.Response;
using WebAPI.Data;
using WebAPI.Data.Enum;
using WebAPI.Extensions;
using WebAPI.Utilities.Results;
using WebAPI.Utilities.Security.Jwt;
using IResult = WebAPI.Utilities.Results.IResult;

namespace WebAPI.Business.User.Service;

public class UserManager:IUserService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenHelper _tokenHelper;

    public UserManager(UnitOfWork unitOfWork, IMapper mapper, ITokenHelper tokenHelper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenHelper = tokenHelper;
    }

    public async Task<IResult<List<UserResponse>>> List(string? search,
        CancellationToken cancellationToken = default)
    {
        Expression<Func<Data.Entity.User, bool>>? expression = null;

        var isInt = int.TryParse(search, out var id);

        if (isInt)
            expression = x => x.Id == id;
        else if (!string.IsNullOrWhiteSpace(search))
        {
            expression = x => x.Email.Contains(search) || x.FullName.Contains(search);
        }


        var user = await _unitOfWork.Users
            .GetListAsync(expression: expression, cancellationToken: cancellationToken);

        if (user is null) throw new ApplicationException("Kullanıcı Bulunadı");

        var mapUser = _mapper.Map<List<UserResponse>>(user);

        return new SuccessResult<List<UserResponse>>(mapUser);
    }

    public async Task<IResult> Delete(int id)
    {
        var user = await _unitOfWork.Users.GetAsync(x => x.Id == id, ignore: true);

        if (user is null)
            return new ErrorResult(LangKeys.UserNotFound.Localize());

        await _unitOfWork.Users.SoftDelete(user);

        await _unitOfWork.SaveAsync();

        return new SuccessResult();
    }

    public async Task<IResult<AuthResponse>> Update(UpdateUserRequest request)
    {
        var user = await _unitOfWork.Users
            .GetAsync(
                expression: x => x.Id == request.Id,
                ignore: true);

        if (user is null)
            return new ErrorResult<AuthResponse>(LangKeys.UserNotFound.Localize());

        user.FullName = request.FullName ?? user.FullName;
        user.MobilePhones = request.MobilePhones ?? user.MobilePhones;


        var accessToken = _tokenHelper.CreateToken<AccessToken>(user);

        accessToken.Roles = user.Roles()
            .Select(x => ((Roles)Convert.ToInt32(x)).ToString())
            .ToList();

        accessToken.AvatarUrl = user.AvatarUrl!;

        user.RefreshToken = accessToken.RefreshToken;


        _unitOfWork.Users.Update(user);


        await _unitOfWork.SaveAsync();


        var mapUser = _mapper.Map<UserResponse>(user);

        var result = new AuthResponse
        {
            AccessToken = accessToken,
            User = mapUser
        };


        return new SuccessResult<AuthResponse>(result);
    }
}