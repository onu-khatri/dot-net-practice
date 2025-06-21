namespace Features.String_Interpolation
{
    internal record UserInfo(string FirstName, string Midname, string Lastname, string Pronoun, int? Age);

    internal static class StringInterpolation
    {
        public static void PrintUserInfo(UserInfo userInfo)
        {
            // Composite formating
            Console.WriteLine("{0}. {1} {2} {3}", userInfo.Pronoun, userInfo.FirstName, userInfo.Midname, userInfo.Lastname);

            // String Interpolation
            Console.WriteLine($"{userInfo.Pronoun}. {userInfo.FirstName} {userInfo.Midname} {userInfo.Lastname}");
        }

        public static void TableView(UserInfo userInfo)
        {
            const short rightAlign = 12, leftAlign = -12;

            Console.WriteLine($"{"First-Name",leftAlign} | {"Mid-Name",rightAlign} | {"Last-Name",rightAlign}");
            Console.WriteLine($"{userInfo.FirstName,leftAlign} | {userInfo.Midname,rightAlign} | {userInfo.Lastname,rightAlign}");
        }

        public static void WithPatternMatch(UserInfo userInfo)
        {
            Console.WriteLine($"{userInfo.Pronoun}. {userInfo.FirstName} {userInfo.Midname} {userInfo.Lastname} is {(userInfo.Age ?? 20) switch
            {
                > 60 => "a young person near to retire.",
                > 35 => "a super active person.",
                > 25 => "an exited energy.",
                > 15 => "preparing for solid future.",
                _ => "founding a solid base by education."
            }}");
        }

        public static void WithRawStringLiterals(UserInfo userInfo)
        {
            Console.WriteLine($"""The user "{userInfo.FirstName} {userInfo.Midname}" is {userInfo.Age} years young.""");
            Console.WriteLine($$"""The user {{{userInfo.FirstName}} {{userInfo.Midname}}} is {{userInfo.Age}} years young.""");
        }

    }
}
