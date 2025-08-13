using Microsoft.AspNetCore.Mvc;

namespace Reporter.PresentationLayer.ViewComponents.ArticleDetailViewComponents
{

    public class _ArticleDetailAddCommentComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(int articleId)
        {
            ViewBag.ArticleId = articleId; // ArticleId'yi View'e gönderiyoruz
            return View();
        }
    }
}