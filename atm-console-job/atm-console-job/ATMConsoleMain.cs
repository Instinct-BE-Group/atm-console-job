namespace atm_console_job
{
    internal class ATMConsoleMain
    {
        static void Main(string[] args)
        {
            Console.Title = "Team Instinct";
            // Welcome screen
            DisplayMessage("Welcome to Sterling Bank\n");

            // Customer Input - Card Number and PIN
            string? cardNumber = GetInput("Enter your Card Number: ");
            Console.Clear();

            // Verify the card number and PIN
            AccountHolder? accountHolder = FindAccountHolder(cardNumber);

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
                    DisplayMessage("Invalid input. Thank you for using our ATM.");
                }
            }
            else
            {
                DisplayMessage("Card not found. Thank you for using our ATM.");
            }

        }

        static string? GetPinEntryMode()
        {
            // Display the PIN entry mode options
            DisplayMessage("Please Select your PIN entry mode:");
            DisplayMessage("1) Activate your card or Change Pin");
            DisplayMessage("2) Enter Pin");

            // Prompt the user for input and keep asking until a valid option is selected
            while (true)
            {
                string? input = GetInput(" ");
                if (input == "1" || input == "2")
                {
                    return input;
                }
                DisplayMessage("Invalid input. Please enter a valid option (1-2).");
            }
        }

        static void HandlePinEntryMode1(string? cardNumber)
        {
            string? pin = GetInput("Please enter your old PIN: ");
            string? pinOption = GetInput("1) Proceed\t\t\t 2) Cancel \n");
            Console.Clear();

            if (!string.IsNullOrEmpty(cardNumber) && pinOption == "1" && VerifyCustomer(cardNumber, pin))
            {
                DisplayMessage($"Welcome {GetAccountName(cardNumber)}");
                ChangePIN(cardNumber);
            }
            else
            {
                DisplayMessage("Invalid PIN. Thank you for using our ATM.");
            }
        }

        static void HandlePinEntryMode2(string? cardNumber)
        {
            string? pin = GetInput("Please enter your PIN: ");
            string? pinOption = GetInput("1) Proceed\t\t\t 2) Cancel \n");

            if (pinOption == "1" && VerifyCustomer(cardNumber, pin))
            {
                DisplayMessage($"Welcome {GetAccountName(cardNumber)}\n");
                MainOperation(cardNumber);
            }
        }

        static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
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
                DisplayMessage("Invalid Card number, cannot proceed.");
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
                            DisplayMessage("Invalid option. Please try again.");
                            break;
                    }

                    //do you want to perform another transaction
                    if (continueTransaction)
                    {
                        DisplayMessage("Do you want to perform another transaction? (yes/no): ");
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
            DisplayMessage("Thank you for using our ATM. Please take your card.");
        }

        static int ShowMainMenu()
        {
            // Implement logic to display and get the main menu option chosen by the user
            DisplayMessage("What would you like to do?");
            DisplayMessage("1. Open Account");
            DisplayMessage("2. Withdraw Cash");
            DisplayMessage("3. Change PIN");
            DisplayMessage("4. Check Balance");
            DisplayMessage("5. Transfer Money");
            DisplayMessage("6. QuickTeller");
            DisplayMessage("7. Pay Arena");
            DisplayMessage("8. More");

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
                    DisplayMessage("Invalid input. Please enter a valid option (1-8).");
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
            DisplayMessage("Open Account");
            // Implement logic for the Open Account process
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                DisplayMessage("Select Account Type");
                DisplayMessage("1. Current");
                DisplayMessage("2. Savings");

                string? accountType = GetInput("Enter your choice (1-2): ");

                string? bvn = GetInput("Input your BVN: ");

                if (bvn == accountHolder.BVN)
                {
                    string? otp = GetInput("Input the OTP sent to your registered phone number: ");

                    if (!string.IsNullOrEmpty(otp) && otp.Length == 6)
                    {

                        var random = new Random();
                        string accNumber = string.Empty;
                        for (int i = 0; i < 8; i++)
                            accNumber = String.Concat(accNumber, random.Next(8).ToString());
                        // return s;

                        DisplayMessage($"\nCongratulations {accountHolder.AccountName}");
                        DisplayMessage($"\nYour account has been created your account Number is 00{accNumber}\n");
                    }
                }
            }
            else
            {
                DisplayMessage("No Account Found");
            }
            // Including BVN verification, OTP verification, etc.
        }

        static void WithdrawCash(string? cardNumber)
        {
            DisplayMessage("Withdraw Cash");
            // Implement logic for the Withdraw Cash process
            // Including Account type selection, amount selection, and cross-selling options
            // Find the account holder based on the card number
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                // Select Account Type
                DisplayMessage("Select Account type\n");
                DisplayMessage("1. Current\n");
                DisplayMessage("2. Savings\n");
                DisplayMessage("3. Credit\n");

                string? input = GetInput("Enter your choice (1-3): ");

                if (input != null && int.TryParse(input, out int value) && value >= 1 && value <= 3)
                {
                    string? selectedAccountType = GetAccountType(value);

                    if (selectedAccountType != null && selectedAccountType == accountHolder.AccountType)
                    {
                        // Display available withdrawal amounts
                        DisplayMessage("Available withdrawal amounts:");
                        DisplayMessage("1. ₦500");
                        DisplayMessage("2. ₦1000");
                        DisplayMessage("3. ₦2000");
                        DisplayMessage("4. ₦5000");
                        DisplayMessage("5. ₦10000");
                        DisplayMessage("6. ₦20000");
                        DisplayMessage("7. ₦40000");
                        DisplayMessage("8. Others");

                        string? withdrawalOption = GetInput("Select an option (1-8): ");

                        if (int.TryParse(withdrawalOption, out int option) && option >= 1 && option <= 8)
                        {
                            decimal withdrawalAmount = 0;
                            if (option < 8)
                            {
                                switch (option)
                                {
                                    case 1: withdrawalAmount = 500; break;
                                    case 2: withdrawalAmount = 1000; break;
                                    case 3: withdrawalAmount = 2000; break;
                                    case 4: withdrawalAmount = 5000; break;
                                    case 5: withdrawalAmount = 10000; break;
                                    case 6: withdrawalAmount = 20000; break;
                                    case 7: withdrawalAmount = 40000; break;
                                }

                                if (withdrawalAmount > accountHolder.AccountBalance)
                                {
                                    DisplayMessage("Insufficient Balance.");
                                }
                                else
                                {
                                    DisplayMessage($"Take your cash: ₦{withdrawalAmount:N2}");

                                    string? rechargeOption = GetInput("Do you want to recharge your phone? (yes/no): ");
                                    if (rechargeOption?.ToLower() == "yes")
                                    {
                                        DisplayMessage("Okay");
                                    }
                                    else
                                    {
                                        string? anotherTransactionOption = GetInput("Do you want to perform another transaction? (yes/no): ");
                                        if (anotherTransactionOption?.ToLower() == "no")
                                        {
                                            DisplayMessage("Thank you for using our ATM. Please take your card.");
                                            return;
                                        }
                                    }
                                }
                            }
                            else // Others
                            {
                                decimal enteredAmount = 0;
                                string? amountInput = GetInput("Enter the withdrawal amount (in multiples of ₦500): ");

                                if (decimal.TryParse(amountInput, out enteredAmount) && enteredAmount % 500 == 0)
                                {
                                    if (enteredAmount > accountHolder.AccountBalance)
                                    {
                                        DisplayMessage("Insufficient Balance.");
                                    }
                                    else
                                    {
                                        DisplayMessage($"Take your cash: ₦{enteredAmount:N2}");

                                        string? rechargeOption = GetInput("Do you want to recharge your phone? (yes/no): ");
                                        if (rechargeOption?.ToLower() == "yes")
                                        {
                                            DisplayMessage("Okay");
                                        }
                                        else
                                        {
                                            string? anotherTransactionOption = GetInput("Do you want to perform another transaction? (yes/no): ");
                                            if (anotherTransactionOption?.ToLower() == "no")
                                            {
                                                DisplayMessage("Thank you for using our ATM. Please take your card.");
                                                return;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DisplayMessage("Invalid amount. Please enter a valid amount in multiples of ₦500.");
                                }
                            }
                        }
                        else
                        {
                            DisplayMessage("Invalid option. Please select a valid option (1-8).");
                        }
                    }
                    else
                    {
                        DisplayMessage($"Wrong account type");
                    }
                }
            }
            else
            {
                DisplayMessage("Issuer error.");
            }
        }

        static void ChangePIN(string cardNumber)
        {
            DisplayMessage("Change Pin");
            // Find the account holder based on the card number
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                string? oldPIN = GetInput("Enter your old PIN: ");
                if (oldPIN == accountHolder.PIN)
                {
                    string? newPIN = GetInput("Enter your new PIN: ");
                    string? confirmNewPIN = GetInput("Confirm your new PIN: ");

                    if (newPIN == confirmNewPIN)
                    {
                        // Update the PIN
                        accountHolder.PIN = newPIN;
                        DisplayMessage("PIN successfully changed.");
                    }
                    else
                    {
                        DisplayMessage("PINs do not match. PIN change failed.");
                    }
                }
                else
                {
                    DisplayMessage("Incorrect old PIN. PIN change failed.");
                }
            }
            else
            {
                DisplayMessage("Account not found.");
            }
        }

        static void CheckBalance(string cardNumber)
        {
            DisplayMessage("Check Balance");
            // Find the account holder based on the card number
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            // Including Account type selection and displaying account name and balance
            if (accountHolder != null)
            {
                // Select Account Type
                DisplayMessage("Select Account type\n");
                DisplayMessage("1. Current\n");
                DisplayMessage("2. Savings\n");
                DisplayMessage("3. Credit\n");

                string? input = GetInput("Enter your choice (1-3): ");

                if (input != null && int.TryParse(input, out int value) && value >= 1 && value <= 3)
                {
                    string? selectedAccountType = GetAccountType(value);

                    if (selectedAccountType != null && selectedAccountType == accountHolder.AccountType)
                    {
                        DisplayMessage($"Account Name: {accountHolder.AccountName}");
                        DisplayMessage($"Account Balance: ₦{accountHolder.AccountBalance:N2}");
                    }
                    else
                    {
                        DisplayMessage($"Wrong Account type");
                    }
                }
            }
            else
            {
                DisplayMessage("Account not found.");
            }
        }

        static void TransferMoney(string? cardNumber)
        {
            DisplayMessage("Transfer Money");
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                // Select Account Type
                DisplayMessage("Select Account type\n");
                DisplayMessage("1. CURRENT");
                DisplayMessage("2. SAVINGS");
                DisplayMessage("3. CREDIT");

                string? input = GetInput("Enter your choice (1-3): ");

                if (input != null && int.TryParse(input, out int value) && value >= 1 && value <= 3)
                {

                    string? amount = GetInput("Enter amount: ");
                    string? beneficiaryaccountnumber = GetInput("Enter beneficiary account number: ");

                    if (!string.IsNullOrEmpty(amount) && !string.IsNullOrEmpty(beneficiaryaccountnumber) && beneficiaryaccountnumber.Length == 10)
                    {
                        DisplayMessage("Select bank type\n");
                        DisplayMessage("1. Access Diamond");
                        DisplayMessage("2. Key Stone Bank");
                        DisplayMessage("3. United Bank Of Africa");
                        DisplayMessage("4. Fidelity Bank");
                        DisplayMessage("5. First Bank");
                        DisplayMessage("6. Sterling Bank");
                        DisplayMessage("7. Polaris Bank");
                        DisplayMessage("8. Providous Bank");
                        DisplayMessage("9. Standard Trust");
                        DisplayMessage("10. Zenith Bank");
                        DisplayMessage("11. Guaranty Trust Bank");

                        string? banks = Console.ReadLine();
                        if (banks != null && int.TryParse(banks, out int options) && options >= 1 && options <= 11)
                        {
                            int.TryParse(amount, out int transferamount);
                            decimal balance = accountHolder.AccountBalance;
                            decimal balanceleft = balance - transferamount;

                            DisplayMessage("Transaction successful!");
                            DisplayMessage($"Your new balance is {balanceleft}");
                        }
                        else
                        {
                            DisplayMessage("Failed Transaction!");
                        }
                    }
                }
                else
                {
                    DisplayMessage($"Wrong account type");
                }
            }
        }
        static void QuickTeller(string? cardNumber)
        {
            DisplayMessage("QuickTeller");
            // Find the account holder based on the card number
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                //Display Options
                DisplayMessage("PLEASE SELECT OPTION");
                DisplayMessage("1. AIRTIME RECHARGE");
                DisplayMessage("2. PAYBILLS");
                DisplayMessage("3. SEND MONEY");
                DisplayMessage("4. SAFETOKEN");
                DisplayMessage("5. MAKE A PAYMENT");
                DisplayMessage("6. RECEIVE MONEY");
                DisplayMessage("7. BUY TICKET");
                DisplayMessage("8. MORE");

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
                DisplayMessage("1. CURRENT");
                DisplayMessage("2. SAVINGS");
                DisplayMessage("3. CREDIT");
                int selcetCardType = Convert.ToInt32(Console.ReadLine());

                //select a payment option
                Console.Clear();
                Console.WriteLine("Please select a payment option");
                DisplayMessage("1. ELECTRICITY");
                DisplayMessage("2. CABLE TV");
                DisplayMessage("3. LCC TV");
                DisplayMessage("4. MOBILE POSTPAID");
                DisplayMessage("5. PAY A MERCHANT");
                DisplayMessage("6. AIRLINES");
                DisplayMessage("7. DIESEL PURCHASE");
                DisplayMessage("8. MORE");
                int paymentOption = Convert.ToInt32(Console.ReadLine());

                //select the package you wish to pay for
                Console.Clear();
                Console.WriteLine("Please select a payment option");
                DisplayMessage("1. POSTPAID");
                DisplayMessage("2. PREPAID");

                //select your power distribution company
                int prepaid = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Please select your power distribution company");
                DisplayMessage("1. EKO");
                DisplayMessage("2. ENUGU");
                DisplayMessage("3. IKEJA");
                DisplayMessage("4. BENIN");
                DisplayMessage("5. IBADAN");
                DisplayMessage("6. PORTHARCOURT");
                DisplayMessage("7. KANO");
                DisplayMessage("8. NEXT");

                //define user input for distCo
                int distCo = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                //define the amount to debit
                Console.WriteLine("Please enter the amount for the prepaid unit you want to buy");

                int amount = Convert.ToInt32(Console.ReadLine());
                if (amount > accountHolder.AccountBalance)
                {
                    DisplayMessage("Insufficient funds");
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
                        DisplayMessage("input a valid meter number");
                    }
                }

            }
            else
            {
                DisplayMessage("Issuer switch");
            }
        }

        static void PayArena(string? cardNumber)
        {
            DisplayMessage("PayArena");
            // Find the account holder based on the card number
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                DisplayMessage("\n---------Pay Arena---------\n");
                // Implement logic for the Pay Bills process
                // Including card type selection, inputting amount, bill type selection, validation, etc.
                //Display Options
                DisplayMessage("PLEASE SELECT OPTION");
                DisplayMessage("1. AIRTIME RECHARGE");
                DisplayMessage("2. BILL PAYMENT");
                DisplayMessage("3. MONEY TRANSFER");
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
                DisplayMessage("1. CURRENT");
                DisplayMessage("2. SAVINGS");
                DisplayMessage("3. CREDIT");
                int selcetCardType = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please select your account type");
                DisplayMessage("1. CURRENT");
                DisplayMessage("2. SAVINGS");
                DisplayMessage("3. CREDIT");

                //select an option
                Console.Clear();
                Console.WriteLine("Please select an option");
                DisplayMessage("1. AIRTEL");
                DisplayMessage("2. MTN");
                DisplayMessage("3. GLO");
                DisplayMessage("4. ETISALAT");
                DisplayMessage("5. SMILE");
                int selectOption = Convert.ToInt32(Console.ReadLine());

                //select airtime amount
                Console.Clear();
                Console.WriteLine("Please select the airtime amount");
                DisplayMessage("1. N 100");
                DisplayMessage("2. N 200");
                DisplayMessage("3. N 500");
                DisplayMessage("4. N 1000");
                DisplayMessage("5. N 1500");
                DisplayMessage("6. OTHER");

                int airtimeamount = Convert.ToInt32(Console.ReadLine());
                if (airtimeamount > accountHolder.AccountBalance)
                {
                    DisplayMessage("             ");
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
                        DisplayMessage("Insufficient fund");
                    }
                }

            }




        }

    }
}





























