using OpenCvSharp;
using System.IO;

public class ImageProcessor
{
    private readonly string _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    private readonly string _processedDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "processed");

    public ImageProcessor()
    {
        Directory.CreateDirectory(_uploadDirectory);
        Directory.CreateDirectory(_processedDirectory);
    }

    public string AnalyzeAndTraceMole(string fileName, byte[] fileBytes)
    {
        // Save the uploaded file
        string uploadedFilePath = Path.Combine(_uploadDirectory, fileName);
        File.WriteAllBytes(uploadedFilePath, fileBytes);

        // Load the image
        Mat image = Cv2.ImRead(uploadedFilePath);
        Mat gray = new Mat();
        Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

        // Detect edges
        Mat edges = new Mat();
        Cv2.Canny(gray, edges, 50, 150);

        // Find and draw contours
        Point[][] contours;
        HierarchyIndex[] hierarchy;
        Cv2.FindContours(edges, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
        Cv2.DrawContours(image, contours, -1, new Scalar(0, 255, 0), 2); // Green borders

        // Save the processed image
        string processedFilePath = Path.Combine(_processedDirectory, fileName);
        Cv2.ImWrite(processedFilePath, image);

        return processedFilePath;
    }
}
