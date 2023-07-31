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

                        if (pinOption == "1" && VerifyCustomer(cardNumber, pin))
                        {
                            // If pin is correct, proceed to Main Menu
                            verifiedPin = pin;
                            DisplayMessage($"Welcome {GetAccountName(cardNumber)}");
                            ChangePIN();
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
                            DisplayMessage($"Welcome {GetAccountName(cardNumber)}");
                            MainOperation();
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

        static void MainOperation()
        {
            // Main Menu (Screen 5)
            bool continueTransaction = true;
            while (continueTransaction)
            {
                int option = ShowMainMenu();

                switch (option)
                {
                    case 1:
                        OpenAccount();
                        break;
                    case 2:
                        WithdrawCash();
                        break;
                    case 3:
                        ChangePIN();
                        break;
                    case 4:
                        CheckBalance();
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
                        DisplayMessage("Invalid option. Please try again.");
                        Console.Clear();
                        // got to welcome screen
                        break;
                }
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
                if (int.TryParse(input, out int option) && option >= 1 && option <= 8)
                {
                    return option;
                }
                DisplayMessage("Invalid input. Please enter a valid option (1-8).");
            }
        }

        static void OpenAccount()
        {
            DisplayMessage("Open Account");
            // Implement logic for the Open Account process
            // Including BVN verification, OTP verification, etc.
        }

        static void WithdrawCash()
        {
            DisplayMessage("Withdraw Cash");

            // Implement logic for the Withdraw Cash process
            // Including card type selection, amount selection, and cross-selling options
        }

        static void ChangePIN()
        {
            DisplayMessage("Change Pin");
            // Implement logic for the Change PIN process
            // Including inputting old PIN, inputting new PIN, and confirming new PIN
        }

        static void CheckBalance()
        {
            DisplayMessage("Withdraw Cash");
            // Implement logic for the Check Balance process
            // Including card type selection and displaying account name and balance
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