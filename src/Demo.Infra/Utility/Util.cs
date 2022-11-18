using NewGO.Integration.Infra;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text;

namespace Keeggo.Integration.Infra
{
    public static class Util
    {
        public static string OnlyNumbers(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;

            return new string((from v in valor.ToCharArray() where char.IsDigit(v) select v).ToArray());
        }

        public static string RemoveAccents(this string text, bool invertC = false)
        {
            StringBuilder sbReturn = new();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);

            if (invertC)
                return sbReturn.ToString().Replace('Ç', 'C');

            return sbReturn.ToString();
        }


        /// <summary>
        ///   Checks if name matches pattern with '?' and '*' wildcards.
        /// </summary>
        /// <param name="name">
        ///   Name to match.
        /// </param>
        /// <param name="pattern">
        ///   Pattern to match to.
        /// </param>
        /// <returns>
        ///   <c>true</c> if name matches pattern, otherwise <c>false</c>.
        /// </returns>
        public static bool NameMatchesPattern(this string name, string pattern)
        {
            if (pattern.IsNullOrEmpty())
                return true;

            pattern = pattern.ToLower();
            name = name.ToLower();

            var filter = pattern.ToLower();

            if (!pattern.Contains('*'))
                return name.Equals(filter);

            if (pattern.StartsWith('*') && pattern.EndsWith('*'))
                return name.Contains(filter.Replace("*", ""));

            if (pattern.StartsWith('*') && !pattern.EndsWith('*'))
                return name.EndsWith(filter.Replace("*", ""));

            if (!pattern.StartsWith('*') && pattern.EndsWith('*'))
                return name.StartsWith(filter.Replace("*", ""));

            var filters = pattern.Split('*');
            return name.StartsWith(filters[0]) && name.EndsWith(filters[1]);
        }

        public static string ToRelativePath(this string path)
        {
            return path.Replace('\\', '/').Replace("//", "/");
        }

        public static string ToAbsolutePath(this string path)
        {
            return path.Replace('/', '\\').Replace("\\\\", "\\");
        }

        public static string ToCpf(this string value)
        {
            if (value.IsNullOrEmpty())
                return value;

            value = value.OnlyNumbers();

            value = value.PadLeft(11, '0');
            return Format(value, "###.###.###-##");
        }

        public static string Format(string value, string mask)
        {
            StringBuilder data = new();
            foreach (char c in value)
            {
                if (char.IsNumber(c))
                    data.Append(c);
            }

            int indexMask = mask.Length;
            int indexField = data.Length;

            for (; indexField > 0 && indexMask > 0;)
                if (mask[--indexMask] == '#')
                    indexField--;

            StringBuilder saida = new();
            for (; indexMask < mask.Length; indexMask++)
                saida.Append((mask[indexMask] == '#') ? data[indexField++] : mask[indexMask]);

            return saida.ToString();
        }

        public static TimeSpan GetTimeHHmmDifference(this string startTime, string endTime)
        {
            if (startTime.IsNullOrEmpty() || endTime.IsNullOrEmpty())
                return TimeSpan.MinValue;

            return TimeSpan.Parse(endTime).Subtract(TimeSpan.Parse(startTime));
        }

        public static string? GetIP()
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in ipEntry.AddressList)
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip.ToString();

            return null;
        }

        public static string? GetPropertyByName<T>(this string name)
        {
            Type t = typeof(T);
            return t.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.Name;
        }

        public static int GetYearDiferenceDates(DateTime startDate, DateTime endDate, string typeOperation)
        {
            try
            {
                var newDateStart = $"{startDate.Year}-{startDate.Month}-{startDate.Day}";
                var newDateEnd = $"{endDate.Year}-{endDate.Month}-{endDate.Day}";

                TimeSpan date = newDateEnd.ToDateTimeBR() - newDateStart.ToDateTimeBR();
                int value = 0;

                switch (typeOperation)
                {
                    case "year":
                        value = (date.Days / 365);
                        break;
                    case "day":
                        value = date.Days;
                        break;
                }

                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
