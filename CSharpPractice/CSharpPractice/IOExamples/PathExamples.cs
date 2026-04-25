namespace CSharpPractice.IOExamples
{
    internal static partial class PathExamples
    {
        public static void TestTheUsageOfPathClass()
        {
            string filePath = "d:\\Users\\JohnDoe\\Documents\\file.txt";
            string combinedPath = Path.Combine("d:\\Users", "JohnDoe", "Documents");

            Console.WriteLine($"Combine: {combinedPath}");
            Console.WriteLine($"GetDirectoryName: {Path.GetDirectoryName(filePath)}");
            Console.WriteLine($"GetFileName: {Path.GetFileName(filePath)}");
            Console.WriteLine($"GetExtension: {Path.GetExtension(filePath)}");
            Console.WriteLine($"GetFileNameWithoutExtension: {Path.GetFileNameWithoutExtension(filePath)}");
            Console.WriteLine($"IsPathRooted: {Path.IsPathRooted(filePath)}");

            string changedExtensionPath = Path.ChangeExtension(filePath, ".log");
            Console.WriteLine($"ChangeExtension: {changedExtensionPath}");
            Console.WriteLine($"GetPathRoot: {Path.GetPathRoot(filePath)}");
            Console.WriteLine($"HasExtension: {Path.HasExtension(filePath)}");
            Console.WriteLine($"GetFullPath: {Path.GetFullPath(filePath)}");

            string relativePath = Path.GetRelativePath("d:\\Users\\JohnDoe", filePath);
            Console.WriteLine($"GetRelativePath: {relativePath}");

            string withTrailingSeparator = "d:\\Users\\JohnDoe\\Documents\\";
            Console.WriteLine($"TrimEndingDirectorySeparator: {Path.TrimEndingDirectorySeparator(withTrailingSeparator)}");

            Console.WriteLine($"GetTempPath: {Path.GetTempPath()}");
            Console.WriteLine($"GetRandomFileName: {Path.GetRandomFileName()}");

            Console.WriteLine($"DirectorySeparatorChar: {Path.DirectorySeparatorChar}");
            Console.WriteLine($"AltDirectorySeparatorChar: {Path.AltDirectorySeparatorChar}");
            Console.WriteLine($"PathSeparator: {Path.PathSeparator}");
        }

        public static void ExistsMethodOfPathFileAndDirectory()
        {
            string filePath = "d:\\Users\\JohnDoe\\Documents\\file.txt";
            string directoryPath = "d:\\Users\\JohnDoe\\Documents";
            Console.WriteLine($"Does the file exist? {File.Exists(filePath)}");
            Console.WriteLine($"Does the directory exist? {Directory.Exists(directoryPath)}");

            Console.WriteLine($"Does the directory exist? {Directory.Exists(filePath)}");

            Console.WriteLine($"Path.Exists(filePath): {Path.Exists(filePath)}");
            Console.WriteLine($"Path.Exists(directoryPath): {Path.Exists(directoryPath)}");
        }
    }
}
