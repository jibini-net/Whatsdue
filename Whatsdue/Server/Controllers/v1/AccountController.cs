using Generated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Whatsdue.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class AccountController : ControllerBase
{
    private readonly Account.IService accounts;
    public AccountController(Account.IService accounts)
    {
        this.accounts = accounts;
    }

    public record ListParams (
        int page,
        int count);

    [HttpPost("List")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces(typeof(List<Account>))]
    public async Task<IActionResult> List([FromBody] ListParams pars)
    {
        var list = await accounts.List(pars.page, pars.count);
        return Ok(list);
    }

    public record GetParams (
        int id);

    [HttpPost("Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(typeof(Account))]
    public async Task<IActionResult> Get([FromBody] GetParams pars)
    {
        var account = await accounts.Get(pars.id);
        return account is null
            ? NotFound()
            : Ok(account);
    }

    public record AttemptLoginParams (
        string email,
        string password);

    [HttpPost("AttemptLogin")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(typeof(Account))]
    public async Task<IActionResult> AttemptLogin([FromBody] AttemptLoginParams pars)
    {
        var account = await accounts.AttemptLogin(pars.email, pars.password);
        return account is null
            ? Unauthorized()
            : Ok(account);
    }
    /*
    public record SignUpParams(
        string email,
        string firstName,
        string lastName);

    [HttpPost("SignUp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status4...)]
    public async Task<IActionResult> SignUp([FromBody] SignUpParams pars)
    {
        var list = await accounts.SignUp(...);
        return Ok(list);
    }
    */
    public record BeginResetParams (
        string email);

    [HttpPost("BeginReset")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> BeginReset([FromBody] BeginResetParams pars)
    {
        await accounts.BeginReset(pars.email);
        return Ok();
    }

    public record GetResetDetailsParams (
        string resetToken);

    [HttpPost("GetResetDetails")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(typeof(Account))]
    public async Task<IActionResult> GetResetDetails([FromBody] GetResetDetailsParams pars)
    {
        var account = await accounts.GetResetDetails(pars.resetToken);
        return account is null
            ? Unauthorized()
            : Ok(account);
    }

    public record ResetPasswordParams(
        string resetToken,
        string password);

    [HttpPost("ResetPassword")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordParams pars)
    {
        await accounts.ResetPassword(pars.resetToken, pars.password);
        return Ok();
    }
}