namespace MoneyTransactions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        private const string DEPOSIT_COMMAND = "Deposit";
        private const string WITHDRAW_COMMAND = "Withdraw";


        private const string INVALID_COMMAND_EXCEPTION = "Invalid command!";
        private const string INVALID_ACCOUNT_EXCEPTION = "Invalid account!";
        private const string INSUFFICIENT_BALANCE_EXCEPTION = "Insufficient balance!";

        static void Main(string[] args)
        {
            List<BankAccount> bankAccountsData = CreateBankAccounts();
            ATM(bankAccountsData);
        }

        private class BankAccount
        {
            public BankAccount(int accountNumber, double balance)
            {
                this.AccountNumber = accountNumber;
                this.Balance = balance;
            }

            public int AccountNumber { get; private set; }
            public double Balance { get; private set; }

            public void Deposit(double amount)
            {
                // Optional -> Do it using CW or Error Handling
                // if (amount <= 0)
                //     Console.WriteLine("Invalid deposit amount!");

                this.Balance += amount;
            }

            public void Withdraw(double amount)
            {
                // Optional -> Do it using CW or Error Handling
                // if (amount <= 0)
                //     Console.WriteLine("Invalid withdraw amount!");

                if (this.Balance < amount)
                    throw new ArgumentException(INSUFFICIENT_BALANCE_EXCEPTION);

                this.Balance -= amount;
            }

            public override string ToString() => $"Account {this.AccountNumber} has new balance: {this.Balance:F2}";
        }

        private static List<BankAccount> CreateBankAccounts()
        {
            List<BankAccount> bankAccountsData = new List<BankAccount>();
            string[] bankAccountsInfo = Console.ReadLine()!
                .Split(',')
                .ToArray();

            foreach (var bankAccount in bankAccountsInfo)
            {
                string[] currentBankAccountInfo = bankAccount.Split('-').ToArray();
                int bankAccountNumber = int.Parse(currentBankAccountInfo[0]);
                double bankAccountBalance = double.Parse(currentBankAccountInfo[1]);

                BankAccount account = new BankAccount(bankAccountNumber, bankAccountBalance);
                bankAccountsData.Add(account);
            }

            return bankAccountsData;
        }

        private static void ATM(List<BankAccount> bankAccountsData)
        {
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                try
                {
                    string[] inputInfo = input
                        .Split()
                        .ToArray();
                    string mainCommand = inputInfo[0];
                    int bankAccountNumber = int.Parse(inputInfo[1]);
                    double amount = double.Parse(inputInfo[2]);

                    BankAccount bankAccount = bankAccountsData.Find(a => a.AccountNumber == bankAccountNumber);
                    if (bankAccount == null)
                        throw new ArgumentException(INVALID_ACCOUNT_EXCEPTION);

                    switch (mainCommand)
                    {
                        case DEPOSIT_COMMAND:
                            bankAccount.Deposit(amount);
                            Console.WriteLine(bankAccount.ToString());
                            break;
                        case WITHDRAW_COMMAND:
                            bankAccount.Withdraw(amount);
                            Console.WriteLine(bankAccount.ToString());
                            break;
                        default: throw new ArgumentException(INVALID_COMMAND_EXCEPTION);
                    }

                    Console.WriteLine("Enter another command");
                }

                catch (ArgumentException argex)
                {
                    Console.WriteLine(argex.Message);
                    Console.WriteLine("Enter another command");
                }
            }
        }
    }
}