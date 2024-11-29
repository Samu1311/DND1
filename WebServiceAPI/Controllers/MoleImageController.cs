using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DND1.Data;
using DND1.Models;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace DND1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoleImageController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _imageFolderPath;

        public MoleImageController(AppDbContext context)
        {
            _context = context;
            _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "mole-images");

            // Ensure the folder exists
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }
        }

        // POST: api/moleimage/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadMoleImage([FromForm] IFormFile file, [FromForm] int userId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File not provided or empty.");
            }

            // Validate User
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Generate unique filename and file path
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(_imageFolderPath, fileName);

            // Save the file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create MoleImage record
            var moleImage = new MoleImage
            {
                FileName = fileName,
                FilePath = filePath,
                UploadedAt = DateTime.UtcNow,
                UserID = userId
            };

            _context.MoleImages.Add(moleImage);
            await _context.SaveChangesAsync();

            return Ok(new { Status = "File uploaded successfully.", MoleImage = moleImage });
        }

        // GET: api/moleimage/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMoleImage(int id)
        {
            var moleImage = await _context.MoleImages.Include(m => m.User).FirstOrDefaultAsync(m => m.MoleImageID == id);

            if (moleImage == null)
            {
                return NotFound("Mole image not found.");
            }

            return Ok(moleImage);
        }

        // GET: api/moleimage/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserMoleImages(int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.UserID == userId);

            if (!userExists)
            {
                return NotFound("User not found.");
            }

            var moleImages = await _context.MoleImages
                .Where(m => m.UserID == userId)
                .ToListAsync();

            return Ok(moleImages);
        }

        // DELETE: api/moleimage/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoleImage(int id)
        {
            var moleImage = await _context.MoleImages.FindAsync(id);

            if (moleImage == null)
            {
                return NotFound("Mole image not found.");
            }

            // Delete file from disk
            if (System.IO.File.Exists(moleImage.FilePath))
            {
                System.IO.File.Delete(moleImage.FilePath);
            }

            _context.MoleImages.Remove(moleImage);
            await _context.SaveChangesAsync();

            return Ok(new { Status = "Mole image deleted successfully." });
        }

        // PUT: api/moleimage/analysis/{id}
        [HttpPut("analysis/{id}")]
        public async Task<IActionResult> UpdateAnalysisResults(int id, [FromBody] string analysisResults)
        {
            var moleImage = await _context.MoleImages.FindAsync(id);

            if (moleImage == null)
            {
                return NotFound("Mole image not found.");
            }

            moleImage.AnalysisResults = analysisResults;
            await _context.SaveChangesAsync();

            return Ok(new { Status = "Analysis results updated successfully.", MoleImage = moleImage });
        }
    }
}
