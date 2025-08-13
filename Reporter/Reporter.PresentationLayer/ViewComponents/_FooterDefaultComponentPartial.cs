using Microsoft.AspNetCore.Mvc;

namespace Reporter.PresentationLayer.ViewComponents
{
    public class _FooterDefaultComponentPartial:ViewComponent
    {
    public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
