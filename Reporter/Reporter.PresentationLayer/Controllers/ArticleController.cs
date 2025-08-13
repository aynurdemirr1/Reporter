using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reporter.BusinessLayer.Abstract;
using Reporter.DataAccessLayer.Context;

namespace Reporter.PresentationLayer.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ArticleContext _context;

        public ArticleController(IArticleService articleService, ArticleContext context)
        {
            _articleService = articleService;
            _context = context;
        }

        [Route("Article/ArticleDetail/{slug}")]
        public IActionResult ArticleDetail(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return RedirectToAction("Index", "Default");

            var article = _context.Articles
                                  .Include(a => a.Category)
                                  .Include(a => a.AppUser)
                                  .Include(a => a.Comments)
                                  .FirstOrDefault(a => a.Slug.ToLower() == slug.ToLower());

            if (article == null)
                return NotFound();

            ViewBag.id = article.ArticleId;
            return View(article);
        }
    }
}
