using Microsoft.AspNetCore.Mvc;
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
		_accountProvider.Deposit(accountIdentifier, amount);
		return Ok();
	}

	[HttpPost("transfer", Name = "Transfer")]
	public IActionResult Transfer(MyTransferDto transfer)
	{
		_accountProvider.Transfer(transfer);
		return Accepted();
	}

	[HttpGet("{accountIdentifier}", Name = "GetBalance")]
	public Account Get(string accountIdentifier) => _accountProvider.Get(accountIdentifier);
}