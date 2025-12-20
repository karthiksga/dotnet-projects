using Microsoft.AspNetCore.Mvc;
using TokenGenerator.Services;

namespace TokenGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthTokenController : ControllerBase
    {
        private readonly JwtService _jwtService;

        // Demo users — in real project use database
        private List<User> Users = new()
     {
         new User { Email="admin@gmail.com", Password="admin123", Role="Admin" },
         new User { Email="teacher@gmail.com", Password="teach123", Role="Teacher" },
         new User { Email="student@gmail.com", Password="stud123", Role="Student" },
     };

        public AuthTokenController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("generate")]
        public IActionResult GenerateToken([FromBody] LoginDTO login)
        {
            var user = Users.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            string token = _jwtService.GenerateToken(user.Email, user.Role);

            return Ok(new { token });
        }
    }
}
