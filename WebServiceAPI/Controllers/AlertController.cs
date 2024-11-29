using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DND1.Data;
using DND1.Models;
using System.Threading.Tasks;

namespace DND1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlertController(AppDbContext context)
        {
            _context = context;
        }

        // Create a new alert
        [HttpPost]
        public async Task<IActionResult> CreateAlert([FromBody] Alert alert)
        {
            if (alert == null)
                return BadRequest("Invalid alert data.");

            // Ensure at least one RelatedItemID is provided
            if (alert.MoleImageID == null && alert.MRIImageID == null && alert.XrayImageID == null)
                return BadRequest("At least one related item must be provided (MoleImageID, MRIImageID, XrayImageID).");

            await _context.Alerts.AddAsync(alert);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Alert created successfully", Alert = alert });
        }

        // Get all alerts
        [HttpGet]
        public async Task<IActionResult> GetAllAlerts()
        {
            var alerts = await _context.Alerts
                .Include(a => a.User)
                .Include(a => a.MoleImage)
                .Include(a => a.MRIImage)
                .Include(a => a.XrayImage)
                .ToListAsync();
            return Ok(alerts);
        }

        // Get alert by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlertById(int id)
        {
            var alert = await _context.Alerts
                .Include(a => a.User)
                .Include(a => a.MoleImage)
                .Include(a => a.MRIImage)
                .Include(a => a.XrayImage)
                .FirstOrDefaultAsync(a => a.AlertID == id);

            if (alert == null)
                return NotFound("Alert not found.");

            return Ok(alert);
        }

        // Update an alert
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlert(int id, [FromBody] Alert updatedAlert)
        {
            if (id != updatedAlert.AlertID)
                return BadRequest("Alert ID mismatch.");

            var alert = await _context.Alerts.FindAsync(id);
            if (alert == null)
                return NotFound("Alert not found.");

            // Update properties
            alert.UserID = updatedAlert.UserID;
            alert.IsRead = updatedAlert.IsRead;
            alert.RelatedItemType = updatedAlert.RelatedItemType;
            alert.MoleImageID = updatedAlert.MoleImageID;
            alert.MRIImageID = updatedAlert.MRIImageID;
            alert.XrayImageID = updatedAlert.XrayImageID;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Alert updated successfully", Alert = alert });
        }

        // Delete an alert
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(int id)
        {
            var alert = await _context.Alerts.FindAsync(id);
            if (alert == null)
                return NotFound("Alert not found.");

            _context.Alerts.Remove(alert);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Alert deleted successfully" });
        }
    }
}
