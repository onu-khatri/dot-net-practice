namespace CSharpPractice.IOExamples
{
    internal static partial class FileCreateUpdate
    {
        public static void CreateOrUpdateFile(string filePath, string content)
        {
            try
            {
                // Check if the file exists
                if (System.IO.File.Exists(filePath))
                {
                    // If it exists, append the new content
                    System.IO.File.AppendAllText(filePath, content + Environment.NewLine);
                    Console.WriteLine($"Content appended to existing file: {filePath}");
                }
                else
                {
                    // If it does not exist, create a new file and write the content
                    System.IO.File.WriteAllText(filePath, content + Environment.NewLine);
                    Console.WriteLine($"New file created with content: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating/updating the file: {ex.Message}");
            }
        }

        public static void DeleteFile(string filePath)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    Console.WriteLine($"File deleted: {filePath}");
                }
                else
                {
                    Console.WriteLine($"File does not exist: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the file: {ex.Message}");
            }
        }

        public static void ReadFile(string filePath)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    string content = System.IO.File.ReadAllText(filePath);
                    Console.WriteLine($"Content of the file {filePath}:\n{content}");
                }
                else
                {
                    Console.WriteLine($"File does not exist: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
        }

        public static void ReadFileUsingStreamReader(string filePath)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                  //  var outerReader = System.IO.File.OpenText(filePath);

                    using (var reader = new System.IO.StreamReader(filePath))
                    {
                        string? line;
                        Console.WriteLine($"Content of the file {filePath}:");
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"File does not exist: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
        }

        public static void WriteFileUsingStreamWriter(string filePath, string content)
        {
            try
            {
                using (var writer = File.AppendText(filePath)) //new System.IO.StreamWriter(filePath, append: true))
                {
                    writer.WriteLine(content);
                    Console.WriteLine($"Content written to file: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }

            /*
            using (var writer = File.AppendText(filePath)) //new System.IO.StreamWriter(filePath, append: true))
            writer.WriteLine(content);
            Console.WriteLine($"Content written to file: {filePath}");
            */
            

        }

        public static void WriteFileUsingStreamWriterClose(string filePath, string content)
        {
            StreamWriter? writer = null;
            try
            {
                writer = File.AppendText(filePath); //new System.IO.StreamWriter(filePath, append: true))
                
                    writer.WriteLine(content);
                    Console.WriteLine($"Content written to file: {filePath}");

                //    writer.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }
            finally
            {
                writer?.Close();
            }

        }

        public static void CreateOrUpdateFileUsingFileStream(string filePath, string content)
        {
            try
            {
                using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write))
                {
                    fileStream.Seek(0, System.IO.SeekOrigin.End); // Move to the end of the file for appending
                    using (var writer = new System.IO.StreamWriter(fileStream))
                    {
                        writer.WriteLine(content);
                        Console.WriteLine($"Content written to file using FileStream: {filePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating/updating the file using FileStream: {ex.Message}");
            }
        }

    }
}