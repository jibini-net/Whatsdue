using Generated;
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
    public async Task<IActionResult> List()
    {
        return Ok(await accounts.List());
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
}