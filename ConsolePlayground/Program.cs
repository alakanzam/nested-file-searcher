using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ConsolePlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var root = Path.Combine(applicationPath, "parent-folder");

            var items = new List<string>();
            var queue = new Queue<string>();
            queue.Enqueue(root);

            while (queue.TryDequeue(out var folder))
            {
                if (string.IsNullOrEmpty(folder))
                    continue;

                var directories = Directory.GetDirectories(folder);
                var files = Directory.GetFiles(folder);
                if (files != null && files.Length > 0)
                    items.AddRange(files);

                if (directories != null && directories.Length > 0)
                {
                    foreach (var directory in directories)
                        queue.Enqueue(directory);

                    items.AddRange(directories);
                }
            }

            foreach (var item in items)
                Console.WriteLine(item);
        }
    }
}
