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
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register", Name = "register")]
    public IActionResult Register(UserDTO dto)
    {
        var user = _userService.RegisterUser(dto);
        return Ok(user);
    }

    [HttpGet("login", Name = "login")]
    public IActionResult Login([FromQuery] string username, string password)
    {
        var token = _userService.Login(username, password);
        if (token == null)
        {
            return BadRequest("Invalid username or password.");
        }

        return new JsonResult(new { token });
    }
}
