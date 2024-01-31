
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoundSphere.Helpers;
using SoundSphere.Models;
using SoundSphere.Helpers;
using SoundSphere.Models;
using SoundSphere.ViewModels;

namespace SoundSphere.Controllers;

public class AppAccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppDatabaseContext _context;

    public AppAccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDatabaseContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public IActionResult Login()
    {
        var response = new AppLoginVM();
        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Login(AppLoginVM loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVM);
        }
        var searched_user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
        if (searched_user != null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(searched_user, loginVM.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(searched_user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Concert");
                }
            }
            TempData["Error"] = "Wrong credential password. Please, try again";
            return View(loginVM);
        }
        else
        {
            TempData["Error"] = "Wrong credential email. Please, try again";
            return View(loginVM);
        }
    }

    public IActionResult Register()
    {
        var response = new AppRegisterVM();
        return View(response);
    }
    [HttpPost]
    public async Task<IActionResult> Register(AppRegisterVM appRegisterVM)
    {
        if (!ModelState.IsValid) return View(appRegisterVM);

        var user = await _userManager.FindByEmailAsync(appRegisterVM.EmailAddress);
        if (user != null)
        {
            TempData["Error"] = "This email address is already in use";
            return View(appRegisterVM);
        }

        var newUser = new AppUser
        {
            Email = appRegisterVM.EmailAddress,
            UserName = appRegisterVM.EmailAddress
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, appRegisterVM.Password);

        if (newUserResponse.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Concert");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Concert");
    }
}