using System.Text.RegularExpressions;

namespace rMedic.Helpers
{
    public static class Extensions
    {
        #region String Extensions
        public static bool PhoneNumberIsValid(this string str)
        {
            return !string.IsNullOrWhiteSpace(str) && !Regex.IsMatch(str, @"^[0-9]*$");
        }
        public static string FormatPhoneNumber(this string number)
        {
            return Regex.Replace(number, @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$", "($1$2$3) $4$5$6-$7$8$9$10");
        }
        #endregion
    }
}
