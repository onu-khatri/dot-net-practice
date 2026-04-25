namespace CSharpPractice.ObjectTypes
{
    public static partial class Test
    {
        public static partial void TestEventBasic();

        public static void TestObjectOverride()
        {
            SalaryAccount salaryAccount = new SalaryAccount();
            Console.WriteLine(salaryAccount.GetBalance());
            Console.WriteLine(salaryAccount.GetMiniBalanceRequire());

            var salaryAccount1 = new SalaryAccount();
            salaryAccount1.Deposit(50000);
            Console.WriteLine(salaryAccount1.GetBalance());

            BaseAccount salaryAccount2 = new SalaryAccount();
            if (salaryAccount2 is SalaryAccount sa2)
            {
                sa2.Deposit(50000);
                Console.WriteLine(sa2.GetBalance());
            }
        }
    }
}