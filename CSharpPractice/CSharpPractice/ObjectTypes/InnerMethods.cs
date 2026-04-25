namespace CSharpPractice.ObjectTypes
{
    public static partial class Test
    {

        public static void TestInnerMethods()
        {
            int index = 10;
            void printMessage(BaseAccount account)
            {
                Console.WriteLine($"Index: {index}");
                Console.WriteLine(account.GetBalance());
                Console.WriteLine(account.GetMiniBalanceRequire());
            }

            SalaryAccount salaryAccount = new SalaryAccount();
            printMessage(salaryAccount);

            var salaryAccount1 = new SalaryAccount();
            salaryAccount1.Deposit(50000);
            printMessage(salaryAccount1);


            var printMessageDelegate = (BaseAccount account) =>
            {
                Console.WriteLine($"Index: {index}");
                Console.WriteLine(account.GetBalance());
                Console.WriteLine(account.GetMiniBalanceRequire());
                index++;
            };

            BaseAccount salaryAccount2 = new SalaryAccount();
            if (salaryAccount2 is SalaryAccount sa2)
            {
                sa2.Deposit(50000);
                printMessageDelegate(salaryAccount2);
                

            }
        }
    }
}