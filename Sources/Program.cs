namespace MonokakidoParser;
using System.Text;

internal class Program
{
    protected static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to the RSC file.");
            return;
        }

        string rscFilePath = args[0];

        // Check if the file exists
        if (!File.Exists(rscFilePath))
        {
            Console.WriteLine($"The file '{rscFilePath}' does not exist.");
            return;
        }

        // Load and process the RSC file
        var rscFile = new Resource.RscFile(0, rscFilePath);
        rscFile.LoadContentData();

        // Sort the content data by key (offset)
        var contentDataArray = rscFile.ContentDatas
          .OrderBy(x => x.Key)
          .ToArray();

        foreach (var contentData in contentDataArray)
        {
            var fileContent = contentData.Value;

            // Convert the byte array to a string using UTF-8 encoding
            string fileContentString = Encoding.UTF8.GetString(fileContent);

            Console.WriteLine(fileContentString);

            Console.Error.WriteLine("----------------------------------------");
        }
    }
}
