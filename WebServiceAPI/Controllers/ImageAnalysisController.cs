using Microsoft.AspNetCore.Mvc;
using System.IO;
using DND1.Data;
using DND1.Models;

[ApiController]
[Route("api/[controller]")]
public class ImageAnalysisController : ControllerBase
{
    private readonly AppDbContext _context;

    public ImageAnalysisController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file, [FromQuery] int userId)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var mriImage = new MRIImage
        {
            UserID = userId,
            FileName = file.FileName,
            FileContent = memoryStream.ToArray(),
            UploadedAt = DateTime.UtcNow,
        };

        _context.MRIImages.Add(mriImage);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "File uploaded successfully!", MRIImageID = mriImage.MRIImageID, FileName = mriImage.FileName });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFile(int id)
    {
        var mriImage = await _context.MRIImages.FindAsync(id);
        if (mriImage == null)
        {
            return NotFound("File not found.");
        }

        return File(mriImage.FileContent, "application/octet-stream", mriImage.FileName);
    }
}
