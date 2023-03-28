using System;
using System.Collections.Generic;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Account> accounts = new List<Account>();

            while (true)
            {
                Console.WriteLine("Welcome to the ATM machine. Please select an option:");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Log In");
                Console.WriteLine("3. Exit");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Please enter your full name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Please enter your initial balance:");
                        decimal balance = decimal.Parse(Console.ReadLine());

                        Account account = new Account(name, balance);
                        accounts.Add(account);

                        Console.WriteLine($"Account created successfully. Your account number is {account.AccountNumber}. Please remember this number for future use.");
                        break;
                    case 2:
                        Console.WriteLine("Please enter your account number:");
                        int accountNumber = int.Parse(Console.ReadLine());

                        Account loggedInAccount = null;

                        foreach (Account a in accounts)
                        {
                            if (a.AccountNumber == accountNumber)
                            {
                                loggedInAccount = a;
                                break;
                            }
                        }

                        if (loggedInAccount == null)
                        {
                            Console.WriteLine("Invalid account number. Please try again.");
                            break;
                        }

                        Console.WriteLine($"Welcome, {loggedInAccount.Name}. Please select an option:");
                        Console.WriteLine("1. Balance Inquiry");
                        Console.WriteLine("2. Withdrawal");
                        Console.WriteLine("3. Deposit");
                        Console.WriteLine("4. Transaction History");
                        Console.WriteLine("5. Log Out");

                        int loggedInOption = int.Parse(Console.ReadLine());

                        switch (loggedInOption)
                        {
                            case 1:
                                Console.WriteLine($"Your current balance is {loggedInAccount.Balance:C}");
                                break;
                            case 2:
                                Console.WriteLine("Please enter the amount to withdraw:");
                                decimal withdrawAmount = decimal.Parse(Console.ReadLine());

                                if (withdrawAmount > loggedInAccount.Balance)
                                {
                                    Console.WriteLine("Insufficient funds.");
                                    break;
                                }

                                loggedInAccount.Withdraw(withdrawAmount);
                                Console.WriteLine($"Transaction successful. Your new balance is {loggedInAccount.Balance:C}");
                                break;
                            case 3:
                                Console.WriteLine("Please enter the amount to deposit:");
                                decimal depositAmount = decimal.Parse(Console.ReadLine());

                                loggedInAccount.Deposit(depositAmount);
                                Console.WriteLine($"Transaction successful. Your new balance is {loggedInAccount.Balance:C}");
                                break;
                            case 4:
                                Console.WriteLine("Transaction History:");
                                foreach (Transaction t in loggedInAccount.TransactionHistory)
                                {
                                    Console.WriteLine($"{t.Type}: {t.Amount:C} on {t.Timestamp}");
                                }
                                break;
                            case 5:
                                Console.WriteLine("Logged out successfully.");
                                break;
                            default:
                                Console.WriteLine("Invalid option. Please try again.");
                                break;
                        }
                        break;
                    case 3:
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }

    class Account
    {
        private static int _nextAccountNumber = 1000;

        public int AccountNumber { get; private set; }
        public string Name { get; private set; }
        public decimal Balance { get; private set; }
        public List<Transaction> TransactionHistory { get; private set; }
        public Account(string name, decimal balance)
        {
            AccountNumber = _nextAccountNumber++;
            Name = name;
            Balance = balance;
            TransactionHistory = new List<Transaction>();
        }

        public void Withdraw(decimal amount)
        {
            Balance -= amount;
            TransactionHistory.Add(new Transaction("Withdrawal", amount));
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
            TransactionHistory.Add(new Transaction("Deposit", amount));
        }
    }

    class Transaction
    {
        public string Type { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Timestamp { get; private set; }

        public Transaction(string type, decimal amount)
        {
            Type = type;
            Amount = amount;
            Timestamp = DateTime.Now;
        }
    }
}

