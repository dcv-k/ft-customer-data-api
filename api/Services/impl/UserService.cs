using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

public class UserService
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _dbContext;

    public UserService(IConfiguration configuration, AppDbContext dbContext)
    {
        this._configuration = configuration;
        this._dbContext = dbContext;
    }

    public User RegisterUser(UserDTO dto)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Username = dto.Username,
            Password = passwordHash,
            Type = dto.Type
        };

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return user;
    }

    public string Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return null;
        }

        var user = _dbContext.Users.FirstOrDefault(c => c.Username == username);

        if (user == null)
        {
            return null;
        }

        if (!VerifyPassword(password, user.Password))
        {
            return null;
        }

        return CreateToken(user);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Type ?? throw new InvalidOperationException("Role cannot be null."))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Key").Value
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
