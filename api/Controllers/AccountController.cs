using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;    

public class AccountController : ApiController
{
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser, string> _signInManager;

    public AccountController()
    {
        _userManager = new UserManager<AppUser>(new UserStore<AppUser>(new AppDbContext()));
        _signInManager = new SignInManager<AppUser, string>(_userManager, HttpContext.Current.GetOwinContext().Authentication);
    }
}