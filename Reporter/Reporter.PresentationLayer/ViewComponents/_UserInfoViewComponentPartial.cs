using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reporter.EntityLayer.Entities;

namespace Reporter.PresentationLayer.ViewComponents
{
    public class _UserInfoViewComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _UserInfoViewComponentPartial(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = new UserInfoViewModel
            {
                FullName = user != null ? user.Name + " " + user.Surname : "Misafir",
                ImageUrl = user?.ImageUrl ?? "/sneat-1.0.0/assets/img/avatars/default.png"
            };
            return View(model);
        }
    }

    public class UserInfoViewModel
    {
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
    }

}
