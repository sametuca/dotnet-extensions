using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Marti.Core.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    public static bool IsNotNull(this string str)
    {
        return !string.IsNullOrEmpty(str);
    }

    public static string ToStringData(this object str)
    {
        return str?.ToString() ?? string.Empty;
    }

    public static string Format(this object str)
    {
        if (string.IsNullOrEmpty(str?.ToString()))
        {
            return string.Empty;
        }

        return decimal.TryParse(str.ToString(), out var result) ? result.ToString("N0") : str.ToString();
    }

    public static string ToPhoneNumber(this string str)
    {
        return string.IsNullOrEmpty(str)
            ? str
            : Regex.Replace(str, @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$",
                "($1$2$3) $4$5$6 $7$8 $9$10");
    }

    public static string ClearHtmlTags(this string str)
    {
        return string.IsNullOrEmpty(str) ? str : Regex.Replace(str, "<.*?>", " ");
    }

    public static string ToPascalCase(this string s, TextInfo textInfo = null)
    {
        var result = new StringBuilder();
        var nonWordChars = new Regex(@"[^a-zA-Z0-9]+");
        var tokens = nonWordChars.Split(s);
        foreach (var token in tokens)
        {
            result.Append(token.ToPascalCaseSingleWord(textInfo));
        }

        return result.ToString();
    }

    public static string ToPascalCaseSingleWord(this string s, TextInfo textInfoParam = null)
    {
        var match = Regex.Match(s, @"^(?<word>\d+|^[a-z]+|[A-Z]+|[A-Z][a-z]+|\d[a-z]+)+$");
        var groups = match.Groups["word"];

        var textInfo = textInfoParam ?? CultureInfo.InvariantCulture.TextInfo;
        var result = new StringBuilder();
        foreach (var capture in groups.Captures.Cast<Capture>())
        {
            result.Append(textInfo.ToTitleCase(capture.Value.ToLower()));
        }

        return result.ToString();
    }

    public static string ClearPhoneNumber(this string str)
    {
        if (str == null)
        {
            return null;
        }

        if (str[..3] == "+90")
        {
            str = str.Substring(3, str.Length - 3);
        }

        if (str[..2] == "90")
        {
            str = str.Substring(2, str.Length - 2);
        }

        return string.IsNullOrEmpty(str)
            ? str
            : str.TrimStart('0').Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty);
    }

    private const string Dash = "-";

    public static string ToKeyword(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        str = str.Trim();
        str = str.ToLowerInvariant();
        str = str.Replace("ğ", "g");
        str = str.Replace("ü", "u");
        str = str.Replace("ş", "s");
        str = str.Replace("ı", "i");
        str = str.Replace("ö", "o");
        str = str.Replace("ç", "c");
        str = str.Replace("Ğ", "g");
        str = str.Replace("Ü", "u");
        str = str.Replace("Ş", "s");
        str = str.Replace("İ", "i");
        str = str.Replace("Ö", "o");
        str = str.Replace("Ç", "c");
        str = str.Replace("+", Dash);
        str = str.Replace("'", string.Empty);
        str = str.Replace("(", string.Empty);
        str = str.Replace(")", string.Empty);
        str = str.Replace(" ", Dash);
        str = str.Replace("/", Dash);
        str = str.Replace("&", Dash);
        str = str.Replace("!", string.Empty);
        str = str.Replace("?", string.Empty);
        str = str.Replace(".", string.Empty);
        str = str.Replace(":", string.Empty);
        str = str.Replace(@"\", Dash);
        str = str.Replace("---", Dash);
        str = str.Replace("--", Dash);
        str = str.Replace("\"", Dash);
        str = str.Replace("%", string.Empty);
        return str;
    }

    public static string ReplaceTurkishCharacters(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        str = str.Replace("ğ", "g");
        str = str.Replace("ü", "u");
        str = str.Replace("ş", "s");
        str = str.Replace("ı", "i");
        str = str.Replace("ö", "o");
        str = str.Replace("ç", "c");
        str = str.Replace("Ğ", "g");
        str = str.Replace("Ü", "u");
        str = str.Replace("Ş", "s");
        str = str.Replace("İ", "i");
        str = str.Replace("Ö", "o");
        str = str.Replace("Ç", "c");
        return str;
    }

    public static string RemoveNonAsciiCharacters(this string str)
    {
        return string.IsNullOrEmpty(str) ? str : Regex.Replace(str, @"[^\u0000-\u007F]+", string.Empty);
    }

    public static string RemoveWhiteSpaces(this string str)
    {
        return string.IsNullOrEmpty(str) ? str : str.Replace(" ", string.Empty);
    }

    public static string Base64Encode(this string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(this string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string AddCountryCode(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        //HACK: e.g. 95353804608 -> 905353804608, for different roles
        if (Regex.IsMatch(value, @"^9(?!0)\d{10}"))
        {
            value = $"+90{value.TrimStart('9')}";
        }

        return value.Length switch
        {
            10 => $"+90{value}",
            11 => $"+9{value}",
            12 => $"+{value}",
            13 => value,
            _ => value
        };
    }

    public static string[] LinesToStringArray(this string text)
    {
        return string.IsNullOrWhiteSpace(text) ? Array.Empty<string>() : text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    public static int[] LinesToIntArray(this string text)
    {
        return string.IsNullOrWhiteSpace(text) ? Array.Empty<int>() : LinesToStringArray(text).Select(x =>
        {
            var isOk = int.TryParse(x, out var result);
            return isOk ? result : default;
        }).Where(x => x > 0).ToArray();
    }
}