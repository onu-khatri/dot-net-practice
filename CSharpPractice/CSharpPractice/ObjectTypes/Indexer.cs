using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPractice.ObjectTypes
{
    public static partial class Test
    {
        public static void TestIndexer()
        {
            var accountManager = new AccountManager();
            accountManager.AddAccount(new SalaryAccount
            {
                AccountNumber = "1234567890",
                AccountHolderName = "John Doe",
                BankName = "ABC Bank",
                BranchName = "Main Branch",
                IFSCCode = "ABC123456",
                CurrencyCode = CurrencyCode.USD,
                Balance = 1000,
                AvailableBalance = 1000,
                MinimumBalanceRequirement = 500,
                OverdraftLimit = 0,
                InterestRate = 0.05m,
                AccountOpenedOn = DateOnly.FromDateTime(DateTime.Now),
                IsActive = true
            });
            var account1 = accountManager["1234567890"];
            if (account1 != null)
            {
                Console.WriteLine($"Account Holder Name: {account1.AccountHolderName}");
                Console.WriteLine($"Bank Name: {account1.BankName}");
            }

            var accountNumber = accountManager[0];
            if (accountNumber != null) {
                Console.WriteLine($"Account Number at index 0: {accountNumber}");
            }
        }
    }

    internal class AccountManager
    {
        private readonly Dictionary<string, SalaryAccount> _accounts = new(StringComparer.Ordinal);
        private List<int> _accountNumbers = new();

        public void AddAccount(SalaryAccount account)
        {
            ArgumentNullException.ThrowIfNull(account);

            if (string.IsNullOrWhiteSpace(account.AccountNumber))
                throw new ArgumentException("Account number is required.", nameof(account));

            if(int.TryParse(account.AccountNumber, out var accountNumber))
            {
                if (_accountNumbers.Contains(accountNumber))
                    throw new InvalidOperationException($"An account with number {account.AccountNumber} already exists.");
            }
            else
            {
                throw new ArgumentException("Account number must be a valid integer.", nameof(account));
            }

            _accounts[account.AccountNumber] = account;
            _accountNumbers.Add(int.Parse(account.AccountNumber));
        }

        public bool RemoveAccount(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                return false;

            return _accounts.Remove(accountNumber);
        }

        public int? this[int index]
        {
            get { 
                if (index < 0 || index >= _accountNumbers.Count)
                    return null;
                return _accountNumbers[index];
            }
            set             {
                if (index < 0 || index >= _accountNumbers.Count)
                    throw new IndexOutOfRangeException("Index is out of range.");
                if (value is null)
                    throw new ArgumentNullException(nameof(value), "Account number cannot be null.");
                _accountNumbers[index] = value.Value;
            }
        }

        public SalaryAccount? this[string accountNumber]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(accountNumber))
                    return null;

                return _accounts.TryGetValue(accountNumber, out var account) ? account : null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(accountNumber))
                    throw new ArgumentException("Account number is required.", nameof(accountNumber));

                if (value is null)
                {
                    _accounts.Remove(accountNumber);
                    return;
                }

                _accounts[accountNumber] = value;
            }
        }
    }
}
