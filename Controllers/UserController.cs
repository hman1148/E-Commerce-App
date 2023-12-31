using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace E_Commerce.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {

        private readonly MongoDBContext _context;

        public UserController(MongoDBContext context)
        {
            this._context = context;
        }

        // POST to /usrs/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel model)
        {
            var existingUser = await _context.Users.Find(u => u.Email == model.Email).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return BadRequest(new { message = "User already exists with those credentials" });
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = hashedPassword,
            };
            await _context.Users.InsertOneAsync(user);

            return Ok(new { message = "User registered successfully" });

        }

    }

}
