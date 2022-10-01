using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RTGS.TechTest.Api.Controllers;
using RTGS.TechTest.Api.Models;
using RTGS.TechTest.Api.Services;
using Xunit;

namespace RTGS.TechTest.Test;

public class BankAccountTests : IClassFixture<WebApplicationFactory<Program>>
{
	private readonly HttpClient _client;

	public BankAccountTests(WebApplicationFactory<Program> fixture)
	{
		_client = fixture
			.WithWebHostBuilder(builder => builder.ConfigureServices(services =>
			{
				services.AddSingleton<IAccountProvider, AccountProvider>();
			}))
			.CreateDefaultClient();
	}

	[Fact]
	public async Task GivenAccountExistsWithNoTransactions_ThenGetBalanceShouldReturnZero()
	{
		var result = await _client.GetFromJsonAsync<Account>("/account/account-a");

		Assert.Equal(0, result.Balance);
	}

	[Fact]
	public async Task GivenAccountExists_WhenTransactionIsAdded_ThenGetBalanceShouldReturnExpected()
	{
		await _client.PostAsJsonAsync("/account/account-a", "1000");
		var result = await _client.GetFromJsonAsync<Account>("/account/account-a");

		Assert.Equal(1000, result.Balance);
	}

	[Fact]
	public async Task GivenAccountExists_WhenMultipleDepositsAreAdded_ThenGetBalanceShouldReturnExpected()
	{
		await _client.PostAsJsonAsync("/account/account-a", "1000");
		await _client.PostAsJsonAsync("/account/account-a", "2000");
		var result = await _client.GetFromJsonAsync<Account>("/account/account-a");

		Assert.Equal(3000, result.Balance);
	}

	[Fact]
	public async Task GivenAccountExists_WhenTransferIsMade_ThenGetBalanceShouldReturnExpected()
	{
		await _client.PostAsJsonAsync("/account/transfer", new MyTransferDto("account-a", "account-b", 1000));
		var accountA = await _client.GetFromJsonAsync<Account>("/account/account-a");
		var accountB = await _client.GetFromJsonAsync<Account>("/account/account-b");

		Assert.Equal(-1000, accountA.Balance);
		Assert.Equal(1000, accountB.Balance);
	}

	[Fact]
	public async Task GivenAccountDoesntExist_WhenGetAccount_ThenReturn404()
	{
		var result = await _client.GetAsync("/account/account-c");

		Assert.NotNull(result);
		Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
	}

	[Fact]
	public async Task GivenDebtorAccountDoesntExist_WhenTransferIsMade_ThenReturn404()
	{
		var result = await _client.PostAsJsonAsync("/account/transfer", new MyTransferDto("account-c", "account-b", 1000));

		Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
	}

    [Fact]
    public async Task GivenCreditorAccountDoesntExist_WhenTransferIsMade_ThenReturn404()
    {
        var result = await _client.PostAsJsonAsync("/account/transfer", new MyTransferDto("account-a", "account-c", 1000));

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task GivenAccountDoesntExist_WhenDepositIsMade_ThenReturn404()
    {
        var result = await _client.PostAsJsonAsync("/account/account-c", "1000");

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
