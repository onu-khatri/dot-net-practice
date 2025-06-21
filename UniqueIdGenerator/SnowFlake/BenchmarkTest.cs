using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueIdGenerator.SnowFlake
{
    internal class BenchmarkTest
    {
        public static void RunBenchmark()
        {
            var manager = new SnowflakeManager(32);
            int totalThreads = 100;
            int opsPerThread = 100;
            var sw = Stopwatch.StartNew();
            var allIds = new ConcurrentBag<long>();

            Parallel.For(0, totalThreads, _ =>
            {
                for (int i = 0; i < opsPerThread; i++)
                {
                    var id = manager.GenerateNumbericId();
                    allIds.Add(id);
                }
            });

            sw.Stop();
            Console.WriteLine($"Generated {totalThreads * opsPerThread} IDs in {sw.ElapsedMilliseconds}ms");

            var duplicates = allIds.GroupBy(x => x).Where(g => g.Count() > 1).ToList();
            Console.WriteLine($"Duplicates found: {duplicates.Count}");
        }
    }
}
