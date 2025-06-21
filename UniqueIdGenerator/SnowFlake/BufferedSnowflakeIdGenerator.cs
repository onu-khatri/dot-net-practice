using NUlid;
using System.Collections.Concurrent;

public class BufferedSnowflakeIdGenerator
{
    private readonly long _workerId;
    private readonly long _datacenterId;
    private readonly bool _useFallbackIfBusy;

    private const int SequenceBits = 12;
    private const int WorkerIdBits = 5;
    private const int DatacenterIdBits = 5;

    private const long MaxSequence = (1 << SequenceBits) - 1;

    private const int WorkerIdShift = SequenceBits;
    private const int DatacenterIdShift = SequenceBits + WorkerIdBits;
    private const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

    private static readonly DateTime _epoch = new(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    private long _lastTimestamp = -1;
    private ConcurrentQueue<long> _sequenceBuffer = new();

    public BufferedSnowflakeIdGenerator(long datacenterId, long workerId, bool useFallbackIfBusy = true)
    {
        _datacenterId = datacenterId;
        _workerId = workerId;
        _useFallbackIfBusy = useFallbackIfBusy;
    }

    public long GenerateId()
    {
        long timestamp = GetCurrentTimestamp();

        if(detectClockRollback(timestamp, out long id))
        {
            return id;
        }

        RefillSequenceIfNextMillisecond(timestamp);
        long sequence = GetSequenceAndWaitForNextMillisecondIfNoSequenceLeft(ref timestamp);

        return ((timestamp << TimestampLeftShift)
                | (_datacenterId << DatacenterIdShift)
                | (_workerId << WorkerIdShift)
                | sequence);

    }

    private bool detectClockRollback(long timestamp, out long id)
    {
        id = 0;
        if (timestamp < _lastTimestamp)
        {
            if (_useFallbackIfBusy)
            {
                id = BitConverter.ToInt64(Ulid.NewUlid().ToByteArray(), 0);
                return true;
            }

            throw new InvalidOperationException("Clock moved backwards.");
        }

        return false;
    }

    private long GetSequenceAndWaitForNextMillisecondIfNoSequenceLeft(ref long timestamp)
    {
        if (!_sequenceBuffer.TryDequeue(out long sequence))
        {
            // Wait until next ms
            timestamp = WaitNextMillis(_lastTimestamp);
            Interlocked.Exchange(ref _lastTimestamp, timestamp);
            var newQueue = new ConcurrentQueue<long>(Enumerable.Range(0, (int)MaxSequence + 1).Select(i => (long)i));
            Interlocked.Exchange(ref _sequenceBuffer, newQueue);

            _sequenceBuffer.TryDequeue(out sequence);
        }

        return sequence;
    }

    private void RefillSequenceIfNextMillisecond(long timestamp)
    {
        if (timestamp != _lastTimestamp)
        {
            Interlocked.Exchange(ref _lastTimestamp, timestamp);
            var newQueue = new ConcurrentQueue<long>(Enumerable.Range(0, (int)MaxSequence + 1).Select(i => (long)i));
            Interlocked.Exchange(ref _sequenceBuffer, newQueue);
        }
    }

    private static long GetCurrentTimestamp()
        => (long)(DateTime.UtcNow - _epoch).TotalMilliseconds;

    private long WaitNextMillis(long lastTimestamp)
    {
        long timestamp = GetCurrentTimestamp();
        while (timestamp <= lastTimestamp)
        {
            Thread.SpinWait(1);
            timestamp = GetCurrentTimestamp();
        }
        return timestamp;
    }
}
