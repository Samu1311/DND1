using Microsoft.AspNetCore.Mvc;
using DND1.Data;
using DND1.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // For async LINQ methods
using System;

namespace DND1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // Create User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
            {
                return BadRequest("Invalid user data. Email and Password are required.");
            }

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "User created successfully", User = user });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        // Get User by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound("User not found.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }

        // Update User
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.UserID)
                return BadRequest("User ID mismatch.");

            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound("User not found.");

                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Email = updatedUser.Email;
                user.PasswordHash = updatedUser.PasswordHash;
                user.DateOfBirth = updatedUser.DateOfBirth;
                user.Gender = updatedUser.Gender;

                await _context.SaveChangesAsync();
                return Ok(new { Message = "User updated successfully", User = user });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        // Delete User
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound("User not found.");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }

        // Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving users: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }
    }
}
