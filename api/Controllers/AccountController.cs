using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
    private readonly IAuthService _authService; // Add this line

    public AccountController(UserManager<AppUser> userManager, IAuthService authService) // Add IAuthService parameter
    {
        _userManager = userManager;
        _authService = authService; // Assign the injected authService
    }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO dto)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = dto.Email, Email = dto.Email };
                var result = await _userManager.CreateAsync(user, dto.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, dto.Roles);
                    return Ok("Registration successful");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }

            return BadRequest("Invalid registration data");
        }

        [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
    {
        if (ModelState.IsValid)
        {
            var isValid = await _authService.ValidateUser(dto); 
            if (isValid)
            {
                var token = await _authService.CreateUser();
                return Ok(new { token = token });
            }
            else
            {
                return BadRequest("Invalid login attempt");
            }
        }

        return BadRequest("Invalid login data");
    }
    }


