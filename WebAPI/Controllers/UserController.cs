using Microsoft.AspNetCore.Mvc;
using WebAPI.Attributes;
using WebAPI.Business.User.Request;
using WebAPI.Business.User.Service;
using WebAPI.Controllers.Base;
using WebAPI.Data.Enum;

namespace WebAPI.Controllers;

[BackOffice]
public class UserController : BaseController
{
    private readonly IUserService _userManager;

    public UserController(IUserService userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [ActionKey(ActionName.UserGet)]
    public async Task<IActionResult> List(string? search, CancellationToken cancellationToken)
    {
        var user = await _userManager.List(search, cancellationToken: cancellationToken);
        return GetResponse(user);
    }

    [HttpPut]
    [ActionKey(ActionName.UserUpdate)]
    public async Task<IActionResult> Update(UpdateUserRequest request)
    {
        var result = await _userManager.Update(request);

        return GetResponse(result);
    }


    [HttpDelete("{id}")]
    [ActionKey(ActionName.UserDelete)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userManager.Delete(id);

        return GetResponse(result);
    }
}