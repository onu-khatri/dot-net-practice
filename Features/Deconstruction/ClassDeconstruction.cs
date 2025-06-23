using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Features.Deconstruction
{
    internal class PersonalInformation() {
        public required string FirstName { get; set; }
        public string? City { get; set; }
        public required string State { get; set; }

        public void Deconstruct(out string firstName, out string city)
        {
            firstName = FirstName;
            city = City ?? "NaN";
        }

        public void Deconstruct(out string firstName, out string city, out string state)
        {
            firstName = FirstName;
            city = City ?? "NaN";
            state = State;
        }
    }


    internal class ClassDeconstruction
    {
        public static void PrintPersonalInfoText(PersonalInformation personalInformation)
        {
            var (firstName, city) = personalInformation;
            Console.WriteLine($"Available Info: {firstName} lives in {city}");

            personalInformation.State = "Delhi";

            (firstName, city, string state) = personalInformation;

            Console.WriteLine($"Updated info: {firstName} lives in {city} of {state}");  

        }

    }
}
