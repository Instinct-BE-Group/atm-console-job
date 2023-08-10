namespace atm_console_job
{
    internal class ATMConsoleMain
    {
        static void Main(string[] args)
        {
            Console.Title = "Team Instinct ATM";
            // Welcome screen
            Console.WriteLine("Welcome, please insert your card\n");

            // Customer Input - Card Number and PIN
            string? cardNumber = GetInput("Insert Card: ");
            Console.Clear();

            // Verify the card number and PIN
            AccountHolder? accountHolder = FindAccountHolder(cardNumber);
            Console.WriteLine("Please Wait\n");
            Console.Clear();


            if (accountHolder != null)
            {
                Console.Clear();
                string? pinEntryMode = GetPinEntryMode();

                if (pinEntryMode == "1")
                {
                    HandlePinEntryMode1(cardNumber);
                }
                else if (pinEntryMode == "2")
                {
                    HandlePinEntryMode2(cardNumber);
                }
                else
                {
                    Console.WriteLine("Invalid input. Thank you for using our ATM.");
                }
            }
            else
            {
                Console.WriteLine("Card not found. Thank you for using our ATM.");
            }

        }

        static string? GetPinEntryMode()
        {
            // Display the PIN entry mode options
            Console.WriteLine("Please select your PIN entry mode\n");
            Console.WriteLine("1) Activate your card or Change Pin\t\t\t2) Enter Pin\n");

            // Prompt the user for input and keep asking until a valid option is selected
            while (true)
            {
                string? input = GetInput("");
                if (input == "1" || input == "2")
                {
                    return input;
                }
                Console.WriteLine("Invalid input. Please enter a valid option (1-2).");
            }
        }

        static void HandlePinEntryMode1(string? cardNumber)
        {
            Console.Clear();
            string? pin = GetInput("Please enter your old PIN: \n");
            string? pinOption = GetInput("1) Proceed\t\t\t\t2) Cancel \n");
            Console.Clear();

            if (!string.IsNullOrEmpty(cardNumber) && pinOption == "1" && VerifyCustomer(cardNumber, pin))
            {
                Console.WriteLine($"Welcome {GetAccountName(cardNumber)}");
                ChangePIN(cardNumber);
            }
            else
            {
                Console.WriteLine("Invalid PIN. Thank you for using our ATM.");
            }
        }

        static void HandlePinEntryMode2(string? cardNumber)
        {
            Console.Clear();
            string? pin = GetInput("Please enter your PIN: \n");
            string? pinOption = GetInput("1) Proceed\t\t\t\t2) Cancel \n");
            Console.Clear();

            if (pinOption == "1" && VerifyCustomer(cardNumber, pin))
            {
                Console.WriteLine($"Welcome {GetAccountName(cardNumber)}\n");
                MainOperation(cardNumber);
            }
        }

        static string? GetInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        static bool VerifyCustomer(string? cardNumber, string? pin)
        {
            // Implement customer verification logic here
            foreach (var item in AccountData.accountHolders)
            {
                if (item.CardNumber == cardNumber && item.PIN == pin)
                {
                    // Return true if customer is verified successfully, otherwise false
                    return true;
                }
            }
            return false;
        }

        static AccountHolder? FindAccountHolder(string? cardNumber)
        {
            return AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);
        }

        static string? GetAccountName(string? cardNumber)
        {
            // Implement logic to retrieve account name based on card number
            foreach (var item in AccountData.accountHolders)
            {
                if (item.CardNumber == cardNumber)
                {
                    return item.AccountName;
                }
            }
            return "Account Not Found";
        }

        static string GetAccountType(int value)
        {
            switch (value)
            {
                case 1:
                    return "Current";
                case 2:
                    return "Savings";
                case 3:
                    return "Credit";
                default:
                    return "";
            }
        }

        static void MainOperation(string? cardNumber)
        {
            if
                (string.IsNullOrEmpty(cardNumber))
            {
                Console.Clear();
                // Handle null cardNumber, such as displaying an error message.
                Console.WriteLine("Invalid Card number, cannot proceed.\n");
                return;
            }
            else
            {
                // Main Menu
                bool continueTransaction = true;
                do
                {
                    int option = ShowMainMenu();
                    switch (option)
                    {
                        case 1:
                            OpenAccount(cardNumber);
                            break;
                        case 2:
                            WithdrawCash(cardNumber);
                            break;
                        case 3:
                            ChangePIN(cardNumber);
                            break;
                        case 4:
                            CheckBalance(cardNumber);
                            break;
                        case 5:
                            TransferMoney(cardNumber);
                            break;
                        case 6:
                            QuickTeller(cardNumber);
                            break;
                        case 7:
                            PayArena(cardNumber);
                            break;
                        case 8:
                            continueTransaction = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }

                    //do you want to perform another transaction
                    if (continueTransaction)
                    {
                        Console.WriteLine("Do you want to perform another transaction? (yes/no): \n");
                        string? response = GetInput("");
                        if (response?.Trim().ToLower() != "yes")
                        {
                            continueTransaction = false;
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }

                } while (continueTransaction);
            }
            // End of transaction
            Console.WriteLine("Thank you for using our ATM. Please take your card.\n");
        }

        static int ShowMainMenu()
        {
            // Implement logic to display and get the main menu option chosen by the user
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("1. Open Account \t\t\t2. Withdraw Cash\n");
            Console.WriteLine("3. Change PIN \t\t\t\t4. Check Balance\n");
            Console.WriteLine("5. Transfer Money\t\t\t6. QuickTeller\n");
            Console.WriteLine("7. Pay Arena \t\t\t\t8. More\n");


            while (true)
            {
                string? input = GetInput("Enter your choice (1-8): ");
                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int option) && option >= 1 && option <= 8)
                {
                    Console.Clear();
                    return option;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option (1-8): \n");
                }
            }
        }

        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        static void OpenAccount(string? cardNumber)
        {
            Console.WriteLine("Open Account");
            // Implement logic for the Open Account process
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                Console.WriteLine("Select Account Type\n");
                Console.WriteLine("1. Current\n");
                Console.WriteLine("2. Savings\n");

                string? accountType = GetInput("Enter your choice (1-2): ");
                Console.Clear();

                string? bvn = GetInput("Input your BVN: \n");
                Console.Clear();

                if (bvn == accountHolder.BVN)
                {
                    string? otp = GetInput("Input the OTP sent to your registered phone number: ");
                    Console.Clear();

                    if (!string.IsNullOrEmpty(otp) && otp.Length == 6)
                    {
                        var random = new Random();
                        string accNumber = string.Empty;
                        for (int i = 0; i < 8; i++)
                            accNumber = String.Concat(accNumber, random.Next(8).ToString());

                        Console.WriteLine($"\nCongratulations {accountHolder.AccountName}");
                        Console.WriteLine($"\nYour account has been created your account Number is 00{accNumber}\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("Ooops, error");
            }
        }

        static void WithdrawCash(string? cardNumber)
        {
            Console.WriteLine("Withdraw Cash");
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                // Select Account Type
                Console.WriteLine("Select Account type\n");
                Console.WriteLine("1. Current\n");
                Console.WriteLine("2. Savings\n");
                Console.WriteLine("3. Credit\n");

                string? input = GetInput("Enter your choice (1-3): ");
                Console.Clear();

                if (input != null && int.TryParse(input, out int value) && value >= 1 && value <= 3)
                {
                    string? selectedAccountType = GetAccountType(value);

                    if (selectedAccountType != null && selectedAccountType == accountHolder.AccountType)
                    {
                        // Display available withdrawal amounts
                        Console.WriteLine("Available withdrawal amounts\n");
                        Console.WriteLine("1. N 500\t\t\t\t5. N 10000\n");
                        Console.WriteLine("2. N 1000\t\t\t\t6. N 20000\n");
                        Console.WriteLine("3. N 2000\t\t\t\t7. N 40000\n");
                        Console.WriteLine("4. N 5000\t\t\t\t8. Others\n");

                        string? withdrawalOption = GetInput("Select an option (1-8): ");
                        //Console.Clear();

                        if (int.TryParse(withdrawalOption, out int opt) && opt >= 1 && opt <= 8)
                        {
                            decimal withdrawalAmount;

                            if (opt >= 1 && opt <= 7)
                            {
                                // Map the selected option to the corresponding withdrawal amount
                                decimal[] withdrawalAmounts = { 500, 1000, 2000, 5000, 10000, 20000, 40000 };
                                withdrawalAmount = withdrawalAmounts[opt - 1];

                                if (withdrawalAmount > accountHolder.AccountBalance)
                                {
                                    Console.WriteLine("Insufficient Balance.");
                                }
                                else if (withdrawalAmount < accountHolder.AccountBalance)
                                {
                                    Console.WriteLine($"Withdrawal Amount is: N {withdrawalAmount:N2}");
                                    Console.WriteLine($"Available Balance is: N {accountHolder.AccountBalance:N2}");

                                    // Debit the account and display success message
                                    accountHolder.UpdateBalance(-withdrawalAmount);

                                    Console.WriteLine($"Take your cash: N {withdrawalAmount:N2}");
                                    Console.WriteLine($"Available Balance is: N {accountHolder.AccountBalance:N2}");


                                    Console.Clear();

                                    //Console.WriteLine("Did you know you can recharge your phone on this ATM right now!\n");
                                    //Console.WriteLine("It's easy to use, just select recharge now\n\n");
                                    //Console.WriteLine("1. Recharge Now\n");
                                    //Console.WriteLine("2. Try Later\n");
                                    //string? rechargeOption = GetInput("Select an option (1-2): ");
                                    //if (rechargeOption == "1")
                                    //{
                                    //    // recharge steps
                                    //    Console.WriteLine("Okay");
                                    //}
                                    //else
                                    //{
                                    //    string? anotherTransactionOption = GetInput("Do you want to perform another transaction? (yes/no): ");
                                    //    Console.Clear();

                                    //    if (anotherTransactionOption?.ToLower() == "no")
                                    //    {
                                    //        //Console.WriteLine("Thank you for using our ATM. Please take your card.");
                                    //        return;
                                    //    }
                                    //}
                                }
                                else if (withdrawalAmount <= 0)
                                {
                                    Console.WriteLine("Invalid withdrawal amount.");
                                }
                                else if (accountHolder.AccountBalance - withdrawalAmount < 0)
                                {
                                    Console.WriteLine("Insufficient Balance.");
                                }
                                else
                                {
                                    Console.WriteLine("Error");
                                }
                            }
                            else if (opt == 8) // Others
                            {
                                string? amountInput = GetInput("Enter the withdrawal amount (in multiples of ₦500):\n\n");
                                Console.Clear();

                                if (decimal.TryParse(amountInput, out decimal enteredAmount) && enteredAmount % 500 == 0)
                                {
                                    withdrawalAmount = enteredAmount;

                                    if (withdrawalAmount > accountHolder.AccountBalance)
                                    {
                                        Console.WriteLine("Insufficient Balance.");
                                    }
                                    else if (withdrawalAmount <= 0)
                                    {
                                        Console.WriteLine("Invalid withdrawal amount.");
                                    }
                                    else if (accountHolder.AccountBalance - withdrawalAmount < 0)
                                    {
                                        Console.WriteLine("Insufficient Balance.");
                                    }
                                    else
                                    {
                                        // Debit the account and display success message
                                        accountHolder.UpdateBalance(-withdrawalAmount);

                                        Console.WriteLine($"Take your cash: N {withdrawalAmount:N2}");
                                        Console.Clear();

                                        Console.WriteLine("Did you know you can recharge your phone on this ATM right now!\n");
                                        Console.WriteLine("It's easy to use, just select recharge now\n\n");
                                        Console.WriteLine("1. Recharge Now\n");
                                        Console.WriteLine("2. Try Later\n");
                                        string? rechargeOption = GetInput("Select an option (1-2): ");
                                        if (rechargeOption == "1")
                                        {
                                            // recharge steps
                                            Console.WriteLine("Okay");
                                        }
                                        else
                                        {
                                            string? anotherTransactionOption = GetInput("Do you want to perform another transaction? (yes/no): ");
                                            Console.Clear();

                                            if (anotherTransactionOption?.ToLower() == "no")
                                            {
                                                //Console.WriteLine("Thank you for using our ATM. Please take your card.");
                                                return;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid amount. Please enter a valid amount in multiples of ₦500.");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please select a valid option (1-8).");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Wrong account type");
                    }
                }
            }
            else
            {
                Console.WriteLine("Issuer error.");
            }
        }

        static void ChangePIN(string cardNumber)
        {
            Console.WriteLine("Change Pin");
            // Find the account holder based on the card number
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                string? oldPIN = GetInput("Enter your old PIN: ");
                Console.Clear();
                if (oldPIN == accountHolder.PIN)
                {
                    string? newPIN = GetInput("Enter your new PIN: ");
                    Console.Clear();
                    string? confirmNewPIN = GetInput("Confirm your new PIN: ");
                    Console.Clear();

                    if (newPIN == confirmNewPIN)
                    {
                        // Update the PIN
                        accountHolder.PIN = newPIN;
                        Console.WriteLine("PIN successfully changed.");
                    }
                    else
                    {
                        Console.WriteLine("PINs do not match. PIN change failed.");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect old PIN. PIN change failed.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        static void CheckBalance(string cardNumber)
        {
            Console.WriteLine("Check Balance");
            // Find the account holder based on the card number
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            // Including Account type selection and displaying account name and balance
            if (accountHolder != null)
            {
                // Select Account Type
                Console.WriteLine("Select Account type\n");
                Console.WriteLine("1. Current\n");
                Console.WriteLine("2. Savings\n");
                Console.WriteLine("3. Credit\n");

                string? input = GetInput("Enter your choice (1-3): ");
                Console.Clear();

                if (input != null && int.TryParse(input, out int value) && value >= 1 && value <= 3)
                {
                    string? selectedAccountType = GetAccountType(value);

                    if (selectedAccountType != null && selectedAccountType == accountHolder.AccountType)
                    {
                        Console.WriteLine($"Account Name: {accountHolder.AccountName}");
                        Console.WriteLine($"Account Balance: N {accountHolder.AccountBalance:N2}");
                    }
                    else
                    {
                        Console.WriteLine($"Wrong Account type");
                    }
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        static void TransferMoney(string? cardNumber)
        {
            Console.WriteLine("Transfer Money");
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                // Select Account Type
                Console.WriteLine("Select Account type\n");
                Console.WriteLine("1. CURRENT");
                Console.WriteLine("2. SAVINGS");
                Console.WriteLine("3. CREDIT");

                string? input = GetInput("Enter your choice (1-3): ");
                Console.Clear();

                if (input != null && int.TryParse(input, out int value) && value >= 1 && value <= 3)
                {

                    string? amount = GetInput("Enter amount: ");
                    Console.Clear();
                    string? beneficiaryaccountnumber = GetInput("Enter beneficiary account number: ");
                    Console.Clear();
                    if (!string.IsNullOrEmpty(amount) && !string.IsNullOrEmpty(beneficiaryaccountnumber) && beneficiaryaccountnumber.Length == 10)
                    {
                        Console.WriteLine("Select bank type\n");
                        Console.WriteLine("1. Access Diamond");
                        Console.WriteLine("2. Key Stone Bank");
                        Console.WriteLine("3. United Bank Of Africa");
                        Console.WriteLine("4. Fidelity Bank");
                        Console.WriteLine("5. First Bank");
                        Console.WriteLine("6. Sterling Bank");
                        Console.WriteLine("7. Polaris Bank");
                        Console.WriteLine("8. Providous Bank");
                        Console.WriteLine("9. Standard Trust");
                        Console.WriteLine("10. Zenith Bank");
                        Console.WriteLine("11. Guaranty Trust Bank");

                        string? banks = Console.ReadLine();
                        Console.Clear();
                        if (banks != null && int.TryParse(banks, out int options) && options >= 1 && options <= 11)
                        {
                            int.TryParse(amount, out int transferamount);
                            decimal balance = accountHolder.AccountBalance;
                            decimal balanceleft = balance - transferamount;

                            Console.WriteLine("Transaction successful!");
                            Console.WriteLine($"Your new balance is {balanceleft}");
                        }
                        else
                        {
                            Console.WriteLine("Failed Transaction!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Wrong account type");
                }
            }
        }
        static void QuickTeller(string? cardNumber)
        {
            Console.WriteLine("QuickTeller");
            // Find the account holder based on the card number
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                //Display Options
                Console.WriteLine("PLEASE SELECT OPTION");
                Console.WriteLine("1. AIRTIME RECHARGE");
                Console.WriteLine("2. PAYBILLS");
                Console.WriteLine("3. SEND MONEY");
                Console.WriteLine("4. SAFETOKEN");
                Console.WriteLine("5. MAKE A PAYMENT");
                Console.WriteLine("6. RECEIVE MONEY");
                Console.WriteLine("7. BUY TICKET");
                Console.WriteLine("8. MORE");

                string[] quickteller = { "NONE", "1. AIRTIME RECHARGE", "2. PAYBILLS", "3. SEND MONEY", "4. SAFETOKEN", "5. MAKE A PAYMENT", "6. RECEIVE MONEY", "7. BUY TICKET", "8. MORE" };
                // convert the string input to int and use tryparse to handle errors
                int input = Convert.ToInt32(Console.ReadLine());
                int output;

                //SELECT MENU OPTION
                for (int i = 0; i < quickteller.Length; i++)
                {
                    //Console.WriteLine(item);
                    if (input == i)
                    {
                        output = i;
                        //print result
                        Console.Clear();
                        Console.WriteLine(quickteller[output]);
                    }
                }
                //paybills option chosen

                //select option for account type
                Console.WriteLine("Please select your account type");
                Console.WriteLine("1. CURRENT");
                Console.WriteLine("2. SAVINGS");
                Console.WriteLine("3. CREDIT");
                int selcetCardType = Convert.ToInt32(Console.ReadLine());

                //select a payment option
                Console.Clear();
                Console.WriteLine("Please select a payment option");
                Console.WriteLine("1. ELECTRICITY");
                Console.WriteLine("2. CABLE TV");
                Console.WriteLine("3. LCC TV");
                Console.WriteLine("4. MOBILE POSTPAID");
                Console.WriteLine("5. PAY A MERCHANT");
                Console.WriteLine("6. AIRLINES");
                Console.WriteLine("7. DIESEL PURCHASE");
                Console.WriteLine("8. MORE");
                int paymentOption = Convert.ToInt32(Console.ReadLine());

                //select the package you wish to pay for
                Console.Clear();
                Console.WriteLine("Please select a payment option");
                Console.WriteLine("1. POSTPAID");
                Console.WriteLine("2. PREPAID");

                //select your power distribution company
                int prepaid = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Please select your power distribution company");
                Console.WriteLine("1. EKO");
                Console.WriteLine("2. ENUGU");
                Console.WriteLine("3. IKEJA");
                Console.WriteLine("4. BENIN");
                Console.WriteLine("5. IBADAN");
                Console.WriteLine("6. PORTHARCOURT");
                Console.WriteLine("7. KANO");
                Console.WriteLine("8. NEXT");

                //define user input for distCo
                int distCo = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                //define the amount to debit
                Console.WriteLine("Please enter the amount for the prepaid unit you want to buy");

                int amount = Convert.ToInt32(Console.ReadLine());
                if (amount > accountHolder.AccountBalance)
                {
                    Console.WriteLine("Insufficient funds");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{amount}");
                    Console.Clear();
                    Console.WriteLine("Please enter your meter number");
                    //Input your meter number
                    string? meterNumber = Console.ReadLine();
                    if (meterNumber != null && meterNumber.Length == 14 && int.TryParse(meterNumber, out int meterNo))
                    {
                        Console.WriteLine("Unit purchased successfully");
                    }
                    else
                    {
                        Console.WriteLine("input a valid meter number");
                    }
                }
            }
            else
            {
                Console.WriteLine("Issuer switch");
            }
        }

        static void PayArena(string? cardNumber)
        {
            Console.WriteLine("PayArena");
            // Find the account holder based on the card number
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                Console.WriteLine("\n---------Pay Arena---------\n");
                // Implement logic for the Pay Bills process
                // Including card type selection, inputting amount, bill type selection, validation, etc.
                //Display Options
                Console.WriteLine("PLEASE SELECT OPTION");
                Console.WriteLine("1. AIRTIME RECHARGE");
                Console.WriteLine("2. BILL PAYMENT");
                Console.WriteLine("3. MONEY TRANSFER");
                string[] payarena = { "NONE", "1. AIRTIME RECHARGE", "2. BILL PAYMENT", "3. MONEY TRANSFER" };
                // convert the string input to int and use tryparse to handle errors
                int input = Convert.ToInt32(Console.ReadLine());
                int output;
                //SELECT MENU OPTION
                for (int b = 0; b < payarena.Length; b++)
                {
                    //Console.WriteLine(item);
                    if (input == b)
                    {

                        output = b;
                        //print result
                        Console.Clear();
                        Console.WriteLine(payarena[output]);
                    }
                }
                //airtime rech option chosen
                //select option for account type
                Console.WriteLine("Please select your account type");
                Console.WriteLine("1. CURRENT");
                Console.WriteLine("2. SAVINGS");
                Console.WriteLine("3. CREDIT");
                int selcetCardType = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please select your account type");
                Console.WriteLine("1. CURRENT");
                Console.WriteLine("2. SAVINGS");
                Console.WriteLine("3. CREDIT");

                //select an option
                Console.Clear();
                Console.WriteLine("Please select an option");
                Console.WriteLine("1. AIRTEL");
                Console.WriteLine("2. MTN");
                Console.WriteLine("3. GLO");
                Console.WriteLine("4. ETISALAT");
                Console.WriteLine("5. SMILE");
                int selectOption = Convert.ToInt32(Console.ReadLine());

                //select airtime amount
                Console.Clear();
                Console.WriteLine("Please select the airtime amount");
                Console.WriteLine("1. N 100");
                Console.WriteLine("2. N 200");
                Console.WriteLine("3. N 500");
                Console.WriteLine("4. N 1000");
                Console.WriteLine("5. N 1500");
                Console.WriteLine("6. OTHER");

                int airtimeamount = Convert.ToInt32(Console.ReadLine());
                if (airtimeamount > accountHolder.AccountBalance)
                {
                    Console.WriteLine("             ");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{airtimeamount}");
                    Console.Clear();
                    string[] airtime = { "NONE", "1. N 100", "2. N 200", "3. N 500", "4. N 1000", "5. N 1500", "6. OTHER" };
                    Console.WriteLine("Please enter the phone number you wish to recharge");
                    //Input your phone number
                    string? phoneNumber = Console.ReadLine();
                    if (phoneNumber != null && phoneNumber.Length == 11 && int.TryParse(phoneNumber, out int phoneNo))
                    {
                        Console.WriteLine("Airtime recharged successfully");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient fund");
                    }
                }
            }
        }
    }
}
