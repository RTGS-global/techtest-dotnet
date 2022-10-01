using RTGS.TechTest.Api.Models;

namespace RTGS.TechTest.Api.Services;

public class AccountProvider : IAccountProvider
{
    private readonly IDictionary<string, Account> _accounts;

    public AccountProvider()
    {
        _accounts = new Dictionary<string, Account> { { "account-a", new Account() }, { "account-b", new Account() } };
    }

    public Account Get(string accountIdentifier) => _accounts[accountIdentifier];

    public void Deposit(string accountIdentifier, float amount) => AddTransaction(accountIdentifier, amount);

    public void Transfer(MyTransferDto transfer)
    {
        AddTransaction(transfer.DebtorAccountIdentifier, -transfer.Amount);
        AddTransaction(transfer.CreditorAccountIdentifier, transfer.Amount);
    }

    private void AddTransaction(string accountIdentifier, float amount)
    {
        var account = _accounts[accountIdentifier];
        account.Balance += amount;
    }
}
