namespace RTGS.TechTest.Api.Models;

public class MyTransferDto
{
    public MyTransferDto(string debtorAccountIdentifier, string creditorAccountIdentifier, float amount)
    {
        DebtorAccountIdentifier = debtorAccountIdentifier;
        CreditorAccountIdentifier = creditorAccountIdentifier;
        Amount = amount;
    }

    public string DebtorAccountIdentifier { get; set; }
    public string CreditorAccountIdentifier { get; set; }
    public float Amount { get; set; }
}
