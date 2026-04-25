using System.Text;
using System.IO.Compression;

namespace CSharpPractice.IOExamples
{
    internal class FileCompressionTest
    {
        public static void TestFileCompression()
        {
            string sourceFile = "c:/example.txt";
            string compressedFile = "example.zip";
            // Create a sample file to compress
            File.WriteAllText(sourceFile, "This is an example file to demonstrate compression.");
            // Compress the file
            using (var zipArchive = ZipFile.Open(compressedFile, ZipArchiveMode.Create))
            {
                zipArchive.CreateEntryFromFile(sourceFile, Path.GetFileName(sourceFile));
            }
            Console.WriteLine($"File '{sourceFile}' has been compressed to '{compressedFile}'.");
            // Clean up the sample file
            File.Delete(sourceFile);
        }

        public static void TestFileDecompression()
        {
            string compressedFile = "example.zip";
            string extractPath = "extracted";
            // Ensure the compressed file exists
            if (File.Exists(compressedFile))
            {
                // Extract the contents of the zip file
                ZipFile.ExtractToDirectory(compressedFile, extractPath);
                Console.WriteLine($"File '{compressedFile}' has been extracted to '{extractPath}'.");
            }
            else
            {
                Console.WriteLine($"Compressed file '{compressedFile}' does not exist.");
            }
        }

        public static void GzipCompressData(string fileName)
        {
            string data = "This is some sample data to be compressed using GZip.";
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            using (var gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
            {
                gzipStream.Write(dataBytes, 0, dataBytes.Length);
            }
            Console.WriteLine($"Data has been compressed and saved to '{fileName}'.");
        }

        public static void CompressLargeListOfStrings(string fileName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

            using var fileStream = new FileStream(
                fileName,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None);

            using var gzipStream = new GZipStream(
                fileStream,
                CompressionLevel.Optimal);

            using var writer = new StreamWriter(gzipStream, Encoding.UTF8);

            for (int i = 0; i < 1000; i++)
            {
                writer.WriteLine($"This is line {i + 1}");
            }

            Console.WriteLine($"Large list of strings has been compressed and saved to '{fileName}'.");
        }

        public static void GzipDecompressData(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (var fileStream = new FileStream(fileName, FileMode.Open))
                using (var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                using (var reader = new StreamReader(gzipStream))
                {
                    string decompressedData = reader.ReadToEnd();
                    Console.WriteLine($"Decompressed data from '{fileName}': {decompressedData}");
                }
            }
            else
            {
                Console.WriteLine($"Compressed file '{fileName}' does not exist.");
            }
        }
    }
}
