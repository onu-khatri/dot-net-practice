namespace CSharpPractice.ObjectTypes
{
    internal record Person(string Name, int Age);
    internal record struct Point(int X, int Y);
    internal record class Employee(string Name, int Age, string Position);

    internal struct Square {         
        public int SideLengthA { get; set; }
        public int SideLengthB { get; set; }

        public Square(int sideLengthA, int sideLengthB)
        {
            SideLengthA = sideLengthA;
            SideLengthB = sideLengthB;
        }
    }

    internal class SquareType
    {
        public int SideLengthA { get; set; }
        public int SideLengthB { get; set; }

        public SquareType() { }
        public SquareType(int _sideLengthA, int _sideLengthB)
        {
            SideLengthA = _sideLengthA;
            SideLengthB = _sideLengthB;
        }

        public int GetArea() => SideLengthA * SideLengthB;
    }

    internal class SquareSmart(int sideLengthA, int sideLengthB)
    {
        public int GetArea() => sideLengthA * sideLengthB;
    }



    internal static class RecordTesting
    {
        public static void Test()
        {
            Person person1 = new("Alice", 30);
            var (name, age) = person1; // Deconstructing the record into its components

            var person2 = new Person("Alice", 30);
            Console.WriteLine(person1 == person2); // True, because records compare by value

            var point1 = new Point(1, 2);
            var point2 = new Point(1, 2);
            Console.WriteLine(point1 == point2); // True, because record structs also compare by value

            var employee1 = new Employee("Bob", 40, "Manager");
            var employee2 = new Employee("Bob", 40, "Manager");
            Console.WriteLine(employee1 == employee2); // True, because record classes also compare by value

        }

        public static void TestTypeOfAndPeropertiesOfAllRecords()
        {
            var person = new Person("Charlie", 25);
            var point = new Point(3, 4);
            var employee = new Employee("Dave", 35, "Developer");
            Console.WriteLine($"Type of person: {person.GetType()}");
            Console.WriteLine($"Type of point: {point.GetType()}");
            Console.WriteLine($"Type of employee: {employee.GetType()}");
        }

        public static void StuctVSClass()
        {
            var squareStruct = new Square(5, 5);
            var squareClass = new SquareType(5, 5);
            Console.WriteLine($"Square struct: SideLengthA = {squareStruct.SideLengthA}, SideLengthB = {squareStruct.SideLengthB}");
            Console.WriteLine($"Square class: SideLengthA = {squareClass.SideLengthA}, SideLengthB = {squareClass.SideLengthB}");

            Square sq = new Square();
            sq.SideLengthA = 5;
            sq.SideLengthB = 5;

            SquareType sqType = new SquareType();
            sq.SideLengthA = 5;
            sq.SideLengthB = 5;
        }
    }
}
