using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenCvSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using DND1.Models;

namespace DND1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoleTrackingController : ControllerBase
    {
        private readonly string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "images");

        public MoleTrackingController()
        {
            // Ensure the uploads directory exists
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeMole([FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image provided.");
            }

            // Generate file path for the uploaded image
            var filePath = Path.Combine(_uploadsFolder, image.FileName);

            try
            {
                // Save the uploaded image to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Create MoleImage object to store the data
                var moleImage = new MoleImage
                {
                    FileName = image.FileName,
                    FilePath = filePath,
                    UploadedAt = DateTime.UtcNow,
                    AnalysisResult = "Pending" // Set default analysis result
                };

                // Perform mole analysis (you can add your image processing here)
                var processedImagePath = PerformMoleAnalysis(filePath);

                // Update the analysis result in the MoleImage object
                moleImage.AnalysisResult = "Mole analysis completed.";

                // Optionally, you can save moleImage details to a database here if needed.

                // Return the processed image
                var fileBytes = await System.IO.File.ReadAllBytesAsync(processedImagePath);
                System.IO.File.Delete(processedImagePath); // Cleanup processed image
                return File(fileBytes, "image/png", "processed_image.png");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing image: {ex.Message}");
            }
        }

        private string PerformMoleAnalysis(string imagePath)
        {
            using var srcImage = Cv2.ImRead(imagePath, ImreadModes.Color);
            if (srcImage.Empty())
                throw new Exception("Image could not be loaded.");

            // Convert the image to grayscale
            using var grayImage = new Mat();
            Cv2.CvtColor(srcImage, grayImage, ColorConversionCodes.BGR2GRAY);

            // Apply Gaussian blur to reduce noise
            using var blurredImage = new Mat();
            Cv2.GaussianBlur(grayImage, blurredImage, new Size(5, 5), 0);

            // Threshold the image to create a binary image
            using var binaryImage = new Mat();
            Cv2.Threshold(blurredImage, binaryImage, 50, 255, ThresholdTypes.Binary);

            // Find contours
            Cv2.FindContours(binaryImage, out Point[][] contours, out _, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // Draw contours on the original image
            foreach (var contour in contours)
            {
                var area = Cv2.ContourArea(contour);
                if (area > 100) // Ignore small noise
                {
                    Cv2.DrawContours(srcImage, new[] { contour }, -1, new Scalar(0, 255, 0), 2); // Green border
                }
            }

            // Save processed image
            var processedImagePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            Cv2.ImWrite(processedImagePath, srcImage);

            return processedImagePath;
        }
    }
}
