
using Microsoft.AspNetCore.Mvc;

namespace Reporter.PresentationLayer.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
