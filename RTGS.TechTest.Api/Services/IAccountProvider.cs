using RTGS.TechTest.Api.Models;

namespace RTGS.TechTest.Api.Services;

public interface IAccountProvider
{
    MyBalance GetBalance(string accountIdentifier);
    void Deposit(string accountIdentifier, float amount);
    void Transfer(MyTransferDto transfer);
}
