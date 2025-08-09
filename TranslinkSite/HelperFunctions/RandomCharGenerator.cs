using System;
using System.Collections.Generic;
using System.Text;

namespace TranslinkSite.HelperFunctions
{
    public class RandomCharGenerator
    {
        // Word Type must be either "name" or "random" 
        public static string RandomWordGenerator(int length, string wordType)
        {
            if (length <= 0)
                throw new ArgumentException("Length must be greater than zero.", nameof(length));

            var allowedTypes = new HashSet<string> { "name", "random" };
            wordType = wordType.ToLowerInvariant();

            if (!allowedTypes.Contains(wordType))
                throw new ArgumentException("Parameter must either be 'name' or 'random'.", nameof(wordType));

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789- ";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            if (wordType == "name")
            {
                // Append a random number for uniqueness
                return $"Tester{random.Next(1000, 9999)}";
            }

            // Default: "random"
            return new string(stringChars);
        }
    }
}
            

   
