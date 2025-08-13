using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporter.EntityLayer.Entities;

namespace Reporter.BusinessLayer.Abstract
{
   public interface IArticleService:IGenericService<Article>
    {
        public List<Article> TGetArticlesByCategoryId1();
        public List<Article> TGetArticlesWithAppUser();
        public List<Article> TGetArticlesWithCategories();
        public List<Article> TGetArticlesWithCategoriesAndAppUsers();
        public Article TGetArticlesWithAuthorAndCategoriesById(int id);
        List<Article> TGetArticlesByAuthor(string id);

        Article TGetBySlug(string slug);
    }
}

