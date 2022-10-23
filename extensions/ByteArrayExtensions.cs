using System;

namespace Marti.Core.Extensions;

public static class ByteArrayExtensions
{
    public static ReadOnlySpan<byte> GetSpan(this byte[] bytes)
    {
        return bytes == null ? ReadOnlySpan<byte>.Empty : new ReadOnlySpan<byte>(bytes);
    }
}