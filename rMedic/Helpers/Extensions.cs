using System.Text.RegularExpressions;

namespace rMedic.Helpers
{
    public static class Extensions
    {
        public static bool PhoneNumberIsValid(this string str)
        {
            return !string.IsNullOrEmpty(str) && !Regex.IsMatch(str, @"[^a-zA-z\d_]");
        }
        public static string FormatPhoneNumber(this string number)
        {
            return Regex.Replace(number, @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$", "($1$2$3) $4$5$6-$7$8$9$10");
        }
    }
}
