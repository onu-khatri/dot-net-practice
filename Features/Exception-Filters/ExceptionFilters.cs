using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Exception_Filters
{
    public enum Role { 
        Admin,
        Manager,
        User,
        Guest
    }

    public record ExceptionFilterUser(string UserName, Role role, int Salary,int SalaryPartToDonate);

    internal class ExceptionFilters
    {
        public static int getSalaryToDonate(ExceptionFilterUser exceptionFilterUser)
        {
            try
            {
                return CalculateSalsaryToDonate(exceptionFilterUser.Salary, exceptionFilterUser.SalaryPartToDonate);
            }
            catch (Exception ex) when (exceptionFilterUser.role == Role.Admin)
            {
                Console.WriteLine($"[LOG for Admin] {ex.GetType().Name}: {ex.Message}, user-name: {exceptionFilterUser.UserName}");
            } catch (Exception ex) when(ex.Message.Contains("salary-to-donate")) {
                Console.WriteLine("An error occurred, but no logging due to user role. check salary-to-donate");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("An error occurred, but no logging due to user role.");
            }

            return 0;
        }

        public static int CalculateSalsaryToDonate(int Salary, int salaryToDonate) {
            if (salaryToDonate == 0)
                throw new DivideByZeroException("salary-to-donate can't be zero");

            if (Salary < 1000)
                throw new InvalidOperationException("Salary can't be less than 1000");

            return Salary / salaryToDonate;
        }
    }

    
}
