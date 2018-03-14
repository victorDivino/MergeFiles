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
            string searchPath;
            string fileOutput;

            if (args.Length == 2)
            {
                searchPath = args[0];
                fileOutput = args[1];
            }
            else
            {
                Console.WriteLine("Enter search path:");
                searchPath = Console.ReadLine();

                Console.WriteLine("Enter output file path:");
                fileOutput = Console.ReadLine();
            }

            Console.WriteLine("Searching files...");

            try
            {
                var enumerable = Directory.EnumerateFiles(searchPath, "*.txt", SearchOption.AllDirectories)
                    .SelectMany(File.ReadLines)
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Distinct()
                    .OrderBy(x => x);

                Kurukuru.Spinner.Start("Start Merge", spinner =>
                {
                    var cont = 0;
                    var sw = new StreamWriter(fileOutput);

                    foreach (var s in enumerable)
                    {
                        spinner.Text = $"Lines merged: {++cont}";
                        sw.WriteLine(s);
                    }
                });

                Console.WriteLine("Hit ENTER to open output file.");
                Console.ReadLine();
                Process.Start(fileOutput);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }
        }
    }
}
