namespace RTGS.TechTest.Api.Models;

public class Account
{
	public Account(string identifier)
	{
		Balance = 0;
		Identifier = identifier;
	}

    public float Balance { get; set; }

	public string Identifier { get; }
}
