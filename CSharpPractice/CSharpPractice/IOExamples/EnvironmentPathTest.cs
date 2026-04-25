namespace CSharpPractice.IOExamples
{
    internal static partial class EnvironmentPathTest
    {
        public static void TestAllProperitesOFEnvironmentPath()
        {
            Console.WriteLine("CurrentDirectory: " + Environment.CurrentDirectory);
            Console.WriteLine("SystemDirectory: " + Environment.SystemDirectory);
            Console.WriteLine("UserName: " + Environment.UserName);
            Console.WriteLine("UserDomainName: " + Environment.UserDomainName);
            Console.WriteLine("GetFolderPath(SpecialFolder.Desktop): " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            Console.WriteLine("GetFolderPath(SpecialFolder.MyDocuments): " + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        public static void TestAllSpecialFolderValues()
        {
            foreach (Environment.SpecialFolder folder in Enum.GetValues(typeof(Environment.SpecialFolder)))
            {
                try
                {
                    string path = Environment.GetFolderPath(folder);
                    Console.WriteLine($"{folder}: {path}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{folder}: Error retrieving path - {ex.Message}");
                }
            }
        }

    }
}
