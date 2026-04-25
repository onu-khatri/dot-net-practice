using CSharpPractice.ObjectTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSharpPractice.IOExamples
{
    internal static class JsonSerializerTest
    {
        public static string SerializeSalaryAccountToJson()
        {
            var salaryAccount = new SalaryAccount()
            {
                AccountHolderName = "John Doe",
                AccountNumber = "123456789",
                BankName = "Example Bank",
                Balance = 5000.00m,
                LastSalaryCreditedOn = DateOnly.Parse("2024-06-01")
            };

            string jsonString = JsonSerializer.Serialize(salaryAccount);
            Console.WriteLine("Serialized JSON:");
            Console.WriteLine(jsonString);

            // create object JsonSerializerOption to demostrate all possible options
            var options = new JsonSerializerOptions
            {
                WriteIndented = false, // for pretty printing
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // use camelCase for property names
                DefaultIgnoreCondition = JsonIgnoreCondition.Always, // ignore null values
                IncludeFields = true, // include fields in serialization
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // allow special characters without escaping
            };

            Console.WriteLine("Serialized JSON with options:");
            string jsonStringWithOptions = JsonSerializer.Serialize(salaryAccount, options);
            Console.WriteLine(jsonStringWithOptions);

            return jsonString;
        }

        public static void saveSalaryAccountJsonToFile(string filePath)
        {
            string jsonString = SerializeSalaryAccountToJson();
            System.IO.File.WriteAllText(filePath, jsonString);
            Console.WriteLine($"JSON data has been saved to {filePath}");
        }

        public async static ValueTask<SalaryAccount?> deserializeSalaryFromSaveJsonFile(string filePath)
        {
            // use stream to read the file and deserialize the json data to SalaryAccount object with right JsonSerializerOptions
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // ignore case when matching property names
                IncludeFields = true // include fields in deserialization
            };

            using (var stream = System.IO.File.OpenRead(filePath))
            {
                var salaryAccount = await JsonSerializer.DeserializeAsync<SalaryAccount>(stream, options);
                return salaryAccount;
            }
        }
    }
    }