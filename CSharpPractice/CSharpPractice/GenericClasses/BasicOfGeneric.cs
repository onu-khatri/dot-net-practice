namespace CSharpPractice.GenericClasses
{
    interface IEmployee
    {
        public int Id { get; set; }
        public string GetEmployeeInfo();
    }

    public class Teacher : IEmployee
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string GetEmployeeInfo()
        {
            return $"Name: {Name}, Id: {Id}";
        }
    }

    public class Resecption : IEmployee
    {
        public Resecption() { }
        public Resecption(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public int Id { get; set; }
        public string? FullName { get; set; }

        public string GetEmployeeInfo()
        {
            return $"FullName: {FullName}, Id: {Id}";
        }
    }

    internal class BasicOfGeneric<T> where T : IEmployee, new()
    {
        private readonly List<T> _employees = new List<T> { };

        public void AddEmployee(T employee)
        {
            _employees.Add(employee);
        }

        public void AddDummyEmployee(int Id)
        {
            var employee = new T() { Id = Id };
            _employees.Add(employee);
        }

        public void ShowEmployeesInfo()
        {
            foreach (var employee in _employees)
            {
                Console.WriteLine(employee.GetEmployeeInfo());
            }
        }

        public T? GetEmployee(int id)
        {
            return _employees.FirstOrDefault(t => t.Id == id);
        }
    }
}
