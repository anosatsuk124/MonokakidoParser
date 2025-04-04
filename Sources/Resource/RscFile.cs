namespace MonokakidoParser.Resource;


public class RscFile
{
    public uint FileIndex { get; set; }
    public string FilePath { get; set; }
    public Dictionary<uint, byte[]> ContentDatas { get; set; } = new Dictionary<uint, byte[]>();

    public RscFile(uint fileIndex, string filePath)
    {
        FileIndex = fileIndex;
        FilePath = filePath;
    }

    public void LoadContentData()
    {
        using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
        {
            while (stream.Position < stream.Length)
            {
                try
                {
                    var contentData = LoadContentData(stream);
                    ContentDatas.Add((uint)stream.Position, contentData);
                }
                catch (EndOfStreamException)
                {
                    // Handle the end of stream exception
                    break;
                }
                catch (InvalidDataException ex)
                {
                    // Handle invalid data exception
                    Console.Error.WriteLine($"Invalid data: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Handle any other exceptions
                    Console.Error.WriteLine($"An error occurred: {ex.Message}");
                    break;
                }
            }
        }
    }

    public static byte[] LoadContentData(Stream stream)
    {
        // Read the 4-byte length of the compressed data in little-endian format
        var lengthBytes = new byte[4];
        stream.ReadExactly(lengthBytes);
        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(lengthBytes);
        }
        var compressedDataLength = BitConverter.ToUInt32(lengthBytes, 0);

        // Read the compressed data
        var compressedData = new byte[compressedDataLength];
        stream.ReadExactly(compressedData);

        // Decompress the data
        var decompressedData = ZlibHelper.Decompress(compressedData);

        return decompressedData;
    }
}
