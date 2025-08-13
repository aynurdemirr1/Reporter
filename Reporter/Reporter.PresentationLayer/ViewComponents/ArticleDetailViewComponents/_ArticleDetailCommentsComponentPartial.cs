using Microsoft.AspNetCore.Mvc;
using Reporter.BusinessLayer.Abstract;


namespace Reporter.PresentationLayer.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailCommentsComponentPartial : ViewComponent
    {
        private readonly ICommentService _commentService;
        private readonly IToxicityDetectionService _toxicityDetectionService;

        public _ArticleDetailCommentsComponentPartial(
            ICommentService commentService,
            IToxicityDetectionService toxicityDetectionService)
        {
            _commentService = commentService;
            _toxicityDetectionService = toxicityDetectionService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var comments = _commentService.TGetCommentsByArticleId(id);

            foreach (var comment in comments)
            {
                var toxicity = await _toxicityDetectionService.DetectToxicityAsync(comment.CommentDetail);
                comment.IsToxic = toxicity.IsToxic;
                comment.ToxicityScore = (float)toxicity.Score;
            }

            return View(comments);
        }
    }
}