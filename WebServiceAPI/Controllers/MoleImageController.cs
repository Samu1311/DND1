using Microsoft.AspNetCore.Mvc;
using RestSharp;
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
        public async Task<IActionResult> UploadMoleImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Hardcode userId as 1 for testing
            int userId = 1;

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

        // GET: api/moleimage/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMoleImage(int id)
        {
            var moleImage = await _context.MoleImages.FindAsync(id);
            if (moleImage == null)
                return NotFound("Image not found.");

            return File(moleImage.ImageData, "image/png", moleImage.FileName);
        }

        // GET: api/moleimage/metadata/{id}
        [HttpGet("metadata/{id}")]
        public async Task<IActionResult> GetMoleImageMetadata(int id)
        {
            var moleImage = await _context.MoleImages.FindAsync(id);
            if (moleImage == null)
                return NotFound("Metadata not found for this image.");

            return Ok(moleImage);
        }
    }
}