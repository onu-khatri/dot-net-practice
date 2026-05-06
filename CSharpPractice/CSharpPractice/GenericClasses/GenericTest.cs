using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPractice.GenericClasses
{
    internal static class GenericTest
    {
        public static void TestGeneric()
        {
            var teacherManager = new BasicOfGeneric<Teacher>();
            teacherManager.AddEmployee(new Teacher { Name = "Alice", Id = 1 });
            teacherManager.AddEmployee(new Teacher { Name = "Bob", Id = 2 });
            Console.WriteLine("Teachers:");
            teacherManager.ShowEmployeesInfo();

            var receptionManager = new BasicOfGeneric<Resecption>();
            receptionManager.AddEmployee(new Resecption { FullName = "Charlie", Id = 3 });
            receptionManager.AddEmployee(new Resecption { FullName = "Dave", Id = 4 });
            Console.WriteLine("\nReceptionists:");
            receptionManager.ShowEmployeesInfo();

           var employee = receptionManager.GetEmployee(3);
            if (employee != null)
            {
                Console.WriteLine(employee.FullName);
            }
        }
    }
}
