using Microsoft.AspNetCore.Mvc;
using RTGS.TechTest.Api.Exceptions;
using RTGS.TechTest.Api.Models;
using RTGS.TechTest.Api.Services;

namespace RTGS.TechTest.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
	private readonly IAccountProvider _accountProvider;

	public AccountController(IAccountProvider accountProvider)
	{
		_accountProvider = accountProvider;
	}

	[HttpPost("{accountIdentifier}", Name = "Deposit")]
	public IActionResult Deposit(string accountIdentifier, [FromBody]float amount)
	{
		try
		{
            _accountProvider.Deposit(accountIdentifier, amount);
            return Ok();
        }
		catch (AccountNotFoundException)
		{
			return NotFound();
		}		
	}

	[HttpPost("transfer", Name = "Transfer")]
	public IActionResult Transfer(MyTransferDto transfer)
	{
        try
        {
            _accountProvider.Transfer(transfer);
            return Accepted();
        }
        catch (AccountNotFoundException)
        {
            return NotFound();
        }
	}

	[HttpGet("{accountIdentifier}", Name = "GetBalance")]
	public IActionResult Get(string accountIdentifier)
	{
		var account = _accountProvider.Get(accountIdentifier);
		return account != null ? Ok(account) : NotFound();
	}
}
