using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Automation.Common.Utils
{
    public static class StringUtils
    {

        public static string ReplaceLast(string replaceIn, string replace, string with)
        {
            int length = replaceIn.LastIndexOf(replace);
            if (length == -1)
                return replaceIn;
            return replaceIn.Substring(0, length) + with + replaceIn.Substring(length + replace.Length);
        }

        public static string ReplaceWithMatchEvaluator(this string source, string pattern, MatchEvaluator evaluator)
        {
            return Regex.Replace(source, pattern, evaluator);
        }


        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }

        public static string GetSharedFolderName(string folderName, string ownerName, string ownerLastName)
        {
            return String.Format("{0} ({1} {2})", folderName, ownerName, ownerLastName);
        }

        public static string ReplaceLast(object applicationName, string v, string empty)
        {
            throw new NotImplementedException();
        }

        public static string CombineUrl(params string[] urls)
        {
            var combinedUrl = urls[0];
            for (int i = 1; i < urls.Length; i++)
            {
                combinedUrl += '/' + urls[i];
            }
            return combinedUrl;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool ToBool(this string source)
        {
            bool bValue;
            if (!bool.TryParse(source, out bValue))
            {
                throw new InvalidCastException($"Unable to parse {source} to {bValue.GetType()}!");
            }

            return bValue;
        }

        public static string Reverse(this string value)
        {
            return new string(value.ToCharArray().Reverse().ToArray());
        }
    }
}
