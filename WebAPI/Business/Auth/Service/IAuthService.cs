using WebAPI.Business.Auth.Request;
using WebAPI.Business.Auth.Response;
using WebAPI.Utilities.Results;
using IResult = WebAPI.Utilities.Results.IResult;

namespace WebAPI.Business.Auth.Service;

public interface IAuthService
{
    public Task<IResult> Login(LoginRequest login);
    public Task<IResult<AuthResponse>> LoginVerify(LoginVerifyRequest request);

    public Task<IResult> Register(RegisterRequest request);
    public Task<IResult> Verify(VerifyRequest request);
    public Task<IResult<AuthResponse>> LoginWithRefreshToken(LoginWithToken request);
}