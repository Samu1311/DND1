using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DND1.Data;
using DND1.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DND1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoleImageController : ControllerBase
    {
        private readonly ImageProcessor _imageProcessor;
        private readonly AppDbContext _context;
        private readonly string _sharedImagePath;
        private readonly string _resultImagePath;

        public MoleImageController(AppDbContext context, ImageProcessor imageProcessor)
        {
            _context = context;
            _imageProcessor = imageProcessor;
            _sharedImagePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "WebApp", "wwwroot", "MoleImages");
            _resultImagePath = Path.Combine(_sharedImagePath, "MoleImageResults");

            if (!Directory.Exists(_sharedImagePath))
            {
                Directory.CreateDirectory(_sharedImagePath);
            }
            if (!Directory.Exists(_resultImagePath))
            {
                Directory.CreateDirectory(_resultImagePath);
            }
        }

        // POST: api/moleimage/analyze
        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeMoleImage(IFormFile file, int userId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(_sharedImagePath, fileName);
                var url = $"/MoleImages/{fileName}";

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Process the image
                var (resultFilePath, averageColor, contourCount) = _imageProcessor.AnalyzeAndTraceMole(fileName, System.IO.File.ReadAllBytes(filePath));

                // Move the processed image to the shared path
                var processedFileName = Path.GetFileName(resultFilePath);
                var processedFilePath = Path.Combine(_resultImagePath, processedFileName);
                System.IO.File.Move(resultFilePath, processedFilePath);

                var processedUrl = $"/MoleImages/MoleImageResults/{processedFileName}";

                var moleImage = new MoleImage
                {
                    UserID = userId,
                    FileName = fileName,
                    FilePath = url,
                    Url = url,
                    AnalysisResults = $"Average Color: {averageColor}, Contour Count: {contourCount}",
                    UploadedAt = DateTime.UtcNow
                };

                _context.MoleImages.Add(moleImage);
                await _context.SaveChangesAsync();

                return Ok(new { moleImage.MoleImageID, moleImage.FilePath, moleImage.AnalysisResults, ProcessedImageUrl = processedUrl });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error analyzing mole image: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, $"An error occurred while uploading the mole image: {ex.Message}");
            }
        }

        // GET: api/moleimage/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMoleImagesByUser(int userId)
        {
            var moleImages = await _context.MoleImages.Where(m => m.UserID == userId).ToListAsync();

            if (!moleImages.Any())
                return NotFound("No mole images found for this user.");

            return Ok(moleImages);
        }

        // GET: api/moleimage/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMoleImage(int id)
        {
            var moleImage = await _context.MoleImages.FindAsync(id);
            if (moleImage == null)
            {
                return NotFound();
            }

            return Ok(moleImage);
        }

        // GET: api/moleimage
        [HttpGet]
        public async Task<IActionResult> GetAllMoleImages()
        {
            var moleImages = await _context.MoleImages.ToListAsync();
            return Ok(moleImages);
        }

        // PUT: api/moleimage/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMoleImage(int id, MoleImage updatedMoleImage)
        {
            if (id != updatedMoleImage.MoleImageID)
            {
                return BadRequest("MoleImage ID mismatch.");
            }

            var moleImage = await _context.MoleImages.FindAsync(id);
            if (moleImage == null)
            {
                return NotFound();
            }

            moleImage.FileName = updatedMoleImage.FileName;
            moleImage.FilePath = updatedMoleImage.FilePath;
            moleImage.Url = updatedMoleImage.Url;
            moleImage.ThumbnailUrl = updatedMoleImage.ThumbnailUrl;
            moleImage.AnalysisResults = updatedMoleImage.AnalysisResults;
            moleImage.UploadedAt = updatedMoleImage.UploadedAt;

            _context.MoleImages.Update(moleImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/moleimage/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoleImage(int id)
        {
            var moleImage = await _context.MoleImages.FindAsync(id);
            if (moleImage == null)
            {
                return NotFound();
            }

            _context.MoleImages.Remove(moleImage);
            await _context.SaveChangesAsync();

            if (System.IO.File.Exists(moleImage.FilePath))
            {
                System.IO.File.Delete(moleImage.FilePath);
            }

            return NoContent();
        }

        // DELETE: api/moleimage
        [HttpDelete]
        public async Task<IActionResult> DeleteAllMoleImages()
        {
            var moleImages = _context.MoleImages.ToList();

            if (!moleImages.Any())
                return NotFound("No mole images to delete.");

            _context.MoleImages.RemoveRange(moleImages);
            await _context.SaveChangesAsync();

            foreach (var moleImage in moleImages)
            {
                if (System.IO.File.Exists(moleImage.FilePath))
                {
                    System.IO.File.Delete(moleImage.FilePath);
                }
            }

            return Ok("All mole images have been deleted.");
        }
    }
}