using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporter.EntityLayer.Entities;

namespace Reporter.DataAccessLayer.Abstract
{
    public interface IArticleDal : IGenericDal<Article>
    {
        List<Article> GetArticlesByCategoryId1();
        List<Article> GetArticlesWithAppUser();
        List<Article> GetArticlesWithCategories();
        List<Article> GetArticlesWithCategoriesAndAppUsers();
        Article GetArticlesWithAuthorAndCategoriesById(int id);
        List<Article> GetArticlesByAuthor(string id);
        Article GetBySlug(string slug);
        List<Article> GetArticlesWithUser();
    }
}


