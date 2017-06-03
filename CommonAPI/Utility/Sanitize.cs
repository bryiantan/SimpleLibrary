using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CommonApp.Utility
{
    public static class Sanitize
    {
        public static string SafeSqlLiteral(string input)
        {

            if ((!string.IsNullOrWhiteSpace(input)))
            {
                input = input.Replace("'", "''");
                input = input.Replace("/*", "");
                input = input.Replace("*/", "");
                input = input.Replace("VARCHAR", "");
                input = input.Replace("@@", "");
                input = input.Replace("%2D", "");
                input = input.Replace("%3B", "");
            }

            return input;
        }

        public static string CleanHTMLText(string content)
        {
            //remove script
            content = Regex.Replace(content, "<script.*?</script>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //remove events
            content = Regex.Replace(content, "on\\w+=\"[^\"]*\"", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //remove meta tag
            content = Regex.Replace(content, "<meta\\b(?:[^\"'>]|\"[^\"]*\"|'[^']*')*>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //remove style
            //content = Regex.Replace(content, @"sty\w+=""[^""]*""", string.Empty,
            //  RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //remove iframe
            content = Regex.Replace(content, "<iframe.*?/iframe>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //attribute
            content = Regex.Replace(content, "attr\\w+=\"[^\"]*\"", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //src
            content = Regex.Replace(content, "src\\w+=\"[^\"]*\"", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //href
            //content = Regex.Replace(content, "href\\w+=\"[^\"]*\"", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //remove unwated keywords
            content = Regex.Replace(content, "javascript\\s?\\:|expression\\s?\\(", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //uncaught
            content = Regex.Replace(content, "</?(?i:script|embed|object|img|xss|video|isindex|frameset|frame|iframe|meta|link|style|applet|head|title|html|body|noscript|noframe|button|form|layer|base|xml|\\?xml|t\\:set|param)(.|\\n)*?>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            return content;
        }

        public static string RemoveHTMLTag(string input)
        {
            string newInput = string.Empty;

            if ((!string.IsNullOrWhiteSpace(input)))
            {
                //strip all action for potential XSS
                newInput = CleanHTMLText(input);
            }

            return newInput;
        }
    }
}