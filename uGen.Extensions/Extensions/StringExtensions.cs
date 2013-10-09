using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace uGen.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if the string is null or empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Returns true if the string is NOT null or empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Returns the same value back if string is not null or empty, otherwise return the default value provided.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string IfNullOrEmptyThenDefault(this string str, string defaultValue)
        {
            return str.IsNotNullOrEmpty() ? str : defaultValue;
        }

        /// <summary>
        /// Returns N/A if the string is null or empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IfNullOrEmptyThenNa(this string str)
        {
            return str.IsNotNullOrEmpty() ? str : "N/A";
        }

        /// <summary>
        /// If the string provided exceeds the length provided then the string will be truncated.
        /// Optionally the truncated string can have an ellipsis appended to indicate that the string has
        /// been truncated.
        /// </summary>
        /// <param name="str">The string to truncate. If it's null or empty then the same string is returned.</param>
        /// <param name="maxLength">The maximum length allowed. May not be less than 1.</param>
        /// <returns></returns>
        public static string Truncate(this string str, int maxLength)
        {
            return str.Truncate(maxLength, true);
        }

        /// <summary>
        /// If the string provided exceeds the length provided then the string will be truncated.
        /// Optionally the truncated string can have an ellipsis appended to indicate that the string has
        /// been truncated.
        /// </summary>
        /// <param name="str">The string to truncate. If it's null or empty then the same string is returned.</param>
        /// <param name="maxLength">The maximum length allowed. May not be less than 1.</param>
        /// <param name="includeEllipsis">If true then if the string is truncated it will have an ellipsis appended</param>
        /// <returns></returns>
        public static string Truncate(this string str, int maxLength, bool includeEllipsis)
        {
            if (String.IsNullOrEmpty(str)) return str;
            if (maxLength < 1) throw new ArgumentException("maxLength may not be less than 1", "maxLength");

            var length = str.Length;
            var truncated = str.Substring(0, (length > maxLength ? maxLength : length));

            if (includeEllipsis && !(str.Length <= maxLength))
                truncated = String.Concat(truncated.Trim(), "...");

            return truncated;
        }

        /// <summary>
        /// Remove extra white spaces between the text.
        /// Example "This is  some    Text" will be "This is some Text".
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns></returns>
        public static string RemoveExtraSpaces(this string str)
        {
            var trimmer = new Regex(@"\s\s+");
            return trimmer.Replace(str.Replace("&nbsp;", " "), " ");
        }

        /// <summary>
        /// Used when we want to completely remove HTML code and not encode it with XML entities.
        /// </summary>
        /// <param name="input">Input string to remove HTML from</param>
        /// <returns></returns>
        public static string StripHtml(this string input)
        {
            // Will this simple expression replace all tags???
            var tagsExpression = new Regex(@"</?.+?>");//<.*?>
            return tagsExpression.Replace(input, "");
        }

        /// <summary>
        /// Get image src links from a string
        /// </summary>
        /// <param name="htmlSource">Input string</param>
        /// <returns></returns>
        public static string FetchFirstImgLinkFromHtmlSource(this string htmlSource)
        {
            //List<string> links = new List<string>();
            string regexImgSrc = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
            MatchCollection matchesImgSrc = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match m in matchesImgSrc)
            {
                string href = m.Groups[1].Value;
                return href;
                //links.Add(href);
            }
            return "";
        }
    }
}
