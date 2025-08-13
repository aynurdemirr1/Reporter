namespace Reporter.PresentationLayer.Models
{
    public class CommentWithToxicityViewModel
    {
        public Reporter.EntityLayer.Entities.Comment Comment { get; set; }
        public bool IsToxic { get; set; }
        public double ToxicityScore { get; set; }
    }
}
