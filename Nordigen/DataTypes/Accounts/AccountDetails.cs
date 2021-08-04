namespace Nordigen.DataTypes.Accounts
{
    public class Account
    {
        public string resourceId { get; set; }
        public string iban { get; set; }
        public string currency { get; set; }
        public string ownerName { get; set; }
        public string name { get; set; }
        public string product { get; set; }
        public string cashAccountType { get; set; }
    }

    public class AccountDetails
    {
        public Account account { get; set; }
    }
}