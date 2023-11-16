using WebAPI.Business.Auth.Response;
using WebAPI.Business.User.Request;
using WebAPI.Business.User.Response;
using WebAPI.Utilities.Results;
using IResult = WebAPI.Utilities.Results.IResult;

namespace WebAPI.Business.User.Service;

public interface IUserService
{
    public Task<IResult<List<UserResponse>>> List(string? search,
        CancellationToken cancellationToken = default);

    public Task<IResult> Delete(int id);

    public Task<IResult<AuthResponse>> Update(UpdateUserRequest request);
}