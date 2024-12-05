using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DND1.Data;
using DND1.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DND1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilePictureController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _sharedImagePath;

        public ProfilePictureController(AppDbContext context)
        {
            _context = context;
            _sharedImagePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "WebApp", "wwwroot", "ProfilePictures");
            if (!Directory.Exists(_sharedImagePath))
            {
                Directory.CreateDirectory(_sharedImagePath);
            }
        }

        // POST: api/profilepicture/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file, int userId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(_sharedImagePath, fileName);
                var url = $"/ProfilePictures/{fileName}";

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Generate a thumbnail
                var thumbnailFileName = $"{Guid.NewGuid()}_thumb_{file.FileName}";
                var thumbnailFilePath = Path.Combine(_sharedImagePath, thumbnailFileName);
                var thumbnailUrl = $"/ProfilePictures/{thumbnailFileName}";

                using (var image = SixLabors.ImageSharp.Image.Load(filePath))
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(150, 150),
                        Mode = ResizeMode.Crop
                    }));
                    image.Save(thumbnailFilePath);
                }

                var profilePicture = new ProfilePicture
                {
                    UserID = userId,
                    FileName = fileName,
                    FilePath = filePath,
                    Url = url,
                    ThumbnailUrl = thumbnailUrl,
                    UploadedAt = DateTime.UtcNow
                };

                _context.ProfilePictures.Add(profilePicture);
                await _context.SaveChangesAsync();

                return Ok(new { profilePicture.ProfilePictureID, profilePicture.Url, profilePicture.ThumbnailUrl });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading profile picture: {ex.Message}");
                return StatusCode(500, "An error occurred while uploading the profile picture.");
            }
        }

        // GET: api/profilepicture/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfilePicture(int id)
        {
            var profilePicture = await _context.ProfilePictures.FindAsync(id);
            if (profilePicture == null)
                return NotFound("Profile picture not found.");

            return Ok(profilePicture);
        }

        // GET: api/profilepicture/latest/{userId}
        [HttpGet("latest/{userId}")]
        public async Task<IActionResult> GetLatestProfilePicture(int userId)
        {
            var profilePicture = await _context.ProfilePictures
                .Where(p => p.UserID == userId)
                .OrderByDescending(p => p.UploadedAt)
                .FirstOrDefaultAsync();

            if (profilePicture == null)
                return NotFound("Profile picture not found.");

            return Ok(profilePicture);
        }

        // DELETE: api/profilepicture/deleteall
        [HttpDelete("deleteall")]
        public async Task<IActionResult> DeleteAllProfilePictures()
        {
            try
            {
                var profilePictures = await _context.ProfilePictures.ToListAsync();
                _context.ProfilePictures.RemoveRange(profilePictures);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "All profile pictures have been deleted." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting profile pictures: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the profile pictures.");
            }
        }
    }
}