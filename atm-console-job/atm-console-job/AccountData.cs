namespace atm_console_job
{
    // Define a class to represent an account holder
    class AccountHolder
    {
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public decimal AccountBalance { get; set; }
        public string? CardNumber { get; set; }
        public string? AccountType { get; set; }
        public string? PIN { get; set; }
        public string? BVN { get; set; }
    }

    // Create a list to store the account holders' details
    static class AccountData
    {

        public static List<AccountHolder> accountHolders = new List<AccountHolder>
        {
            new AccountHolder
            {
                AccountNumber = "0090020019",
                AccountName = "Tunde Mufasa",
                AccountBalance = 200000,
                CardNumber = "0000900200190000",
                AccountType =  "Current",
                PIN = "1111",
                BVN = "22289012340"
            },
        };
    }
}



