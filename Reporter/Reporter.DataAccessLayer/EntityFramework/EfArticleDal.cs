using Microsoft.EntityFrameworkCore;
using Reporter.DataAccessLayer.Abstract;
using Reporter.DataAccessLayer.Context;
using Reporter.DataAccessLayer.Repositories;
using Reporter.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporter.DataAccessLayer.EntityFramework
{
    public class EfArticleDal : GenericRepository<Article>, IArticleDal
    {
        private readonly ArticleContext _context;

        public EfArticleDal(ArticleContext context) : base(context)
        {
            _context = context;
        }

        public List<Article> GetArticlesByCategoryId1()
        {
            var values = _context.Articles.Where(x => x.CategoryId == 1).ToList();
            return values;
        }

        public List<Article> GetArticlesWithAppUser()
        {
            var values = _context.Articles.ToList();
            return values;
        }

        public Article GetArticlesWithAuthorAndCategoriesById(int id)
        {
            var values = _context.Articles.Include(x => x.AppUser).Include(y => y.Category).Where(z => z.ArticleId == id).FirstOrDefault();
            return values;
        }

        public List<Article> GetArticlesWithCategories()
        {
            return _context.Articles.Include(x => x.Category).ToList();
        }

        public List<Article> GetArticlesWithCategoriesAndAppUsers()
        {
            return _context.Articles.Include(x => x.Category).Include(y => y.AppUser).ToList();
        }

        public List<Article> GetArticlesWithUser()
        {
            var values = _context.Articles.ToList();
            return values;
        }

        public List<Article> GetArticlesByAuthor(string id)
        {
            var values = _context.Articles.Where(x => x.AppUserId == id).ToList();
            return values;
        }

        public Article GetBySlug(string slug)
        {
            return _context.Articles.Include(a => a.Category).Include(a => a.AppUser).FirstOrDefault(a => a.Slug == slug);
        }
    }
}