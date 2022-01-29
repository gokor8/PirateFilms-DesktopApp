using System.Text.RegularExpressions;
using AngleSharp;

namespace Films.Models.Web
{
    public class ParserCore
    {
        public IBrowsingContext Context;

        public ParserCore()
        {
            Context = BrowsingContext.New(Configuration.Default);
        }

        public string ClearWhiteSpaces(string value)
        {
            value = Regex.Replace(value, @"\s+", " ");
            if (value.Length > 0)
            {
                if (value[0] == ' ')
                    value = value.Substring(1);

                value = value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
            }
            return value.Trim();
        }
    }
}