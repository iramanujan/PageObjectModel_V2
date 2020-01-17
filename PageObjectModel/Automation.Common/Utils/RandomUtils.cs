using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Common.Utils
{
    public static class RandomUtils
    {
        private static Random rnd = new Random();
        private const string DefaultDomain = "dispostable.com";
        private const string DefaultEmailDomain = "gmail.com";

        /// <summary>
        /// Get the random value from enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRandomValueFromEnum<T>() where T : struct, IConvertible
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(rnd.Next(values.Length));
        }

        /// <summary>
        /// Generates random decimal.
        /// </summary>
        /// <param name="min">
        /// The min value.
        /// </param>
        /// <param name="max">
        /// The max value.
        /// </param>
        /// <returns>
        /// Returns randomly generated decimal value.
        /// </returns>
        public static decimal RandomDecimal(int min, int max)
        {
            var integerPart = (decimal)rnd.Next(min, max);
            var fractionPart = (decimal)rnd.Next(0, 99) / 100;
            return integerPart + fractionPart;
        }

        /// <summary>
        /// The random double.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public static double RandomDouble(int min, int max)
        {
            var integerPart = (double)rnd.Next(min, max);
            var fractionPart = (double)rnd.Next(0, 99) / 100;
            return integerPart + fractionPart;
        }

        /// <summary>
        /// The random int.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int RandomNumeric(int min, int max)
        {
            return rnd.Next(min, max);
        }

        /// <summary>
        /// Randomizes the numeric string.
        /// </summary>
        /// <param name="size">The size.</param>
        public static string RandomizeNumericString(int size)
        {
            const string chars = "0123456789";
            string randomNumericString = RandomizeString(size, chars);

            while (randomNumericString.StartsWith("0", StringComparison.CurrentCultureIgnoreCase))
            {
                randomNumericString = randomNumericString.Replace("0", RandomizeString(1, chars));
            }

            return randomNumericString;
        }

        /// <summary>
        /// Randomizes the phone number in north american format.
        /// </summary>
        public static string RandomPhoneNumber()
        {
            return string.Format("{0}-{1}-{2:D4}", rnd.Next(200, 1000), rnd.Next(200, 1000), rnd.Next(0, 10000));
        }

        /// <summary>
        /// Randomizes the alphabetical string.
        /// </summary>
        /// <param name="size">The size.</param>
        public static string RandomizeAlphabeticalString(int size)
        {
            const string chars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return RandomizeString(size, chars);
        }

        /// <summary>
        /// Randomizes the alphanumeric string.
        /// </summary>
        /// <param name="size">The size.</param>
        public static string RandomizeAlphanumericString(int size)
        {
            const string chars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return RandomizeString(size, chars);
        }

        /// <summary>
        /// Randomizes the special symbols string.
        /// </summary>
        /// <param name="size">The size.</param>
        public static string RandomizeSpecialSymbolsString(int size)
        {
            const string chars = "~`@#$%^&*()_+!№;%:?*";
            return RandomizeString(size, chars);
        }

        /// <summary>
        /// Randomizes the not english string.
        /// </summary>
        /// <param name="size">The size.</param>
        public static string RandomizeNotEnglishString(int size)
        {
            const string chars =
                "¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýÿАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            return RandomizeString(size, chars);
        }

        /// <summary>
        /// Randomizes the spaces string.
        /// </summary>
        /// <param name="size">The size.</param>
        public static string RandomizeSpacesString(int size)
        {
            const string chars = " ";
            return RandomizeString(size, chars);
        }

        /// <summary>
        /// Generate random alphabetical string with length from 4 to 12.
        /// </summary>
        /// <returns>Random string</returns>
        public static string RandomizeAlphabeticalString()
        {
            return RandomizeAlphabeticalString(RandomNumeric(4, 12));
        }

        /// <summary>
        /// Generate random domain name (in .com domain) for specified number of levels (1 - syncp.com, 2 - subdomain1.syncp.com, 3 - subdomain2.subdomain1.syncp.com, etc.)
        /// </summary>
        /// <param name="numberOfLevels">Number of levels (1 - syncp.com, 2 - subdomain1.syncp.com, 3 - subdomain2.subdomain1.syncp.com, etc.)</param>
        /// <returns>Random domain name</returns>
        public static string RandomDomain(int numberOfLevels)
        {
            var randomDomainName = new StringBuilder();

            for (int i = 0; i < numberOfLevels - 1; i++)
            {
                randomDomainName.AppendFormat("{0}.", RandomizeAlphabeticalString());
            }

            randomDomainName.Append(DefaultEmailDomain);

            return randomDomainName.ToString();
        }

        /// <summary>
        /// Generate random email address with specified prefix for default domain, for example: prefix-zosnpxw@dispostable.com.
        /// </summary>
        /// <param name="prefix">Prefix for email address</param>
        /// <returns>Returns random email address</returns>
        public static string RandomDefaultDomainEmail(string prefix)
        {
            var email = $"{prefix}-{RandomizeAlphabeticalString(7)}@{DefaultDomain}";

            return email;
        }

        /// <summary>
        /// Generate random email address with random domain.
        /// </summary>
        /// <returns>Random email address</returns>
        public static string RandomEmail()
        {
            var randomEmail = $"{RandomizeAlphabeticalString()}@{RandomDomain(2)}";

            return randomEmail;
        }

        /// <summary>
        /// Generates multiple random emails based on count parameter.
        /// </summary>
        /// <param name="count">The emails count.</param>
        /// <param name="prefix">Prefix for email address.</param>
        /// <returns>IEnumerable with random emails</returns>
        public static IEnumerable<string> MultipleRandomEmails(int count, string prefix = "test")
        {
            var randomEmails = new List<string>();
            for (var i = 0; i < count; i++)
            {
                randomEmails.Add(RandomDefaultDomainEmail(prefix));
            }

            return randomEmails;
        }

        /// <summary>
        /// Generates the random boolean
        /// </summary>
        /// <returns></returns>
        public static bool RandomBool()
        {
            return rnd.NextDouble() > 0.5;
        }

        /// <summary>
        /// General method to randomize strings
        /// </summary>
        /// <param name="size">size of new string</param>
        /// <param name="charsForRandom">Chars that will partcipate in random</param>
        /// <returns>result random string</returns>
        private static string RandomizeString(int size, string charsForRandom)
        {
            var buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = charsForRandom[rnd.Next(charsForRandom.Length)];
            }
            return new string(buffer);
        }

        /// <summary>
        /// Generate Lorem Ipsum sentence
        /// </summary>
        /// <param name="wordCount">Words length</param>
        /// <returns>Sentense with defined length of words</returns>
        public static string RadomSentence(int? wordCount = null)
        {
            var lorem = new Bogus.DataSets.Lorem();

            return lorem.Sentence(wordCount);
        }

        public static String GetTimestamp()
        {
            return DateTime.Now.ToString("_yyyyMMddHHmmssffff");
        }
    }

}
