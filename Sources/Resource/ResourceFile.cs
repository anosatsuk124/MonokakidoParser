namespace MonokakidoParser.Resource;

using System;
using System.IO;
using System.IO.Compression;

public static class ZlibHelper
{
    /// <summary>
    /// Decompresses zlib-compressed data and returns the decompressed byte array.
    /// </summary>
    /// <param name="inBuf">Input compressed byte array</param>
    /// <returns>Decompressed byte array</returns>
    public static byte[] Decompress(byte[] inBuf)
    {
        using (var inStream = new MemoryStream(inBuf))
        using (var decompressor = new ZLibStream(inStream, CompressionMode.Decompress))
        using (var outStream = new MemoryStream())
        {
            decompressor.CopyTo(outStream);
            // 入力全体が処理されたかチェック
            if (inStream.Position != inBuf.Length)
            {
                throw new InvalidDataException("Incorrect stream length.");
            }
            return outStream.ToArray();
        }
    }
}
