using NewGO.Integration.Infra;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace Keeggo.Integration.Infra
{
    public static class ConvertExtensions
    {
        public static string ToJson(this object value)
        {
            var configJson = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            configJson.Converters.Add(new IsoDateTimeConverter());
            JsonConvert.DefaultSettings = () => configJson;

            return JsonConvert.SerializeObject(value);
        }

        public static T ToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static DateTime ToDateTimeBR(this object value) => ToDateTimeBR(value.ToString());

        public static DateTime ToDateTimeBR(this string value)
        {
            if (value.IsNullOrEmpty())
                return DateTime.MinValue;

            var date = Convert.ToDateTime(value.ToString(), CultureInfo.CreateSpecificCulture("pt-BR"));

            return date;
        }

        public static DateTime ToDateTime(this string value, string? formatDate = null)
        {
            if (value.IsNullOrEmpty())
                return DateTime.MinValue;

            if (formatDate.IsNullOrEmpty())
                return Convert.ToDateTime(value.ToString(), CultureInfo.InvariantCulture);

            return DateTime.ParseExact(value, formatDate, CultureInfo.InvariantCulture);
        }

        public static DateTime ToDateTime(this long unixtime)
        {
            DateTime dtDateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixtime).ToLocalTime().ToDateTimeBR();
            return dtDateTime;
        }

        public static long ToLong(this DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
        }
    }
}
