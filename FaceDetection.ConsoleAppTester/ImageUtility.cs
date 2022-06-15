using System.Drawing;
using System.Drawing.Imaging;

namespace FacesConsoleAppTester
{
    public class ImageUtility
    {
        public byte[] ConvertToBytes(string imagePath)
        {
            using var memoryStream = new MemoryStream();
            using var fileStream = new FileStream(imagePath, FileMode.Open);
            fileStream.CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();
            return bytes;

        }

        public void FromBytesToImage(byte[] imageBytes, string fileName)
        {
            using var memoryStream = new MemoryStream(imageBytes);
            var image = Image.FromStream(memoryStream);
            image.Save(Path.Combine("faces", $"{fileName}.jpg"), ImageFormat.Jpeg);
        }
    }
}
