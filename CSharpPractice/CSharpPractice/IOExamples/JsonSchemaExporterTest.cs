using CSharpPractice.ObjectTypes;
using System.Text.Json.Schema;

namespace CSharpPractice.IOExamples
{
    internal static class JsonSchemaExporterTest
    {
        public static void TestJsonSchemaExporter()
        {
            // 'JsonSerializerOptions instance must specify a TypeInfoResolver setting before being marked as read-only.'
            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                IncludeFields = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                TypeInfoResolver = new System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver()
            };
            
            var node = JsonSchemaExporter.GetJsonSchemaAsNode(options, typeof(SalaryAccount));
            string jsonSchema = node.ToJsonString(options);
            Console.WriteLine(jsonSchema);
        }
    }
}
