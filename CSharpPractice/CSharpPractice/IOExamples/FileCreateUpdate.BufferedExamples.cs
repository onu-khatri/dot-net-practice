using System;
using System.IO;
using System.Text;

namespace CSharpPractice.IOExamples
{
    internal static partial class FileCreateUpdate
    {
        public static void AppendLineToFile(string filePath, string content)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
            ArgumentNullException.ThrowIfNull(content);

            const int bufferSize = 16 * 1024;

            using var fileStream = new FileStream(
                filePath,
                FileMode.Append,
                FileAccess.Write,
                FileShare.Read,
                bufferSize,
                FileOptions.SequentialScan);

            using var writer = new StreamWriter(fileStream);

            writer.WriteLine(content);
        }

        public static void WriteLargeReport(string filePath)
        {
            using var fileStream = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None);

            using var bufferedStream = new BufferedStream(fileStream, 16 * 1024);
            using var writer = new StreamWriter(bufferedStream, Encoding.UTF8);

            for (int i = 1; i <= 100_000; i++)
            {
                writer.WriteLine($"Row {i}: report data generated at {DateTime.UtcNow:O}");
            }
        }

        public static void ReadLargeFile(string filePath)
        {
            byte[] buffer = new byte[4096];

            using var fileStream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);

            using var bufferedStream = new BufferedStream(fileStream, 16 * 1024);

            int bytesRead;
            while ((bytesRead = bufferedStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                // Process bytes here
                Console.WriteLine($"Read {bytesRead} bytes");
                // Process the buffer as needed, for example, convert to string and display
                Console.WriteLine(bytesRead);
                Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
        }

       /* So use BufferedStream mainly when:

            you are writing a very large file
            you are doing many tiny writes
            profiling / testing shows benefit
            you want explicit control over buffer size

            Rule of thumb
                normal text file writing → FileStream + StreamWriter
                huge output with many small writes → FileStream + BufferedStream + StreamWriter
       */
    }
}
