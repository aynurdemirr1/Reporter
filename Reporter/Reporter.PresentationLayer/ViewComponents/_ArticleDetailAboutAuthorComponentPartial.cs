using Microsoft.AspNetCore.Mvc;
using Reporter.BusinessLayer.Abstract;

namespace Reporter.PresentationLayer.ViewComponents
{
    public class _ArticleDetailAboutAuthorComponentPartial : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _ArticleDetailAboutAuthorComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var article = _articleService.TGetArticlesWithAuthorAndCategoriesById(id);
            var author = article?.AppUser;

            if (author == null)
            {
                return Content("Yazar bilgisi bulunamadı.");
            }

            return View(author);
        }
    }
}
