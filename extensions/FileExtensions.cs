using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Marti.Core.Extensions;

public static class FileExtensions
{
    public static async Task<string> ReadAsStringAsync(this IFormFile file)
    {
        var result = new StringBuilder();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                result.AppendLine(await reader.ReadLineAsync());
            }
        }
        return result.ToString();
    }

    public static string ReadAsString(this IFormFile file)
    {
        var result = new StringBuilder();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                result.AppendLine(reader.ReadLine());
            }
        }
        return result.ToString();
    }

}