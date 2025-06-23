using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Features.Deconstruction
{
    record UserLocation(string City, string State, String Country);

    internal class TupleDeconstruction
    {
        public static void PrintLocationText(UserLocation location)
        {
            string country = "India";

            (string city, var state, country) = location; // When you declare a record type by using two or more positional parameters, the compiler creates a Deconstruct method with an out parameter for each positional parameter in the record declaration.

            var (citySuffixed, stateSuffixed, _) = SuffixLocation(city, state, country);


            Console.WriteLine($"User-Location: {citySuffixed}-{stateSuffixed}-{country}");
        }

        public static void PrintLocationText()
        {
            string country = "India";

            var location = ("New-Delhi", state: "Delhi", country); // Tuple assignment

            var (citySuffixed, stateSuffixed, _) = SuffixLocation(location.Item1, location.state, country);


            Console.WriteLine($"Default-Location: {citySuffixed}-{stateSuffixed}-{country}");
        }

        private static (string city, string state, string country) SuffixLocation(string city, string state, string country) { 
        
            return (city+"_city", state+"_state", country+"_country");
        }
    }
}
