using RTGS.TechTest.Api.Models;

namespace RTGS.TechTest.Api.Services;

public interface IAccountProvider
{
    Account Get(string accountIdentifier);

    /// <summary>
    /// Deposit money into an account
    /// </summary>
    /// <param name="accountIdentifier"></param>
    /// <param name="amount"></param>
    /// <exception cref="RTGS.TechTest.Api.Exceptions.AccountNotFoundException">thrown when account with <paramref name="accountIdentifier"/>not found</exception>
    void Deposit(string accountIdentifier, float amount);

    void Transfer(MyTransferDto transfer);
}
