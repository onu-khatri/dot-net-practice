namespace CSharpPractice.Unsafe
{
    unsafe struct Person
    {
        public fixed char name[50];
    }

    internal class Pointer
    {
        delegate int AddDelegate(int a, int b);

        public unsafe void PointerExample()
        {
            int number = 42;
            int* pointerToNumber = &number;
            Console.WriteLine("Value of number: " + number);
            Console.WriteLine("Address of number: " + (IntPtr)pointerToNumber);
            Console.WriteLine("Value at pointer: " + *pointerToNumber);
            // Modifying the value through the pointer
            *pointerToNumber = 100;
            Console.WriteLine("Modified value of number: " + number);
        }

        public unsafe void UserFixedBuffer()
        {
            Person person;
            string name = "John Doe";
            for (int i = 0; i < name.Length && i < 50; i++)
            {
                person.name[i] = name[i];
            }
            Console.WriteLine("Person's name: " + new string(person.name));

            char* namePtr = person.name;
            Console.WriteLine("Person's name using pointer: " + new string(namePtr));
        }

        public unsafe void VoidPointerExample()
        {
            void* voidPtr = null;
            int number = 42;
            voidPtr = &number;
            Console.WriteLine("Value of number: " + number);
            Console.WriteLine("Address of number: " + (IntPtr)voidPtr);
            // To read the value, we need to cast the void pointer back to the appropriate type
            int* intPtr = (int*)voidPtr;
            Console.WriteLine("Value at void pointer: " + *intPtr);
        }

        public static int AddValues(int a, int b)
        {
            return a + b;
        }

        public void pointerToMethod()
        {
            unsafe
            {
                delegate*<int, int, int> addPtr = &AddValues;
                var sum = addPtr(5, 10);
                Console.WriteLine("Sum using function pointer: " + sum);
            }

            Func<int, int, int> addFunc = AddValues;
            var sumFunc = addFunc(5, 10);
            Console.WriteLine("Sum using Func delegate: " + sumFunc);

            AddDelegate addFuncDelegate = AddValues;
            var sumFuncDelegate = addFuncDelegate(5, 10);
            Console.WriteLine("Sum using delegate: " + sumFuncDelegate);
        }
    }
}
