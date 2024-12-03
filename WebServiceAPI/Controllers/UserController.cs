using DND1.Models;
using DND1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DND1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public UserController(IConfiguration configuration, AppDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        // Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                return BadRequest("Email already exists.");
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = HashPassword(model.Password),
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                UserType = model.UserType ?? "Basic" // Default to "Basic"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { user.UserID, user.Email });
        }

        // Get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }


        // Get user by ID
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        // Update user
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateModel model)
        {
            if (id != model.UserID)
            {
                return BadRequest("User ID mismatch.");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.PasswordHash = HashPassword(model.Password);
            }
            user.DateOfBirth = model.DateOfBirth;
            user.Gender = model.Gender;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "User updated successfully", User = user });
        }

        // Delete user
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User deleted successfully" });
        }

        //Update UserType
        [HttpPost("upgrade")]
        public async Task<IActionResult> UpgradeUser([FromBody] UpgradeUserRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserID);
            if (user == null) return NotFound("User not found");

            user.UserType = request.UserType;
            await _context.SaveChangesAsync();

            // Generate a new token with updated UserType
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim("UserType", user.UserType)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

            public class UpgradeUserRequest
            {
                public int UserID { get; set; } // Add this property
                public string UserType { get; set; } // Ensure this matches the incoming request structure
            }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }

        public class UserRegistrationModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
    
        [Required]
        public string LastName { get; set; } = string.Empty;
    
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    
        public DateTime? DateOfBirth { get; set; }
    
        public string? Gender { get; set; }
        public string UserType { get; set; } = "Basic"; // Default to "Basic"
    }

    public class UserUpdateModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? UserType { get; set; }
    }
}