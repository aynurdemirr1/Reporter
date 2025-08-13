using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reporter.BusinessLayer.Abstract;
using Reporter.EntityLayer.Entities;

namespace Reporter.PresentationLayer.Controllers
{
    public class AuthorController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;


        public AuthorController(UserManager<AppUser> userManager, IArticleService articleService, ICategoryService categoryService, ICommentService commentService)
        {
            _userManager = userManager;
            _articleService = articleService;
            _categoryService = categoryService;
            _commentService = commentService;
        }

        public async Task<IActionResult> CreateArticle()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.id = user.Id;
            ViewBag.name = user.Name + " " + user.Surname;
            List<SelectListItem> values = (from x in _categoryService.TGetListAll()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString()
                                           }).ToList();
            ViewBag.categories = values;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateArticle(Article article)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            article.AppUserId = user.Id;
            article.CreatedDate = DateTime.Now;
            _articleService.TInsert(article);
            return RedirectToAction("Index", "Default");
        }

        public IActionResult DeleteArticle(int id)
        {
            var article = _articleService.TGetById(id);
            if (article == null)
                return NotFound();

            _articleService.TDelete(article.ArticleId);

            return RedirectToAction("MyArticleList");
        }

        public IActionResult DeleteComment(int id)
        {
            var comment = _commentService.TGetById(id);
            if (comment == null)
                return NotFound();

            _commentService.TDelete(comment.CommentId);

            return RedirectToAction("MyCommentList");
        }

        public async Task<IActionResult> MyArticleList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _articleService.TGetArticlesByAuthor(user.Id);
            return View(values);
        }

        public async Task<IActionResult> MyCommentList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _commentService.TGetListAll();
            return View(values);
        }
    }
}