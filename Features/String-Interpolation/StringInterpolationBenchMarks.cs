using BenchmarkDotNet.Attributes;
using System.Text;

namespace Features.String_Interpolation
{
    [MemoryDiagnoser]
    public class StringFormatBenchmark
    {
        private readonly UserInfo _userInfo = new("Anup", "Singh", "Khatri", "Mr", 34);

        [Benchmark]
        public string UsingStringConcat()
        {
            return string.Concat(_userInfo.Pronoun, ". ", _userInfo.FirstName, " ", _userInfo.Midname, " ", _userInfo.Lastname);
        }

        [Benchmark]
        public string UsingStringBuilder()
        {
            var sb = new StringBuilder();
            sb.Append(_userInfo.Pronoun).Append(". ")
              .Append(_userInfo.FirstName).Append(" ")
              .Append(_userInfo.Midname).Append(" ")
              .Append(_userInfo.Lastname);
            return sb.ToString();
        }

        [Benchmark]
        public string UsingStringFormat()
        {
            return string.Format("{0}. {1} {2} {3}", _userInfo.Pronoun, _userInfo.FirstName, _userInfo.Midname, _userInfo.Lastname);
        }

        [Benchmark]
        public string UsingStringInterpolation()
        {
            return $"{_userInfo.Pronoun}. {_userInfo.FirstName} {_userInfo.Midname} {_userInfo.Lastname}";
        }
    }
}
