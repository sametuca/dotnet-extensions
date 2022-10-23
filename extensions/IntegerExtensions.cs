namespace Marti.Core.Extensions;

public static class IntegerExtensions
{
    public static int ToInt(this object value)
    {
        var success = int.TryParse(value.ToString(), out var result);
        return success ? result : 0;
    }

    public static string MToKmReadable(this long value)
    {
        return value switch
        {
            < 0 => $"Negatif mesafe: {value} m",
            < 1000 => $"{value} m",
            _ => $"{value / 1000} km {(value % 1000 > 0 ? $"{value % 1000} m" : string.Empty)}"
        };
    }
}