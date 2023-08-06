namespace atm_console_job
{
    internal class ATMConsoleMain
    {
        static void Main(string[] args)
        {
            Console.Title = "Sterling Bank ATM - Team Instinct";
            // Welcome screen
            DisplayMessage("Welcome to Sterling Bank");

            // Customer Input - Card Number and PIN
            string? cardNumber = GetInput("Enter your Card Number: ");

            // Verify the card number and PIN
            string? verifiedPin = null;

            foreach (var item in AccountData.accountHolders)
            {
                if (cardNumber == item.CardNumber)
                {
                    Console.Clear();
                    string? pinEntryMode = GetPinEntryMode();

                    // Please Select your pin entry mode
                    if (pinEntryMode == "1")
                    {
                        Console.Clear();
                        // Please enter your pin
                        string? pin = GetInput("Please enter your old PIN: ");
                        string? pinOption = GetInput("1) Proceed\t\t\t 2) Cancel");
                        Console.Clear();

                        if (!string.IsNullOrEmpty(cardNumber) && pinOption == "1" && VerifyCustomer(cardNumber, pin))
                        {
                            // If pin is correct, proceed to Main Menu
                            verifiedPin = pin;
                            DisplayMessage($"Welcome {GetAccountName(cardNumber)}");
                            ChangePIN(cardNumber);
                        }
                        else
                        {
                            // If pin is incorrect or user cancels, end the transaction
                            DisplayMessage("Invalid PIN. Thank you for using our ATM.");
                        }
                    }
                    else if (pinEntryMode == "2")
                    {
                        Console.Clear();
                        // Screen 4: Please enter your pin
                        string? pin = GetInput("Please enter your PIN: ");
                        string? pinOption = GetInput("1) Proceed\t\t\t 2) Cancel");
                        // If "Enter Pin" option is chosen, proceed directly to Main Menu
                        if (pinOption == "1" && VerifyCustomer(cardNumber, pin))
                        {
                            // If pin is correct, proceed to Main Menu
                            verifiedPin = pin;
                            DisplayMessage($"Welcome {GetAccountName(cardNumber)}\n");
                            MainOperation(cardNumber);
                        }
                    }
                    else
                    {
                        // Invalid input for pin entry mode, end the transaction
                        DisplayMessage("Invalid input. Thank you for using our ATM.");
                    }
                }

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
                // Main Menu (Screen 5)
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
                            TransferMoney();
                            break;
                        case 6:
                            QuickTeller();
                            break;
                        case 7:
                            PayArena();
                            break;
                        case 8:
                            continueTransaction = false;
                            break;
                        default:
                            Console.Clear();
                            DisplayMessage("Invalid option. Please try again.");
                            continueTransaction = true;
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

        static string? GetPinEntryMode()
        {
            // Display the PIN entry mode options
            DisplayMessage("Please Select your PIN entry mode:");
            DisplayMessage("1) Activate your card or Change Pin");
            DisplayMessage("2) Enter Pin");

            // Prompt the user for input and keep asking until a valid option is selected
            while (true)
            {
                string? input = GetInput("Enter your choice (1-2): ");
                if (input == "1" || input == "2")
                {
                    return input;
                }
                DisplayMessage("Invalid input. Please enter a valid option (1-2).");
            }
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

        static string GetCardType(int value)
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
        c
        static void OpenAccount(string? cardNumber)
        {
            DisplayMessage("Open Account");
            // Implement logic for the Open Account process
            // Including BVN verification, OTP verification, etc.
        }

        static void WithdrawCash(string? cardNumber)
        {
            DisplayMessage("Withdraw Cash");

            // Implement logic for the Withdraw Cash process
            // Including card type selection, amount selection, and cross-selling options
            // Find the account holder based on the card number
            AccountHolder? accountHolder = AccountData.accountHolders.Find(holder => holder.CardNumber == cardNumber);

            if (accountHolder != null)
            {
                // Select Card Type
                DisplayMessage("Select card type\n");
                DisplayMessage("1. Current\n");
                DisplayMessage("2. Savings\n");
                DisplayMessage("3. Credit\n");

                string? input = Console.ReadLine();

                if (input != null && int.TryParse(input, out int value) && value >= 1 && value <= 3)
                {
                    string? selectedCardType = GetCardType(value);

                    if (selectedCardType != null && selectedCardType == accountHolder.CardType)
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
                        DisplayMessage($"Wrong card type");
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

            // Including card type selection and displaying account name and balance
            if (accountHolder != null)
            {
                // Select Card Type
                DisplayMessage("Select card type\n");
                DisplayMessage("1. Current\n");
                DisplayMessage("2. Savings\n");
                DisplayMessage("3. Credit\n");

                string? input = Console.ReadLine();

                if (input != null && int.TryParse(input, out int value) && value >= 1 && value <= 3)
                {
                    string? selectedCardType = GetCardType(value);

                    if (selectedCardType != null && selectedCardType == accountHolder.CardType)
                    {
                        DisplayMessage($"Account Name: {accountHolder.AccountName}");
                        DisplayMessage($"Account Balance: ₦{accountHolder.AccountBalance:N2}");
                    }
                    else
                    {
                        DisplayMessage($"Wrong card type");
                    }
                }
            }
            else
            {
                DisplayMessage("Account not found.");
            }
        }

        static void TransferMoney()
        {
            DisplayMessage("Transfer Money");
            // Implement logic for the Transfer Money process
            // Including card type selection, inputting amount, beneficiary account, beneficiary bank, etc.
        }
        static void QuickTeller()
        {
            DisplayMessage("QuickTeller");
            // Implement logic for the Pay Bills process
            // Including card type selection, inputting amount, bill type selection, validation, etc.
        }

        static void PayArena()
        {
            DisplayMessage("Pay Arena");
            // Implement logic for the Pay Bills process
            // Including card type selection, inputting amount, bill type selection, validation, etc.
        }
    }
}