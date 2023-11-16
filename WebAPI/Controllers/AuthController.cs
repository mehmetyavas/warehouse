using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Business.Auth.Request;
using WebAPI.Business.Auth.Service;
using WebAPI.Controllers.Base;
using WebAPI.Utilities.Results;
using WebAPI.Utilities.Security.Jwt;

namespace WebAPI.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    /// <summary>
    /// </summary>
    /// <returns> </returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        var result = await _auth.Login(login);

        return GetResponse(result);
    }

    /// <summary>
    /// Token for Authentication
    /// </summary>
    /// <returns> </returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult<AccessToken>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    [HttpPost]
    public async Task<IActionResult> LoginVerify([FromBody] LoginVerifyRequest request)
    {
        var result = await _auth.LoginVerify(request);

        return GetResponse(result);
    }

    /// <summary>
    /// Register the System
    /// </summary>
    /// <returns></returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest register)
    {
        var result = await _auth.Register(register);

        return GetResponse(result);
    }

    /// <summary>
    /// Verify The User
    /// </summary>
    /// <returns> </returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    [HttpPost]
    public async Task<IActionResult> RegisterVerify([FromBody] VerifyRequest request)
    {
        var result = await _auth.Verify(request);

        return GetResponse(result);
    }

    /// <summary>
    /// Authenticate From RefreshToken
    /// </summary>
    /// <returns> </returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult<AccessToken>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    [HttpPost]
    public async Task<IActionResult> LoginWithRefreshToken(LoginWithToken request)
    {
        var result = await _auth.LoginWithRefreshToken(request);

        return GetResponse(result);
    }
}