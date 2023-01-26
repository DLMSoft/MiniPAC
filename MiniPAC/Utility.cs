using System;

namespace DLMSoft.MiniPAC {
    static class Utility {
        public const string RANDOM_STRING_NUMBERS = "0123456789";
        public const string RANDOM_STRING_LOWER = "abcdefghijklmnopqrstuvwxyz";
        public const string RANDOM_STRING_UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string RANDOM_STRING_DEFAULT_PATTERN = RANDOM_STRING_NUMBERS + RANDOM_STRING_LOWER + RANDOM_STRING_UPPER;

        public static string GenerateRandomString(int length, string pattern = RANDOM_STRING_DEFAULT_PATTERN)
        {
            var random = new Random();
            var result = new char[length];

            for (var i = 0; i < length; i++) {
                result[i] = pattern[random.Next(0, pattern.Length)];
            }

            return new string(result);
        }
    }
}
