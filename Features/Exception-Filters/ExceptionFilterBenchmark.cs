using BenchmarkDotNet.Attributes;

namespace Features.Exception_Filters
{
    internal class ExceptionFiltersNoFilter
    {
        public static int getSalaryToDonate(ExceptionFilterUser user)
        {
            try
            {
                return ExceptionFilters.CalculateSalsaryToDonate(user.Salary, user.SalaryPartToDonate);
            }
            catch (Exception ex)
            {
                if (user.role == Role.Admin)
                {
                    Console.WriteLine($"[LOG for Admin] {ex.GetType().Name}: {ex.Message}, user-name: {user.UserName}");
                }
                else if (ex.Message.Contains("salary-to-donate"))
                {
                    Console.WriteLine("An error occurred, but no logging due to user role. check salary-to-donate");
                }
                else
                {
                    Console.WriteLine("An error occurred, but no logging due to user role.");
                }
            }

            return 0;
        }
    }


    [MemoryDiagnoser]
    public class ExceptionFilterBenchmark
    {
        private readonly ExceptionFilterUser adminUser = new("Anup", Role.Admin, 800, 0); // will throw and log
        private readonly ExceptionFilterUser managerUser = new("Kumar", Role.Manager, 800, 0); // will throw and skip logging

        [Benchmark]
        public int WithExceptionFilter_Admin()
        {
            return ExceptionFilters.getSalaryToDonate(adminUser);
        }

        [Benchmark]
        public int WithoutExceptionFilter_Admin()
        {
            return ExceptionFiltersNoFilter.getSalaryToDonate(adminUser);
        }

        [Benchmark]
        public int WithExceptionFilter_Manager()
        {
            return ExceptionFilters.getSalaryToDonate(managerUser);
        }

        [Benchmark]
        public int WithoutExceptionFilter_Manager()
        {
            return ExceptionFiltersNoFilter.getSalaryToDonate(managerUser);
        }
    }
}
