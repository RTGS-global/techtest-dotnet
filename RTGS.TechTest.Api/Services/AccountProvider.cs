using RTGS.TechTest.Api.Exceptions;
using RTGS.TechTest.Api.Models;

namespace RTGS.TechTest.Api.Services;

public class AccountProvider : IAccountProvider
{
    private readonly List<Account> _accounts;

    public AccountProvider()
    {
        _accounts = new List<Account> { new Account("account-a"), new Account("account-b") };
    }

    public Account Get(string accountIdentifier)
    {
        var account = _accounts.SingleOrDefault(a => a.Identifier == accountIdentifier);
        return account;
    }

    public void Deposit(string accountIdentifier, float amount)
    {
        var account = _accounts.SingleOrDefault(a => a.Identifier == accountIdentifier);
        if (account == null)
        {
            throw new AccountNotFoundException();
        }
        account.Balance += amount;
    }

    public void Transfer(MyTransferDto transfer)
    {
        AddTransaction(transfer.DebtorAccountIdentifier, -transfer.Amount);
        AddTransaction(transfer.CreditorAccountIdentifier, transfer.Amount);
    }

    private void AddTransaction(string accountIdentifier, float amount)
    {
        var account = _accounts.SingleOrDefault(a => a.Identifier == accountIdentifier);
        account.Balance += amount;
    }
}
