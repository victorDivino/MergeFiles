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

            var enumerable = Directory.EnumerateFiles(searchPath, "*.txt", SearchOption.AllDirectories)
                                      .SelectMany(File.ReadLines)
                                      .Select(s => s.Trim())
                                      .Where(s => !string.IsNullOrWhiteSpace(s))
                                      .Distinct()
                                      .OrderBy(x => x)
                                      .ToArray();

            var total = enumerable.Length;

            Kurukuru.Spinner.Start("Start", spinner =>
            {
                var cont = 0.0;
                var str = new StreamWriter(fileOutput);

                foreach (var s in enumerable)
                {
                    spinner.Text = ($"{++cont / total:P}");
                    str.WriteLine(s);
                }
            });

            Console.WriteLine("Hit ENTER to open output file.");
            Console.ReadLine();
            Process.Start(fileOutput);
        }
    }
}
