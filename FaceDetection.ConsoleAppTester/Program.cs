using FacesConsoleAppTester;
using Newtonsoft.Json;

namespace FaceDetection.ConsoleAppTester;

public class Program
{
    static async Task Main(string[] args)
    {

        Console.Write("Enter filename: ");
        var filename = Console.ReadLine();

        var imagePath = Path.Combine("faces", filename!);
        var urlAddress = "https://localhost:6001/api/faces";

        var imageUtility = new ImageUtility();
        var bytes = imageUtility.ConvertToBytes(imagePath);
        List<byte[]>? faceList = null;
        var byteContent = new ByteArrayContent(bytes);
        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

        using var httpClient = new HttpClient();
        using var response = await httpClient.PostAsync(urlAddress, byteContent);
        var content = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(content))
        {
            faceList = JsonConvert.DeserializeObject<List<byte[]>>(content);
        }

        if (faceList?.Count > 0)
        {
            for (int i = 0; i < faceList.Count; i++)
            {
                imageUtility.FromBytesToImage(faceList[i], $"face{i}");
            }

            Console.WriteLine($"There were {faceList?.Count} faces");
        }

        Console.ReadKey();
    }
}