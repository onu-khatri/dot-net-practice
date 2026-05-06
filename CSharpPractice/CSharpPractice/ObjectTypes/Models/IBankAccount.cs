namespace CSharpPractice.ObjectTypes
{
    public interface IBankAccount
    {
        public string AccountNumber { get; set; }

        public string AccountHolderName { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string? IBAN { get; set; }

        public CurrencyCode CurrencyCode { get; set; }

        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal MinimumBalanceRequirement { get; set; }
        public decimal OverdraftLimit { get; set; }
        public decimal InterestRate { get; init; }

        public DateOnly AccountOpenedOn { get; set; }
        public DateTime? LastTransactionOn { get; }
        public bool IsActive { get; set; }

        public decimal GetBalance();
    }
}