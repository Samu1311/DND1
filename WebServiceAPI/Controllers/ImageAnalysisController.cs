using Microsoft.AspNetCore.Mvc;
using System.IO;

[ApiController]
[Route("api/[controller]")]
public class ImageAnalysisController : ControllerBase
{
    private readonly ImageProcessor _imageProcessor;

    public ImageAnalysisController(ImageProcessor imageProcessor)
    {
        _imageProcessor = imageProcessor;
    }

    [HttpPost("analyze")]
    public IActionResult AnalyzeMoleImage([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        var fileName = Path.GetFileName(file.FileName);

        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            var processedFilePath = _imageProcessor.AnalyzeAndTraceMole(fileName, memoryStream.ToArray());
            var processedFileUrl = Url.Content($"~/processed/{fileName}");

            return Ok(new { ProcessedFilePath = processedFilePath, ProcessedFileUrl = processedFileUrl });
        }
    }
}
