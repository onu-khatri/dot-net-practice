  namespace CSharpPractice
{
    internal static class XmlSerializerTest
    {
        public static void SalaryAccountToXml()
        {
            var salaryAccount = new ObjectTypes.SalaryAccount()
            {
                AccountHolderName = "John Doe",
                AccountNumber = "123456789",
                BankName = "Example Bank",
                Balance = 5000.00m,
                LastSalaryCreditedOn = DateOnly.Parse("2024-06-01")
            };

            // Serialize the SalaryAccount object to XML and save to file
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(ObjectTypes.SalaryAccount));
            using (var writer = new System.IO.StreamWriter("salary_account.xml"))
            {
                xmlSerializer.Serialize(writer, salaryAccount);
            }
        }

        public static void XmlToSalaryAccount()
        {
            // Deserialize the XML back to a SalaryAccount object
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(ObjectTypes.SalaryAccount));

            using var reader = new System.IO.StreamReader("salary_account.xml");
            var salaryAccount = xmlSerializer.Deserialize(reader) as ObjectTypes.SalaryAccount;

            Console.WriteLine($"Deserialized Salary Account: {salaryAccount?.AccountHolderName}, Balance: {salaryAccount?.Balance}");
        }
    }
}
