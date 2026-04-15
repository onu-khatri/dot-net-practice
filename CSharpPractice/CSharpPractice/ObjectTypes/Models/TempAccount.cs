namespace CSharpPractice.ObjectTypes
{
    internal class TempAccount : SalaryAccount
    {
        public string TemporaryReferenceId { get; set; } = string.Empty;
        public DateOnly? ValidUntil { get; set; }
        public string? Notes { get; set; }

        public override decimal GetBalance()
        {
            return AvailableBalance;
        }

        public override decimal GetOverdraftLimit()
        {
            return 10;
        }
    }
}