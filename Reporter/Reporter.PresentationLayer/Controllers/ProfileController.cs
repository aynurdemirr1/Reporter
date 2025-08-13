using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reporter.EntityLayer.Entities;
using Reporter.PresentationLayer.Models;

public class ProfileController : Controller
{
    private readonly UserManager<AppUser> _userManager;

    public ProfileController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }


    [HttpGet]
    public async Task<IActionResult> UserDetail()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        var model = new UserUpdateViewModel
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Username = user.UserName,
            PhoneNumber = user.PhoneNumber,
            ImageUrl = user.ImageUrl,
        };
        ViewBag.userImage = model.ImageUrl;
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> UserDetail(UserUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        user.Name = model.Name;
        user.Surname = model.Surname;
        user.Email = model.Email;
        user.UserName = model.Username;
        user.PhoneNumber = model.PhoneNumber;
        user.ImageUrl = model.ImageUrl;

        if (!string.IsNullOrEmpty(model.Password))
        {
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
        }

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            TempData["Success"] = "Profil başarıyla güncellendi.";
            return RedirectToAction("Dashboard", "Author");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        return RedirectToAction("Index", "Default");
    }

}