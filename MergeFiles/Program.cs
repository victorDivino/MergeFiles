using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MergeFiles
{
    static class Program
    {
        static void Main(string[] args)
        {
            var searchPath = args[0];
            var fileOutput = args[1];

            if (File.Exists(fileOutput))
                File.Delete(fileOutput);

            Console.WriteLine("Searching files");

            var files = Directory.EnumerateFiles(searchPath, "*.txt", SearchOption.AllDirectories)
                .Select(fn => new FileInfo(fn)).Where(fi => fi.Length > 0);

            Console.WriteLine("Start Merge");

            foreach (var file in files)
            {
                Console.WriteLine($"Merged: {file.FullName}");
                File.AppendAllLines(fileOutput, File.ReadLines(file.FullName));
            }

            Console.WriteLine("Finished");
            Console.WriteLine("Hit ENTER to open output file.");
            Console.ReadLine();
            Process.Start(fileOutput);
        }
    }
}
