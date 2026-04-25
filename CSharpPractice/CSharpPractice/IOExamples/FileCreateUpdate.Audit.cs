using System.Text;

namespace CSharpPractice.IOExamples
{
    internal static partial class FileCreateUpdate
    {
        public sealed class AuditEntry
        {
            public DateTime TimestampUtc { get; init; }
            public string UserName { get; init; } = string.Empty;
            public string Action { get; init; } = string.Empty;
            public string EntityId { get; init; } = string.Empty;
        }

        public static void WriteAuditDump(string filePath, IEnumerable<AuditEntry> entries)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
            ArgumentNullException.ThrowIfNull(entries);

            const int bufferSize = 32 * 1024;

            using var fileStream = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.Read, bufferSize);

          //  using var bufferedStream = new BufferedStream(fileStream, bufferSize);
            using var writer = new StreamWriter(fileStream, Encoding.UTF8);

            foreach (var entry in entries)
            {
                writer.Write(entry.TimestampUtc.ToString("O"));
                writer.Write(" | ");
                writer.Write(entry.UserName);
                writer.Write(" | ");
                writer.Write(entry.Action);
                writer.Write(" | ");
                writer.WriteLine(entry.EntityId);
            }
        }
    }
}
