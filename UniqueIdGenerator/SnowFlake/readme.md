# ❄️ Snowflake ID Generator for High-Concurrency .NET Systems

A high-performance, lock-free implementation of Twitter's Snowflake algorithm for generating **globally unique**, **time-ordered**, 64-bit IDs in distributed and multi-threaded .NET applications.

This system includes:

- `BufferedSnowflakeShard`: Lock-free, sequence-buffered ID generator for one worker
- `SnowflakeShardManager`: Sharded manager that distributes ID generation across multiple workers/threads

---

## 💡 Why Use This?

- 100% **thread-safe**
- **No locks**, uses `ConcurrentQueue` + `Interlocked`
- Designed for **100k+ ID/sec throughput**
- Works in **multi-threaded** or **multi-instance** distributed systems
- Produces **sortable 64-bit integer IDs**

---

## 🔧 ID Structure (64-bit)
| 1-bit unused | 41-bit timestamp | 5-bit datacenter ID | 5-bit worker ID | 12-bit sequence |


- Timestamp: milliseconds since custom epoch (e.g. Jan 1, 2020)
- Sequence: auto-resets every millisecond
- Supports up to:
  - **4096 IDs/ms per shard**
  - **1024 shards** (datacenter + worker)
  - **69 years** of unique timestamps

---

## 🧱 Components

### 1. `BufferedSnowflakeIdGenerator`

Generates IDs for a single (datacenterId, workerId) combo.

- Buffers all sequence numbers per millisecond
- Lock-free and very fast
- Throws no exceptions even when the sequence rolls over — waits for the next ms

### 2. `SnowflakeManager`

A manager that holds N instances of `BufferedSnowflakeShard`, distributes generation across shards using thread ID or round-robin strategy.

- Provides a single `.GenerateId()` method
- Thread-safe and scalable
- Ideal for high-throughput web APIs

---

## 🚀 How to Use

### Register in `Program.cs`

```csharp
builder.Services.AddSingleton(new SnowflakeShardManager(shardCount: 32, datacenterId: 1));
```

Each workerId in the shard manager is automatically assigned from 0 to shardCount-1.

```csharp
[ApiController]
[Route("api/id")]
public class IdController : ControllerBase
{
    private readonly SnowflakeShardManager _manager;

    public IdController(SnowflakeShardManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IActionResult GetId()
    {
        var id = _manager.GenerateId();
        return Ok(id);
    }
}
```

⚡ Benchmark Example
```csharp
public static void RunBenchmark()
{
    var manager = new SnowflakeShardManager(32);
    int threads = 100;
    int opsPerThread = 100;
    var ids = new ConcurrentBag<long>();
    var sw = Stopwatch.StartNew();

    Parallel.For(0, threads, _ =>
    {
        for (int i = 0; i < opsPerThread; i++)
        {
            ids.Add(manager.GenerateId());
        }
    });

    sw.Stop();
    Console.WriteLine($"Generated {threads * opsPerThread} IDs in {sw.ElapsedMilliseconds}ms");

    var duplicates = ids.GroupBy(x => x).Where(g => g.Count() > 1).ToList();
    Console.WriteLine($"Duplicates found: {duplicates.Count}");
}
```

🧪 Output Example
````
Generated 10000 IDs in 22ms
Duplicates found: 0
````

## 🔒 Thread-Safety Notes

- All operations use `ConcurrentQueue` and `Interlocked`, ensuring **lock-free, thread-safe** execution.
- Sequence buffer is **refilled only once per millisecond**, minimizing overhead.
- Supports **100+ threads concurrently** without race conditions or duplicated IDs.
- ID generation is **guaranteed to be unique and ordered** across all shards.

---

## 🏗️ Future Improvements

- [ ] Redis-backed coordination for global sequence sharing across distributed services
- [ ] Base62 / Base36 encoding for shorter, URL-friendly IDs
- [ ] Structured logging and observability (e.g., OpenTelemetry, Serilog)
- [ ] Package as a reusable, configurable **NuGet library**
- [ ] Optional fallback to time-based GUIDs or ULIDs for overflow conditions