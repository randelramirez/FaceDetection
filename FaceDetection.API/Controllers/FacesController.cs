using Microsoft.AspNetCore.Mvc;
using OpenCvSharp;

namespace FaceDetection.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FacesController : ControllerBase
{
    [HttpPost]
    public async Task<List<byte[]>> ReadFaces()
    {
        using var memoryStream = new MemoryStream(2048);
        await Request.Body.CopyToAsync(memoryStream);
        var faces = GetFaces(memoryStream.ToArray());
        return faces;
    }

    private List<byte[]> GetFaces(byte[] image)
    {
        Mat src = Cv2.ImDecode(image, ImreadModes.Color);

        // Convert the byte array into jpeg image and Save the image coming from the source
        // in the root directory for testing purposes
        src.SaveImage("image.jpg", new ImageEncodingParam(ImwriteFlags.JpegProgressive, 255));
        var file = Path.Combine(Directory.GetCurrentDirectory(), "CascadeFile", "haarcascade_frontalface_default.xml");
        var faceCascasde = new CascadeClassifier();
        faceCascasde.Load(file);

        var faces = faceCascasde.DetectMultiScale(src, 1.1, 6, HaarDetectionTypes.DoRoughSearch, new Size(60, 60));
        var faceList = new List<byte[]>();

        int i = 0;
        foreach (var rect in faces)
        {
            var faceImage = new Mat(src, rect);
            faceList.Add(faceImage.ToBytes(".jpg"));
            // TO DO: save images in wwwroot/faces
            faceImage.SaveImage($"face{i}.jpg", new ImageEncodingParam(ImwriteFlags.JpegProgressive, 255));
            i++;
        }
        return faceList;

    }
}


