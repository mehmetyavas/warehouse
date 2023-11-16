using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Attributes;
using WebAPI.Utilities.Results;
using IResult = WebAPI.Utilities.Results.IResult;

namespace WebAPI.Controllers.Base;

/// <inheritdoc />
[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
[Permission]
public class BaseController : Controller
{
    /// <summary>
    /// Get Result response with Data Parameter
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>returns data type of T with result</returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetResponse<T>(IResult<T> result)
    {
        return result.Success
            ? Ok(result)
            : BadRequest(result);
    }

    /// <summary>
    /// Get Result Response Without  Data
    /// </summary>
    /// <param name="result"></param>
    /// <returns> returns only result</returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetResponse(IResult result)
    {
        return result.Success
            ? Ok(result)
            : BadRequest(result);
    }

    /// <summary>
    /// get Response without result
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>returns data only type of T</returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetResponseOnlyResultData<T>(IResult<T> result)
    {
        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.Message);
    }
}