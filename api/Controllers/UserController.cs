using System;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _dbContext;

    public UserController(IConfiguration configuration, AppDbContext dbContext)
    {
        this._configuration = configuration;
        this._dbContext = dbContext;
    }

    [HttpPost("register", Name = "register")]
    public IActionResult Register(UserDTO dto)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User {
            Username = dto.Username,
            Password = passwordHash,
            Type = dto.Type
        };
        
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return Ok(user);
    }

    [HttpGet("login", Name = "login")]
    public IActionResult Login([FromQuery] string Username, string Password)
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            return BadRequest("Username and password are required.");
        }

        var user = _dbContext.Users.FirstOrDefault(c => c.Username == Username);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (!VerifyPassword(Password, user.Password ?? throw new InvalidOperationException("Password cannot be null.")))
        {
            return BadRequest("Invalid password.");
        }

        string token = CreateToken(user);

        return new JsonResult(new { token });
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username ?? throw new InvalidOperationException("Username cannot be null."))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value.PadRight(64)
        ));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1), 
            signingCredentials: cred 
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    } 

    private bool VerifyPassword(string enteredPassword, string savedPasswordHash)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, savedPasswordHash);
    } 

} 