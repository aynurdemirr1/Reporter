using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reporter.BusinessLayer.Abstract;
using Reporter.EntityLayer.Entities;

namespace Reporter.PresentationLayer.Controllers
{

    [Route("[controller]/[action]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IToxicityDetectionService _toxicityDetectionService;
        private readonly ITranslationService _translationService;

        public CommentController(
            ICommentService commentService,
            UserManager<AppUser> userManager,
            IToxicityDetectionService toxicityDetectionService,
            ITranslationService translationService) // Çeviri servisi eklendi
        {
            _commentService = commentService;
            _userManager = userManager;
            _toxicityDetectionService = toxicityDetectionService;
            _translationService = translationService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            try
            {
                if (comment == null || string.IsNullOrWhiteSpace(comment.CommentDetail))
                    return Json(new { status = "error", message = "Yorum boş olamaz." });

                string translatedText = await _translationService.TranslateToEnglishAsync(comment.CommentDetail);

                if (string.IsNullOrWhiteSpace(translatedText))
                    translatedText = comment.CommentDetail; // çeviri başarısızsa orijinali kullan


                var toxicityResult = await _toxicityDetectionService.DetectToxicityAsync(translatedText);

                if (toxicityResult.IsToxic)
                {
                    return Json(new
                    {
                        status = "toxic",
                        message = "Yorumunuz toksik içerik barındırıyor, gönderilemedi."
                    });
                }

                comment.AppUserId = _userManager.GetUserId(User);
                comment.CommentDate = DateTime.Now;
                comment.IsToxic = false;
                comment.ToxicityScore = (float)toxicityResult.Score;

                _commentService.TInsert(comment);

                return Json(new
                {
                    status = "success",
                    message = "Yorumunuz başarıyla kaydedildi.",
                    comment = new
                    {
                        username = User.Identity.Name,
                        date = comment.CommentDate.ToString("g"),
                        text = comment.CommentDetail
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = "Bir hata oluştu: " + ex.Message });
            }
        }
    }
}
