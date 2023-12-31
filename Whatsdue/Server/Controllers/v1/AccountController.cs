using Generated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whatsdue.Server.Services;

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

    [HttpPost("List")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces(typeof(List<Account>))]
    public async Task<IActionResult> List(
        int page,
        int count)
    {
        var list = await accounts.List(page, count);
        return Ok(list);
    }

    [HttpPost("Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(typeof(Account))]
    public async Task<IActionResult> Get(
        int id)
    {
        var account = await accounts.Get(id);
        return account is null
            ? NotFound()
            : Ok(account);
    }

    [HttpPost("AttemptLogin")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(typeof(Account))]
    public async Task<IActionResult> AttemptLogin(
        string email,
        string password)
    {
        var account = await accounts.AttemptLogin(email, password);
        return account is null
            ? Unauthorized()
            : Ok(account);
    }

    /*
    [HttpPost("SignUp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status4...)]
    public async Task<IActionResult> SignUp(
        string email,
        string firstName,
        string lastName,
        ...)
    {
        var list = await accounts.SignUp(...);
        return Ok(list);
    }
    */

    [HttpPost("BeginReset")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> BeginReset(
        string email)
    {
        await accounts.BeginReset(email);
        return Ok();
    }

    public class TestNestedList
    {
        public string Name2 { get; set; } = "";
        public JsonList<Account> Accounts2 { get; set; } = new();
    }

    [HttpPost("TestThing")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> TestThing(
        JsonList<Account> accounts,
        JsonList<string> list2,
        JsonList<int> list3,
        JsonList<decimal> list4,
        TestNestedList nested)
    {
        await Task.CompletedTask;

        return Ok(new
        {
            accounts,
            list2,
            list3,
            list4,
            nested
        });
    }

    [HttpPost("GetResetDetails")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(typeof(Account))]
    public async Task<IActionResult> GetResetDetails(
        string resetToken)
    {
        var account = await accounts.GetResetDetails(resetToken);
        return account is null
            ? Unauthorized()
            : Ok(account);
    }

    [HttpPost("ResetPassword")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ResetPassword(
        string resetToken,
        string password)
    {
        await accounts.ResetPassword(resetToken, password);
        return Ok();
    }
}