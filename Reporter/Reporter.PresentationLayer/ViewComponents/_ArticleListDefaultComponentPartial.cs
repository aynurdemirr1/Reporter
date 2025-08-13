using Microsoft.AspNetCore.Mvc;
using Reporter.BusinessLayer.Abstract;


namespace Reporter.PresentationLayer.ViewComponents
{
    public class _ArticleListDefaultComponentPartial : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _ArticleListDefaultComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _articleService.TGetArticlesWithCategoriesAndAppUsers().OrderByDescending(x => x.CreatedDate)
               .Take(9)
               .ToList();
            return View(values);
        }
    }
}