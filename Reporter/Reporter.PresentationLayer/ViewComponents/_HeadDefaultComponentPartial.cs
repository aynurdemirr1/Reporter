using Microsoft.AspNetCore.Mvc;

namespace Reporter.PresentationLayer.ViewComponents
{
    public class _HeadDefaultComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
