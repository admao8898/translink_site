using System;
using System.Collections.Generic;
using System.Text;

namespace TranslinkSite.HelperFunctions
{
    public class RandomCharGenerator
    {
        // Word Type must be either "name" or "random" 
        public static string RandomWordGenerator(int n, string wordtype)
        {
            var numof_chars = n;
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ-abcdefghijklmnopqrstuvwxyz 0123456789";
            var stringChars = new char[n];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                n = random.Next(chars.Length);
                stringChars[i] = chars[n];
            }

            if (wordtype == "name")
            {
                string displayName = String.Format("Tester{0}", n);
                return displayName;
            }

            if (wordtype == "random")
            {
                string message = new String(stringChars);
                return message;
            }

            else
            {
                throw new System.ArgumentException("Parameter must either be 'name' or 'random'", "Word Type");

            }

        }
    }
}
            

   
