using Microsoft.AspNetCore.Mvc;

namespace Reporter.PresentationLayer.ViewComponents
{
    public class _HeaderDefaultComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
    
}
