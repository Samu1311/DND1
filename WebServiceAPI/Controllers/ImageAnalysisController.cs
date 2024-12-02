using Microsoft.AspNetCore.Mvc;
using System.IO;

[ApiController]
[Route("api/[controller]")]
public class FileUploadController : ControllerBase
{
    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        // Save the file in the wwwroot/UploadedFiles directory
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FileUpload");

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var filePath = Path.Combine(uploadPath, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return the relative file path for use in the app
        var relativePath = $"/FileUpload/{file.FileName}";
        return Ok(new { FileName = file.FileName, FilePath = relativePath });
    }
}