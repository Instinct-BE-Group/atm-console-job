using System.Text;

namespace atm_console_job
{
    internal class ATMConsoleMain
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Red;

            Console.Title = "Team Instinct ATM";
            // Welcome screen
            Console.WriteLine("Welcome, please insert your card");
            Console.ReadKey();
            Console.Clear();
            // Customer Input - Card Number and PIN
            string? cardNumber = "0000900200190000";
            //string? cardNumber = GetInput("Insert Card: ");
            Console.WriteLine("Please Wait");
            Console.ReadKey();
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
            Console.WriteLine("1) Activate your card or Change Pin\t\t2) Enter Pin\n");

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
            string? pin = GetSecureInput("Please enter your old PIN: ");
            string? pinOption = GetInput("\n1) Proceed\t\t\t\t2) Cancel \n\n");
            Console.Clear();

            if (!string.IsNullOrEmpty(cardNumber) && pinOption == "1" && VerifyCustomer(cardNumber, pin))
            {
                Console.WriteLine($"Welcome {GetAccountName(cardNumber)}\n");
                ChangePIN(cardNumber);
                Console.Clear();
                MainOperation(cardNumber);
            }
            else if (pinOption == "2")
            {
                Console.WriteLine("Transaction canceled. Thank you for using our ATM.");
            }
            else
            {
                Console.WriteLine("Invalid PIN. Thank you for using our ATM.");
            }
        }

        static void HandlePinEntryMode2(string? cardNumber)
        {
            Console.Clear();
            string? pin = GetSecureInput("\nPlease enter your PIN: ");
            string? pinOption = GetInput("\n1) Proceed\t\t\t\t2) Cancel \n\n");
            Console.Clear();

            if (pinOption == "1" && VerifyCustomer(cardNumber, pin))
            {
                Console.WriteLine($"Welcome {GetAccountName(cardNumber)}\n");
                MainOperation(cardNumber);
            }
            else if (pinOption == "2")
            {
                Console.WriteLine("Transaction canceled. Thank you for using our ATM.");
            }
            else
            {
                Console.WriteLine("Invalid PIN. Thank you for using our ATM.");
            }
        }

        static string? GetInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        static string GetSecureInput(string prompt)
        {
            // Define what is inputted
            Console.Write(prompt);

            // Creates a place to store the characters that were inputted
            StringBuilder input = new StringBuilder();

            // Starts the loop that will run until the user presses Enter
            ConsoleKeyInfo key;
            do
            {
                // Listens for each key the user inputs
                key = Console.ReadKey(true);

                // If the user presses any key other than Backspace or Enter
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    // Add the key to our input and show an asterisk on the screen
                    input.Append(key.KeyChar);
                    Console.Write("*");
                }
                else
                {
                    // If the user presses the Backspace key
                    if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        // Remove the last character from our input and erase the asterisk from the screen
                        input.Length--;
                        Console.Write("\b \b");
                    }
                }
            }
            // Repeat the loop until the user presses the Enter key
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return input.ToString();
        }

        static bool VerifyCustomer(string? cardNumber, string? pin)
        {
            return AccountData.accountHolders.Any(holder => holder.CardNumber == cardNumber && holder.PIN == pin);
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
            return "Account Not Found\n";
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
            if(string.IsNullOrEmpty(cardNumber))
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
                while (continueTransaction)
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
                        //case 7:
                        //    PayArena(cardNumber);
                        //    break;
                        case 7:
                            continueTransaction = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }

                    //do you want to perform another transaction
                    if (continueTransaction)
                    {
                        Console.WriteLine("Do you want to perform another transaction? \n");
                        Console.WriteLine("1. Yes\t\t\t\t2.No \n");
                        string? response = GetInput("");
                        if (response != null && int.TryParse(response, out int res) && res != 1)
                        {
                            continueTransaction = false;
                            return;
                        }
                        else
                        {
                            Console.Clear();
                            HandlePinEntryMode2(cardNumber);
                            continueTransaction = true;
                        }
                    }
                };
            }// End of transaction
            Console.WriteLine("Thank you for using our ATM. Please take your card.\n");

        }

        static int ShowMainMenu()
        {
            // Implement logic to display and get the main menu option chosen by the user
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("1. Open Account \t\t\t2. Withdraw Cash\n");
            Console.WriteLine("3. Change PIN \t\t\t\t4. Check Balance\n");
            Console.WriteLine("5. Transfer Money\t\t\t6. QuickTeller\n");
            Console.WriteLine("7. More\n");


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

                string? bvn = GetInput("Input your BVN: ");
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
                        Console.ReadKey();
                        Console.Clear();
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

                if (int.TryParse(input, out int value) && value >= 1 && value <= 3)
                {
                    string? selectedAccountType = GetAccountType(value);

                    if (selectedAccountType != null && selectedAccountType == accountHolder.AccountType)
                    {
                        Console.WriteLine("Do you want a receipt for this transaction?\n");
                        Console.WriteLine("1. Yes \t\t\t\t2.No\n");
                        string? receiptOption = GetInput("Select an option (1-2): ");
                        Console.Clear();

                        // Display available withdrawal amounts
                        Console.WriteLine("Available withdrawal amounts:\n");
                        Console.WriteLine("1. N 500\t\t\t\t5. N 10000\n");
                        Console.WriteLine("2. N 1000\t\t\t\t6. N 20000\n");
                        Console.WriteLine("3. N 2000\t\t\t\t7. N 40000\n");
                        Console.WriteLine("4. N 5000\t\t\t\t8. Others\n");

                        string? withdrawalOption = GetInput("Select an option (1-8): ");
                        Console.Clear();

                        if (int.TryParse(withdrawalOption, out int option) && option >= 1 && option <= 8)
                        {
                            decimal withdrawalAmount = 0;

                            if (option >= 1 && option <= 7)
                            {
                                // Map the selected option to the corresponding withdrawal amount
                                decimal[] withdrawalAmounts = { 500, 1000, 2000, 5000, 10000, 20000, 40000 };
                                withdrawalAmount = withdrawalAmounts[option - 1];
                            }
                            else if (option == 8) // Others
                            {
                                string? amountInput = GetInput("Enter the withdrawal amount (in multiples of 500):\n\n");
                                Console.Clear();

                                if (decimal.TryParse(amountInput, out decimal enteredAmount) && enteredAmount % 500 == 0)
                                {
                                    withdrawalAmount = enteredAmount;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid amount. Please enter a valid amount in multiples of ₦500.");
                                    return;
                                }
                            }

                            if (withdrawalAmount <= 0)
                            {
                                Console.WriteLine("Invalid withdrawal amount.\n");
                            }
                            else if (withdrawalAmount > accountHolder.AccountBalance)
                            {
                                Console.WriteLine("Insufficient Balance\n");
                            }
                            else
                            {
                                // Debit the account and display success message
                                accountHolder.AccountBalance -= withdrawalAmount;

                                if (receiptOption != null && int.TryParse(receiptOption, out int receiptOpt) && receiptOpt == 1)
                                {
                                    Console.WriteLine($"Take your cash\n");
                                    Console.ReadKey();
                                    Console.WriteLine($"Receipt\n");
                                    Console.WriteLine($"Withrawn Amount: N {withdrawalAmount:N2}\n");
                                    Console.WriteLine($"Account Balance: N {accountHolder.AccountBalance:N2}\n");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine($"Take your cash\n");
                                    Console.ReadKey();
                                    Console.Clear();
                                }

                                Console.WriteLine("Did you know you can recharge your phone on this ATM right now!\n");
                                Console.WriteLine("It's easy to use, just select recharge now\n\n");
                                Console.WriteLine("1. Recharge Now\n");
                                Console.WriteLine("2. Try Later\n");

                                string? rechargeOption = GetInput("Select an option (1-2): ");
                                if (rechargeOption == "1")
                                {
                                    Console.WriteLine("Okay");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.Clear();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please select a valid option (1-8).\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Wrong account type\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("Issuer error.\n");
            }
        }

        static void ChangePIN(string cardNumber)
        {
            Console.WriteLine("Change Pin\n");
            // Find the account holder based on the card number
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                string? oldPIN = GetSecureInput("Enter your old PIN: ");
                Console.Clear();
                if (oldPIN == accountHolder.PIN)
                {
                    string? newPIN = GetSecureInput("Enter your new PIN: ");
                    Console.Clear();
                    string? confirmNewPIN = GetSecureInput("Confirm your new PIN: ");
                    Console.Clear();

                    if (newPIN == confirmNewPIN)
                    {
                        // Update the PIN
                        accountHolder.PIN = newPIN;
                        Console.WriteLine("PIN successfully changed.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("PINs do not match. PIN change failed.");
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect old PIN. PIN change failed.");
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
                Console.Clear();
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
                        Console.WriteLine($"Hello, {accountHolder.AccountName}\n");
                        Console.WriteLine($"Your account balance is N {accountHolder.AccountBalance:N2}\n");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine($"Wrong Account type\n");
                        Console.ReadKey();
                        Console.Clear();
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
                Console.WriteLine("1. CURRENT\n");
                Console.WriteLine("2. SAVINGS\n");
                Console.WriteLine("3. CREDIT\n");

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
                        Console.WriteLine("1. Access Diamond\t\t\t5. First Bank\n");
                        Console.WriteLine("2. Guaranty Trust Bank\t\t\t6. Sterling Bank\n");
                        Console.WriteLine("3. Zenith Bank\t\t\t\t7. United Bank Of Africa\n");
                        Console.WriteLine("4. Fidelity Bank\t\t\t8. More\n");

                        string? banks = Console.ReadLine();
                        Console.Clear();
                        if (banks != null && int.TryParse(banks, out int options) && options >= 1 && options <= 11)
                        {
                            int.TryParse(amount, out int transferamount);
                            // Debit the account and display success message
                            accountHolder.AccountBalance -= transferamount;

                            Console.WriteLine("Transaction successful!");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Failed Transaction!\n");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid beneficiary account!\n");
                    }
                }
                else
                {
                    Console.WriteLine($"Wrong account type\n");
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
                Console.WriteLine("PLEASE SELECT OPTION\n");
                Console.WriteLine("1. AIRTIME RECHARGE\t\t\t5. MAKE A PAYMENT\n");
                Console.WriteLine("2. PAYBILLS\t\t\t\t6. RECEIVE MONEY\n");
                Console.WriteLine("3. SEND MONEY\t\t\t\t7. BUY TICKET\n");
                Console.WriteLine("4. SAFETOKEN\t\t\t\t8. MORE\n");

                string[] quickteller = { "NONE", "1. AIRTIME RECHARGE", "2. PAYBILLS", "3. SEND MONEY", "4. SAFETOKEN", "5. MAKE A PAYMENT", "6. RECEIVE MONEY", "7. BUY TICKET", "8. MORE" };
                int input = Convert.ToInt32(Console.ReadLine());
                int output;

                for (int i = 0; i < quickteller.Length; i++)
                {
                    if (input == i)
                    {
                        output = i;
                        Console.Clear();
                        Console.WriteLine(quickteller[output]);
                    }
                }

                Console.WriteLine("Please select your account type\n");
                Console.WriteLine("1. CURRENT\n");
                Console.WriteLine("2. SAVINGS\n");
                Console.WriteLine("3. CREDIT\n");
                int selectAccountType = Convert.ToInt32(Console.ReadLine());

                Console.Clear();
                Console.WriteLine("Please select a payment option\n");
                Console.WriteLine("1. ELECTRICITY\t\t\t\t5. PAY A MERCHANT\n");
                Console.WriteLine("2. CABLE TV\t\t\t\t6. AIRLINES\n");
                Console.WriteLine("3. LCC TV\t\t\t\t7. DIESEL PURCHASE\n");
                Console.WriteLine("4. MOBILE POSTPAID\t\t\t8. MORE\n");
                int paymentOption = Convert.ToInt32(Console.ReadLine());

                Console.Clear();
                Console.WriteLine("Please select a payment option\n");
                Console.WriteLine("1. POSTPAID\n");
                Console.WriteLine("2. PREPAID\n");

                int prepaid = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Please select your power distribution company\n");
                Console.WriteLine("1. EKO\t\t\t\t5. IBADAN\n");
                Console.WriteLine("2. ENUGU\t\t\t\t6. PORTHARCOURT\n");
                Console.WriteLine("3. IKEJA\t\t\t\t7. KANO\n");
                Console.WriteLine("4. BENIN\t\t\t\t8. NEXT\n");

                int distCo = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                Console.WriteLine("Please enter the amount for the prepaid unit you want to buy\n");
                int amount = Convert.ToInt32(Console.ReadLine());
                if (amount > accountHolder.AccountBalance)
                {
                    Console.WriteLine("Insufficient funds\n");
                }
                else
                {
                    Console.Clear();
                    accountHolder.AccountBalance -= amount;

                    Console.WriteLine("Please enter your meter number\n");

                    string? meterNumber = Console.ReadLine();
                    if (meterNumber != null)
                    {
                        Console.WriteLine("Unit purchased successfully\n");
                    }
                    else
                    {
                        Console.WriteLine("Input a valid meter number\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("Issuer switch");
            }
        }

    }
}
