namespace CSharpPractice.ObjectTypes
{
    internal class SalaryAccount : BaseAccount
    {
        public SalaryAccount()
        {
            base.MinimumBalanceRequirement = 350000;
        }

        public string EmployerName { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public int SalaryCreditDay { get; set; }
        public DateOnly? LastSalaryCreditedOn { get; set; }
        public override decimal GetBalance()
        {
            return AvailableBalance;
        }

        public override decimal GetMiniBalanceRequire()
        {            
            return MinimumBalanceRequirement;
        }

        public new void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be greater than zero.");
            }
            Balance += amount + 1;
            AvailableBalance += amount + 1;
            LastTransactionOn = DateTime.Now;
        }
    }
}