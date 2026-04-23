using System;
using System.IO;

namespace CSharpPractice.IOExamples
{
    internal static partial class DirectoryExamples
    {
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
                    return Directory.GetFiles(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating directory at {path}: {ex.Message}");
            }

            return [];
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
    }
}
