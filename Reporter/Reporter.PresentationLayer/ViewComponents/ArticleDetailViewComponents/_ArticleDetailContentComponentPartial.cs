using Microsoft.AspNetCore.Mvc;
using Reporter.BusinessLayer.Abstract;


namespace Reporter.PresentationLayer.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailContentComponentPartial : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _ArticleDetailContentComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var value = _articleService.TGetArticlesWithAuthorAndCategoriesById(id);
            return View(value);
        }
    }
}