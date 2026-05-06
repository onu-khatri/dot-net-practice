using CSharpPractice.ObjectTypes;

namespace CSharpPractice.Linq
{
    internal class LinqTest<T> where T : SalaryAccount, new()
    {
        public List<T> accounts = new List<T>{
                new T { EmployerName = "Company A", EmployeeId = "E001", SalaryCreditDay = 5, LastSalaryCreditedOn = new DateOnly(2024, 6, 1), Balance = 50000, AvailableBalance = 50000 },
                new T { EmployerName = "Company B", EmployeeId = "E002", SalaryCreditDay = 10, LastSalaryCreditedOn = new DateOnly(2024, 6, 3), Balance = 75000, AvailableBalance = 75000 },
                new T { EmployerName = "Company A", EmployeeId = "E003", SalaryCreditDay = 10, LastSalaryCreditedOn = new DateOnly(2024, 6, 5), Balance = 60000, AvailableBalance = 60000 },
                new T { EmployerName = "Company C", EmployeeId = "E004", SalaryCreditDay = 20, LastSalaryCreditedOn = new DateOnly(2024, 6, 7), Balance = 80000, AvailableBalance = 80000 }
            };

        public void TestLinqUsingSalaryAccount()
        {
            IEnumerable<T>? highBalanceAccounts = default;
            // Example LINQ query: Get all accounts with balance greater than 60000
            try
            {
                highBalanceAccounts = accounts //db context
                    .Where(account => account.Balance > 60000)
                    .Reverse()
                    .OrderByDescending(t => t.SalaryCreditDay)
                    .ThenBy(t => t.EmployeeId)
                    .Take(2);
                //.Select(t => new { t.EmployeeId, t.SalaryCreditDay });
                // SelectMany, GroupBy, first vs FirstOrDefault, LastOrDefault, Single vs SingleOrDefault, Skip, Take, SkipWhile, TakeWhile

                // Difference between IEnumerable and IQueryable, deferred execution, immediate execution, expression trees, etc.
                // IList, IEnumerable, List
                // List, Array, ArrayList, LinkedList, Stack, Queue, HashSet, Dictionary, SortedList, SortedDictionary, ConcurrentDictionary, etc.
            }
            catch (Exception ex) {
                // this will never reach as try-catch is wraping only query and not execution.
                Console.WriteLine("An error occurred while fetching high balance accounts: " + ex.Message);
            }

            // real error will come there
            var fetchedAccounts = highBalanceAccounts?.ToList();

            var firstAccount = accounts.FirstOrDefault();
            var firstConditionalAccount = accounts.FirstOrDefault(t => t.EmployeeId.Equals("e001", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("Accounts with balance greater than 60000:");
            Console.WriteLine("total high balance accounts: " + fetchedAccounts?.Count);

            foreach (var account in fetchedAccounts ?? [])
            {
                Console.WriteLine($"Employee ID: {account.EmployeeId}, SalaryCreditDay: {account.SalaryCreditDay}");
            }
        }
    }
}
