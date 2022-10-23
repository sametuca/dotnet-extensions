using System;
using System.Collections.Generic;
using System.Text;

namespace Marti.Core.Extensions;

public static class PolygonExtensions
{
    public static IEnumerable<Point> Decode(this string polyline)
    {
        if (!string.IsNullOrEmpty(polyline))
        {
            var polylineItems = polyline.ToCharArray();

            int index = 0, latitude = 0, longitude = 0;

            while (index < polylineItems.Length)
            {
                var sum = 0;
                var shifter = 0;
                int bits;
                do
                {
                    bits = (int)polylineItems[index++] - 63;
                    sum |= (bits & 31) << shifter;
                    shifter += 5;
                } while (bits >= 32 && index < polylineItems.Length);

                if (index >= polylineItems.Length)
                    break;

                latitude += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                sum = 0;
                shifter = 0;
                do
                {
                    bits = polylineItems[index++] - 63;
                    sum |= (bits & 31) << shifter;
                    shifter += 5;
                } while (bits >= 32 && index < polylineItems.Length);

                if (index >= polylineItems.Length && bits >= 32)
                    break;

                longitude += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                yield return new Point
                {
                    Latitude = Convert.ToDouble(latitude) / 1E5,
                    Longitude = Convert.ToDouble(longitude) / 1E5
                };
            }
        }
    }
    public static string Encode(this IEnumerable<Point> points)
    {
        var latitude = 0;
        var longitude = 0;
        var sBuilder = new StringBuilder();
        foreach (var point in points)
        {
            var lat = (int)Math.Round(point.Latitude * 1e5, MidpointRounding.AwayFromZero);
            var lon = (int)Math.Round(point.Longitude * 1e5, MidpointRounding.AwayFromZero);
            sBuilder.Append(EncodeSignedNumber(lat - latitude));
            sBuilder.Append(EncodeSignedNumber(lon - longitude));
            latitude = lat;
            longitude = lon;
        }
        return sBuilder.ToString();
    }
    private static string EncodeSignedNumber(int num)
    {
        var sgn_num = num << 1;
        if (num < 0)
        {
            sgn_num = ~sgn_num;
        }
        return EncodeNumber(sgn_num);
    }
    private static string EncodeNumber(int num)
    {
        StringBuilder sBuilder = new();
        while (num >= 0x20)
        {
            sBuilder.Append((char)((0x20 | (num & 0x1f)) + 63));
            num >>= 5;
        }
        sBuilder.Append((char)(num + 63));
        return sBuilder.ToString().Replace(@"\", @"\\");
    }
}

public class Point : GeoLibrary.Model.Point
{
    public string Geohash { get; set; }

    public Point() : base() { }

    public Point(string geohash)
    {
        var point = NGeoHash.GeoHash.Decode(geohash).Coordinates;
        base.Latitude = point.Lat;
        base.Longitude = point.Lon;
    }

    public Point(double latitude, double longitude, int length = 7) : base(longitude, latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
        Geohash = NGeoHash.GeoHash.Encode(latitude, longitude, length);
    }
}