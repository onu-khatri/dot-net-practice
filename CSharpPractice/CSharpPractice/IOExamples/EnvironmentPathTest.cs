using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
//using static System.Environment;

namespace CSharpPractice.IOExamples
{
    internal static class EnvironmentPathTest
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

        public static void PrintNthLevelDirectoryFolderNames(string path, int n)
        {
            if (n < 0)
            {
                Console.WriteLine("Level must be non-negative.");
                return;
            }

            try
            {
                string[] directories = Directory.GetDirectories(path);
                if (n == 0)
                {
                    Console.WriteLine($"Directories at level {n} in {path}:");
                    foreach (string dir in directories)
                    {
                        Console.WriteLine(dir);
                    }
                }
                else
                {
                    foreach (string dir in directories)
                    {
                        PrintNthLevelDirectoryFolderNames(dir, n - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing path {path}: {ex.Message}");
            }
        }

        public static string[] TakePathAndCreateIfNotExist(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine($"Directory created at: {path}");
                }
                else
                {
                    Console.WriteLine($"Directory already exists at: {path}");
                    // If the directory already exists, we can return its contents
                    return Directory.GetFiles(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating directory at {path}: {ex.Message}");
            }

            return new string[0]; // Return an empty array if directory creation fails
        }

        public static void TestTheUsageOfPathClass()
        {
            Path.Combine("d:\\Users", "JohnDoe", "Documents");
            Path.GetDirectoryName("d:\\Users\\JohnDoe\\Documents\\file.txt");
            Path.GetFileName("d:\\Users\\JohnDoe\\Documents\\file.txt");
            Path.GetExtension("d:\\Users\\JohnDoe\\Documents\\file.txt");
            Path.GetFileNameWithoutExtension("d:\\Users\\JohnDoe\\Documents\\file.txt");
            Path.IsPathRooted("d:\\Users\\JohnDoe\\Documents\\file.txt");

            //Seperators
            Console.WriteLine("DirectorySeparatorChar: " + Path.DirectorySeparatorChar);
            Console.WriteLine("AltDirectorySeparatorChar: " + Path.AltDirectorySeparatorChar);
            Console.WriteLine("" + Path.PathSeparator);
        }

        public static void GetAllInfoOfCDrive()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Drive Name: {drive.Name}");
                Console.WriteLine($"Drive Type: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Volume Label: {drive.VolumeLabel}");
                    Console.WriteLine($"File System: {drive.DriveFormat}");
                    Console.WriteLine($"Available Free Space: {drive.AvailableFreeSpace} bytes");
                    Console.WriteLine($"Total Free Space: {drive.TotalFreeSpace} bytes");
                    Console.WriteLine($"Total Size: {drive.TotalSize} bytes");
                }
                else
                {
                    Console.WriteLine("Drive is not ready.");
                }
                Console.WriteLine();
            }
        }

        public static void ExistsMethodOfPathFileAndDirectory()
        {
            string filePath = "d:\\Users\\JohnDoe\\Documents\\file.txt";
            string directoryPath = "d:\\Users\\JohnDoe\\Documents";
            Console.WriteLine($"Does the file exist? {File.Exists(filePath)}");
            Console.WriteLine($"Does the directory exist? {Directory.Exists(directoryPath)}");

            Console.WriteLine($"Does the directory exist? {Directory.Exists(filePath)}");

            Console.WriteLine(Path.Exists(filePath));
            Console.WriteLine(Path.Exists(directoryPath));
        }
    }
}
