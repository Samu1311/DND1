using OpenCvSharp;
using System;
using System.IO;

public class ImageProcessor
{
    private readonly string _uploadDirectory;
    public string ResultDirectory { get; }

    public ImageProcessor(string uploadDirectory, string resultDirectory)
    {
        _uploadDirectory = uploadDirectory;
        ResultDirectory = resultDirectory;
    }

    public (string resultFilePath, Scalar averageColor, int contourCount) AnalyzeAndTraceMole(string fileName, byte[] fileBytes)
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

        // Calculate the average color within the contour
        Scalar averageColor = CalculateAverageColor(image, contours);

        // Save the processed image
        string resultFileName = Path.Combine(ResultDirectory, "processed_" + fileName);
        Cv2.ImWrite(resultFileName, image);

        // Return the result file path, average color, and contour count
        return (resultFileName, averageColor, contours.Length);
    }

    private Scalar CalculateAverageColor(Mat image, Point[][] contours)
    {
        Mat mask = Mat.Zeros(image.Size(), MatType.CV_8UC1);
        Cv2.DrawContours(mask, contours, -1, Scalar.White, -1); // Fill the contour

        Scalar meanColor = Cv2.Mean(image, mask);
        return meanColor;
    }
}