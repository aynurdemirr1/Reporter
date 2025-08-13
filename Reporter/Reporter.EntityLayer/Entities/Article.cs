using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporter.EntityLayer.Entities
{
   public class Article
    {public int ArticleId { get; set; }
		public string Title { get; set; }
		public DateTime CreatedDate { get; set; }
		public string Content { get; set; }
		public string CoverImageUrl { get; set; }
		public string MainImageUrl { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public string? AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		public List<Comment> Comments { get; set; }
		public string Slug { get; set; }
    }
}
