using Microsoft.AspNetCore.Mvc;
using Reporter.BusinessLayer.Abstract;

namespace Reporter.PresentationLayer.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IArticleService _articleService;
        private const int PageSize = 9;

        public DefaultController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index(int page = 1)
        {
            var allArticles = _articleService.TGetArticlesWithCategoriesAndAppUsers()
                .OrderByDescending(x => x.CreatedDate);

            var paginatedArticles = allArticles
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)allArticles.Count() / PageSize);
            ViewBag.CurrentPage = page;

            return View(paginatedArticles);
        }
    }

}
