using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DND1.Data;
using DND1.Models;

[ApiController]
[Route("api/[controller]")]
public class MedicalDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public MedicalDataController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/MedicalData/save
    [HttpPost("save")]
    public async Task<IActionResult> SaveMedicalData([FromBody] MedicalData medicalDataModel, [FromQuery] int userId)
    {
        if (medicalDataModel == null)
        {
            return BadRequest("MedicalData is null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Set the UserId
        medicalDataModel.UserId = userId;

        // Save it to the database
        _context.MedicalData.Add(medicalDataModel);
        await _context.SaveChangesAsync();

        // Return success response
        return Ok(new { MedicalDataID = medicalDataModel.MedicalDataID });
    }

    // GET: api/MedicalData/get/{id}
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetMedicalData(int id)
    {
        var medicalData = await _context.MedicalData.FirstOrDefaultAsync(md => md.MedicalDataID == id);

        if (medicalData == null)
        {
            return NotFound(new { Message = "Medical data not found" });
        }

        return Ok(medicalData);
    }

    // GET: MEDICAL DATA BY USERID
    // GET: api/MedicalData/getbyuser/{userId}
  [HttpGet("getbyuser/{userId}")]
    public async Task<IActionResult> GetMedicalDataByUser(int userId)
    {
        var medicalData = await _context.MedicalData.FirstOrDefaultAsync(md => md.UserId == userId);

        if (medicalData == null)
        {
            return NotFound(new { Message = "Medical data not found" });
        }

        return Ok(medicalData);
    }


    // PUT: api/MedicalData/update/{id}
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateMedicalData(int id, [FromBody] MedicalData medicalDataModel)
    {
        if (medicalDataModel == null || id != medicalDataModel.MedicalDataID)
        {
            return BadRequest("Invalid medical data.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingMedicalData = await _context.MedicalData.FindAsync(id);
        if (existingMedicalData == null)
        {
            return NotFound(new { Message = "Medical data not found" });
        }

        // Update the existing medical data
        existingMedicalData.Weight = medicalDataModel.Weight;
        existingMedicalData.Height = medicalDataModel.Height;
        existingMedicalData.BloodType = medicalDataModel.BloodType;
        existingMedicalData.Smoking = medicalDataModel.Smoking;
        existingMedicalData.Alcohol = medicalDataModel.Alcohol;
        existingMedicalData.PeanutsAllergy = medicalDataModel.PeanutsAllergy;
        existingMedicalData.ShellfishAllergy = medicalDataModel.ShellfishAllergy;
        existingMedicalData.DairyAllergy = medicalDataModel.DairyAllergy;
        existingMedicalData.GlutenAllergy = medicalDataModel.GlutenAllergy;
        existingMedicalData.PollenAllergy = medicalDataModel.PollenAllergy;
        existingMedicalData.OtherAllergies = medicalDataModel.OtherAllergies;

        _context.MedicalData.Update(existingMedicalData);
        await _context.SaveChangesAsync();

        return Ok(existingMedicalData);
    }

    // DELETE: api/MedicalData/delete/{id}
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteMedicalData(int id)
    {
        var medicalData = await _context.MedicalData.FindAsync(id);
        if (medicalData == null)
        {
            return NotFound(new { Message = "Medical data not found" });
        }

        _context.MedicalData.Remove(medicalData);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Medical data deleted successfully" });
    }

    // DELETE: api/MedicalData/deleteAll
    [HttpDelete("deleteAll")]
    public async Task<IActionResult> DeleteAllMedicalData()
    {
        try
        {
            var medicalDataList = await _context.MedicalData.ToListAsync();
            _context.MedicalData.RemoveRange(medicalDataList);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "All medical data entries have been deleted." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
       }
    }
}
