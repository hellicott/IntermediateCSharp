using System;

namespace BankSystem
{
    public static class MyExtensions
    {
        public static bool InRange(this double num, double min, double max)
        {
            return ((num > min) && (num < max));
        }

        public static string PasswordStrength(this string password)
        {
            bool length = password.Length >= 8;
            bool digit = false;
            bool nonAlphanum = false;

            foreach (char charac in password)
            {
                if (char.IsDigit(charac))
                {
                    digit = true;
                }
                else if (!char.IsLetterOrDigit(charac))
                {
                    nonAlphanum = true;
                }
            }
            if (length && digit && nonAlphanum)
            {
                return "strong";
            }
            else
            {
                return "weak;";
            }
        }
    }
}
