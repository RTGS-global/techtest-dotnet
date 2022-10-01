﻿using RTGS.TechTest.Api.Exceptions;
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
        if (amount < 0)
        {
            throw new ValidationException("Deposit amount must be positive");
        }

        var account = _accounts.SingleOrDefault(a => a.Identifier == accountIdentifier);
        if (account == null)
        {
            throw new AccountNotFoundException();
        }
        account.Balance += amount;
    }

    public void Transfer(MyTransferDto transfer)
    {
        if (transfer.DebtorAccountIdentifier == transfer.CreditorAccountIdentifier)
        {
            throw new ValidationException("Debtor and creditor accounts must be different");
        }

        var debtorAccount = _accounts.SingleOrDefault(a => a.Identifier == transfer.DebtorAccountIdentifier);
        var creditorAccount = _accounts.SingleOrDefault(a => a.Identifier == transfer.CreditorAccountIdentifier);

        if (debtorAccount == null || creditorAccount == null)
        {
            throw new AccountNotFoundException();
        }

        debtorAccount.Balance -= transfer.Amount;
        creditorAccount.Balance += transfer.Amount;
    }

}
