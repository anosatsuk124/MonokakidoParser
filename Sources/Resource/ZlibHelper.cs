namespace MonokakidoParser.Resource;

using System.IO;
using System.IO.Compression;

public static class ZlibHelper
{
    public static byte[] Decompress(byte[] inBuf)
    {
        using (var inStream = new MemoryStream(inBuf))
        using (var decompressor = new ZLibStream(inStream, CompressionMode.Decompress))
        using (var outStream = new MemoryStream())
        {
            decompressor.CopyTo(outStream);
            if (inStream.Position != inBuf.Length)
            {
                throw new InvalidDataException("Incorrect stream length.");
            }
            return outStream.ToArray();
        }
    }
}
