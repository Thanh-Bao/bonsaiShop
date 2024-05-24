using ASP.NET_Backend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/User/username
    [HttpGet("{username}")]
    public async Task<ActionResult<User>> GetUser(string username)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.username == username);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // POST: api/User
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        _context.User.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { username = user.username }, user);
    }


    private bool UserExists(string username)
    {
        return _context.User.Any(e => e.username == username);
    }

     // POST : /api/Users/login
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] User user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.username) || string.IsNullOrWhiteSpace(user.password))
        {
            return BadRequest("Invalid login request");
        }

        try
        {
            var _user = await _context.User.FirstOrDefaultAsync(u => u.username == user.username && u.password == user.password);
            if (_user != null)
            {
                var token = Authentication.GenerateJwtToken(user.username);
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var tokenS = jsonToken as JwtSecurityToken;
                string name_payload = tokenS.Claims.First(claim => claim.Type == "username").Value;
                var result = new
                {
                    username =name_payload,
                    token = token
                };

                Console.WriteLine("---------------------");
                Console.WriteLine(result);

                return Ok(result);
            }
        }
        catch
        {
            return Unauthorized("Đăng nhập thất bại");
        }

        return Unauthorized("Đăng nhập thất bại");
    }

}
