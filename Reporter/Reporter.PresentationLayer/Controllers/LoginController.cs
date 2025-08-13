using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reporter.EntityLayer.Entities;
using Reporter.PresentationLayer.Models;


namespace Reporter.PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

    }
}