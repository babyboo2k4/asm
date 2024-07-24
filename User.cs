namespace SpringHeroBank;

public class User
{
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public decimal Balance { get; set; }
    public bool IsAdmin { get; set; }

    public User(string name, string accountNumber, string phoneNumber, string password, bool isAdmin = false)
    {
        Name = name;
        AccountNumber = accountNumber;
        PhoneNumber = phoneNumber;
        Password = password;
        Balance = 0;
        IsAdmin = isAdmin;
    }
}
