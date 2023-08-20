using Generated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Whatsdue.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly Account.IService accounts;
    public AccountController(Account.IService accounts)
    {
        this.accounts = accounts;
    }

    [Route("List")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> List(int page, int count)
    {
        var list = await accounts.List(page, count);
        return Ok(list);
    }

    [Route("Get/{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int Id)
    {
        var account = await accounts.Get(Id);
        return account is null
            ? NotFound()
            : Ok(account);
    }

    [Route("AttemptLogin")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AttemptLogin(string email, string password)
    {
        var account = await accounts.AttemptLogin(email, password);
        return account is null
            ? Unauthorized()
            : Ok(account);
    }

    [Route("BeginReset")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> BeginReset(string email)
    {
        await accounts.BeginReset(email);
        return Ok();
    }

    [Route("GetResetDetails")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetResetDetails(string resetToken, string password)
    {
        var account = await accounts.GetResetDetails(resetToken);
        return account is null
            ? Unauthorized()
            : Ok(account);
    }

    [Route("ResetPassword")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ResetPassword(string resetToken, string password)
    {
        await accounts.ResetPassword(resetToken, password);
        return Ok();
    }
}