namespace CSharpPractice.ObjectTypes
{
    public static partial class Test
    {        
        public static partial void TestEventBasic()
        {
            int index = 0;
            var printAccountBalanceDelegate = (BaseAccount account) =>
            {
                Console.WriteLine($"Index: {index}");
                Console.WriteLine(account.GetBalance());
                Console.WriteLine(account.GetMiniBalanceRequire());
                index++;
            };

            var printBranchDelegate = (BaseAccount account) =>
            {
                Console.WriteLine($"Index: {index}");
                Console.WriteLine(account.BranchName);
                Console.WriteLine(account.BankName);
                index++;
            };

            //Create a event for printMessageDelegate and subscribe to it
            Action<BaseAccount>? printMessageEvent = null;
            printMessageEvent += printAccountBalanceDelegate;
            printMessageEvent += printBranchDelegate;

            printMessageEvent?.Invoke(new SalaryAccount
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

        }
    }
}
