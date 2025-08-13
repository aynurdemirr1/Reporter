using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reporter.EntityLayer.Entities;
using Reporter.PresentationLayer.Models;

namespace Reporter.PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRegisterViewModel model)
        {
            AppUser appUser = new AppUser()
            {
                Email = model.Email,
                UserName = model.Username,
                Name = model.Name,
                Surname = model.Surname,
                ImageUrl = "test",
                Description = "Test1"
            };
            await _userManager.CreateAsync(appUser, model.Password);
            return RedirectToAction("UserLogin", "Login");
        }
    }
}