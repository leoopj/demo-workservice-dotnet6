using System.Net.Mail;

namespace NewGO.Integration.Infra
{
    public static class ValidationExtensions
    {
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEmpty<T>(this ICollection<T> value)
        {
            if (value.IsNull())
                return true;

            return value.Count <= 0;
        }

        public static bool IsNotEmpty<T>(this ICollection<T> value)
        {
            if (value.IsNull())
                return false;

            return value.Count > 0;
        }

        public static bool IsNotNull(this object value)
        {
            return value != null;
        }

        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsCnpj(this long value)
        {
            return IsCnpj(value.ToString());
        }

        public static bool IsCnpj(this string value)
        {
            if (value.IsNullOrEmpty())
                return false;

            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum;
            int rest;
            string digit;
            string tempCnpj;

            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "").Replace("/", "");

            if (value.Length != 14)
                return false;

            tempCnpj = value.Substring(0, 12);
            sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

            rest = (sum % 11);

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();
            tempCnpj += digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

            rest = (sum % 11);

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit += rest.ToString();
            return value.EndsWith(digit);
        }

        public static bool IsCpf(this long value)
        {
            return IsCpf(value.ToString());
        }

        public static bool IsCpf(this string value)
        {
            if (value.IsNullOrEmpty())
                return false;

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digit;
            int sum;
            int rest;

            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "");

            if (value.Length != 11)
                return false;

            tempCpf = value.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();
            tempCpf += digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit += rest.ToString();
            return value.EndsWith(digit);
        }

        public static bool IsEmailValid(this string value)
        {
            if (value.IsNullOrEmpty())
                return false;

            //Valid
            try
            {
                var email = new MailAddress(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsTimeValid(this string input)
        {
            return TimeSpan.TryParse(input, out _);
        }

        public static bool IsDecimalValid(this string input)
        {
            return decimal.TryParse(input, out _);
        }
    }
}