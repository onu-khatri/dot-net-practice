using System.IO;
using System.Text;

namespace CSharpPractice.IOExamples
{
    internal static partial class FileCreateUpdate
    {
        public static byte[] GenerateCsvInMemory()
        {
            using var memoryStream = new MemoryStream();

            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
            {
                writer.WriteLine("Id,Name,Amount");
                writer.WriteLine("1,Anup,1200");
                writer.WriteLine("2,Rahul,2500");
                writer.WriteLine("3,Neha,1800");
                writer.Flush();
            }

            return memoryStream.ToArray();
            // The caller can then write this byte array to a file or use it as needed
        }

        public static void SaveUploadedTextWithHeader(string filePath, string uploadedContent)
        {
            using var memoryStream = new MemoryStream();

            using (var writer = new StreamWriter(memoryStream, leaveOpen: true))
            {
                writer.WriteLine("=== Uploaded File Header ===");
                writer.WriteLine(uploadedContent);
                writer.Flush();
            }

            memoryStream.Position = 0;

            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            memoryStream.CopyTo(fileStream);

            // Prepare or transform uploaded/generated content in memory before saving to disk or sending to another service.
        }

        /*
         * Easy rule to remember
                BufferedStream = improve reading/writing performance for another stream, A bucket placed in front of the pipe, so instead of sending tiny drops one by one, it sends a fuller batch.
                MemoryStream = keep data in RAM temporarily, It exists because sometimes you do not want to write to a file yet. you need a Stream object but want to avoid disk I/O
            
            When not to use them
                Do not use MemoryStream for very large files unless you really need whole content in memory
                Do not add BufferedStream blindly when FileStream or StreamWriter buffering is already enough
        */
    }
}
