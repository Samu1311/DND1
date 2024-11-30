using Microsoft.AspNetCore.Mvc;
using RestSharp;
using OpenCvSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using DND1.Data;
using DND1.Models;

namespace DND1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoleImageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoleImageController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/moleimage/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadMoleImage([FromForm] IFormFile file, [FromForm] int userId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                var moleImage = new MoleImage
                {
                    UserID = userId,
                    FileName = file.FileName,
                    ImageData = memoryStream.ToArray(),
                    UploadedAt = DateTime.UtcNow
                };

                _context.MoleImages.Add(moleImage);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Image uploaded successfully", MoleImage = moleImage });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image: {ex.Message}");
                return StatusCode(500, "An error occurred while uploading the image.");
            }
        }

        // POST: api/moleimage/analyze/{id}
        [HttpPost("analyze/{id}")]
        public async Task<IActionResult> AnalyzeMoleImage(int id)
        {
            var moleImage = await _context.MoleImages.FindAsync(id);

            if (moleImage == null)
                return NotFound("Image not found.");

            try
            {
                // Send the image to an external API using RestSharp
                var externalAnalysisResult = await AnalyzeWithExternalAPI(moleImage);

                // Save the results in the database
                moleImage.AnalysisResults = externalAnalysisResult;
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Image analyzed successfully", AnalysisResults = externalAnalysisResult });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error analyzing image: {ex.Message}");
                return StatusCode(500, "An error occurred while analyzing the image.");
            }
        }

        private async Task<string> AnalyzeWithExternalAPI(MoleImage moleImage)
            {
                // Initialize the RestClient with the endpoint URL
                var client = new RestClient("https://your-image-analysis-api-endpoint");

                // Initialize the RestRequest and specify the HTTP method
                var request = new RestRequest("endpoint-path", Method.Post); // Replace "endpoint-path" with the actual API path

                // Add the image data as a file to the request
                request.AddFile("file", moleImage.ImageData, moleImage.FileName, "image/png");

                // Execute the request asynchronously
                var response = await client.ExecuteAsync(request);

                if (!response.IsSuccessful)
                {
                    throw new Exception($"External API error: {response.ErrorMessage}");
                }

                // Return the response content as the analysis result
                return response.Content ?? "Analysis results not available.";
            }



        private string AnalyzeImage(byte[] imageData)
        {
            // Local analysis using OpenCV (as a fallback or additional processing)
            using var src = Mat.FromImageData(imageData, ImreadModes.Color);
            using var gray = new Mat();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

            // Apply Gaussian Blur to reduce noise
            Cv2.GaussianBlur(gray, gray, new Size(5, 5), 0);

            // Apply thresholding to detect edges
            Cv2.Threshold(gray, gray, 60, 255, ThresholdTypes.Binary);

            // Find contours
            Cv2.FindContours(
                gray,
                out Point[][] contours,
                out _,
                RetrievalModes.External,
                ContourApproximationModes.ApproxSimple);

            if (contours.Length == 0)
                return "No contours found.";

            // Get the largest contour (assume it's the mole)
            var largestContour = contours[0];
            foreach (var contour in contours)
            {
                if (Cv2.ContourArea(contour) > Cv2.ContourArea(largestContour))
                {
                    largestContour = contour;
                }
            }

            // Draw the contour on the original image
            using var output = src.Clone();
            Cv2.DrawContours(output, new[] { largestContour }, -1, Scalar.Red, 2);

            // Create a mask for the largest contour
            using var mask = new Mat(src.Size(), MatType.CV_8UC1, Scalar.Black);
            Cv2.DrawContours(mask, new[] { largestContour }, -1, Scalar.White, -1);

            // Calculate the average color within the contour
            Scalar meanColor = Cv2.Mean(src, mask);

            // Save the analyzed image to a file (optional)
            string analyzedImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "analyzed-images", $"{Guid.NewGuid()}.jpg");
            Directory.CreateDirectory(Path.GetDirectoryName(analyzedImagePath)!);
            output.SaveImage(analyzedImagePath);

            return $"Average Color: R={meanColor.Val2}, G={meanColor.Val1}, B={meanColor.Val0}. Analyzed image saved at: {analyzedImagePath}.";
        }
    }
}
