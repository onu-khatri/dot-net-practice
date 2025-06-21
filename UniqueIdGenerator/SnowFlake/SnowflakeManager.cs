using NUlid;

public class SnowflakeManager
{
    private readonly BufferedSnowflakeIdGenerator[] _shards;
    private readonly int _shardCount;
    private readonly bool _useFallbackIfBusy;

    public SnowflakeManager(int shardCount = 32, long datacenterId = 1, bool useFallbackIfBusy = true)
    {
        _shardCount = shardCount;
        _shards = new BufferedSnowflakeIdGenerator[shardCount];
        _useFallbackIfBusy = useFallbackIfBusy;

        for (int i = 0; i < shardCount; i++)
        {
            _shards[i] = new BufferedSnowflakeIdGenerator(datacenterId, i);
        }
    }

    public long GenerateNumbericId()
    {
        int index = Environment.CurrentManagedThreadId % _shardCount;
        return _shards[index].GenerateId();
    }

    public string GenerateTextId()
    {
        try
        {
            int index = Environment.CurrentManagedThreadId % _shardCount;
            var id = _shards[index].GenerateId();

            return ToSortableBase64(id);
        }
        catch
        {
            if (_useFallbackIfBusy)
            {
                return Ulid.NewUlid().ToString(); // Fallback to ULID
            }

            throw;
        }

    }

    public static long ConvertTextIDToNumbericId(string id)
    {
        return FromSortableBase64(id);
    }

    private static string ToSortableBase64(long id)
    {
        var bytes = BitConverter.GetBytes(id);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes); // Ensure big-endian for sorting

        return Convert.ToBase64String(bytes)
            .TrimEnd('=') // optional: remove padding
            .Replace('+', '-') // make URL-safe
            .Replace('/', '_');
    }

    private static long FromSortableBase64(string base64)
    {
        base64 = base64
            .Replace('-', '+')
            .Replace('_', '/');

        while (base64.Length % 4 != 0)
            base64 += "=";

        var bytes = Convert.FromBase64String(base64);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);

        return BitConverter.ToInt64(bytes, 0);
    }
}
