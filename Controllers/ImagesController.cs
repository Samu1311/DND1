using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using DND1.Models;  // Include the namespace for the Image model

namespace DND1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/images");

        public ImagesController()
        {
            // Ensure the folder exists
            if (!Directory.Exists(_imagePath))
            {
                Directory.CreateDirectory(_imagePath);
            }
        }

        // POST api/images/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile image, [FromForm] string description)
        {
            if (image == null || image.Length == 0)
                return BadRequest("Image not provided");

            // Generate file paths
            var imageFilePath = Path.Combine(_imagePath, image.FileName);
            var metadataFilePath = Path.ChangeExtension(imageFilePath, ".json");

            // Save image to the server
            using (var stream = new FileStream(imageFilePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Save metadata using the Image model as JSON file
            var imageMetadata = new Image
            {
                FileName = image.FileName,
                Description = description,
                UploadDate = DateTime.Now,
                FilePath = imageFilePath
            };

            // Serialize and save metadata
            await System.IO.File.WriteAllTextAsync(metadataFilePath, JsonSerializer.Serialize(imageMetadata));

            return Ok(new { Status = "Image and metadata uploaded successfully", ImageFileName = image.FileName });
        }

        // GET api/images/{filename}
        [HttpGet("{filename}")]
        public IActionResult GetImageMetadata(string filename)
        {
            var imageFilePath = Path.Combine(_imagePath, filename);
            var metadataFilePath = Path.ChangeExtension(imageFilePath, ".json");

            if (!System.IO.File.Exists(metadataFilePath))
                return NotFound("Metadata not found for this image.");

            var metadata = System.IO.File.ReadAllText(metadataFilePath);
            var imageMetadata = JsonSerializer.Deserialize<Image>(metadata);  // Deserialize into Image model
            return Ok(imageMetadata);
        }

        // GET api/images
        [HttpGet]
        public IActionResult GetAllImages()
        {
            var files = Directory.GetFiles(_imagePath, "*.json");
            var imageMetadataList = new List<Image>();

            foreach (var file in files)
            {
                var metadata = System.IO.File.ReadAllText(file);
                var imageMetadata = JsonSerializer.Deserialize<Image>(metadata);  // Deserialize into Image model
                imageMetadataList.Add(imageMetadata);
            }

            return Ok(imageMetadataList);
        }

        // DELETE api/images/{filename}
        [HttpDelete("{filename}")]
        public IActionResult DeleteImage(string filename)
        {
            var imageFilePath = Path.Combine(_imagePath, filename);
            var metadataFilePath = Path.ChangeExtension(imageFilePath, ".json");

            // Delete the image file
            if (System.IO.File.Exists(imageFilePath))
                System.IO.File.Delete(imageFilePath);

            // Delete the metadata file
            if (System.IO.File.Exists(metadataFilePath))
                System.IO.File.Delete(metadataFilePath);

            return Ok(new { Status = "Image and metadata deleted successfully" });
        }
    }
}