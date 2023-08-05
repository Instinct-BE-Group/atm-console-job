namespace atm_console_job
{
    // Define a class to represent an account holder
    class AccountHolder
    {
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public decimal AccountBalance { get; set; }
        public string? CardNumber { get; set; }
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
                AccountBalance = 20000000,
                CardNumber = "0000900200190000",
                PIN = "2023",
                BVN = "22289012340"
            },
            new AccountHolder
            {
                AccountNumber = "0090020015",
                AccountName = "Akpororo Abdul",
                AccountBalance = 10000,
                CardNumber = "0000900200150000",
                PIN = "2021",
                BVN = "22289018701"
            },
            new AccountHolder
            {
                AccountNumber = "0090020350",
                AccountName = "Chinazam Obi",
                AccountBalance = 61000,
                CardNumber = "0000900203500000",
                PIN = "1234",
                BVN = "22289019550"
            }
        };
    }
}



