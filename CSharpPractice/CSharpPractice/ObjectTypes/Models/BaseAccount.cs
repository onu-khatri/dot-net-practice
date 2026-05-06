namespace CSharpPractice.ObjectTypes
{
    internal abstract class BaseAccount : IBankAccount
    {
        private int _accountValue = 10;

        public int AccountValue
        {
            get { return _accountValue * 2; }
            set {
                if(value < 0)
                {
                    throw new ArgumentException("Account value cannot be negative.");
                }
                _accountValue = value; 
            }
        }

        public int AccountNumberValue
        {
            get { return field; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Account value cannot be negative.");
                }
                field = value;
            }
        }

        public string AccountNumber { get; set; } = string.Empty;

        

        public string AccountHolderName { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string IFSCCode { get; set; } = string.Empty;
        public string? IBAN { get; set; }

        public CurrencyCode CurrencyCode { get; set; } = CurrencyCode.INR;

        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal MinimumBalanceRequirement { get; set; } = 25000;
        public decimal OverdraftLimit { get; set; }
        public decimal InterestRate { get; init; }

        public DateOnly AccountOpenedOn { get; set; }
        public DateTime? LastTransactionOn { get; }
        public bool IsActive { get; set; } = true;

        public abstract decimal GetBalance();

        public virtual decimal GetMiniBalanceRequire()
        {
            return MinimumBalanceRequirement * 0.15M;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be greater than zero.");
            }
            Balance += amount;
            AvailableBalance += amount;
        }
    }
}