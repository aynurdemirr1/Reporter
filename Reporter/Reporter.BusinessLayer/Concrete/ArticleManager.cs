using Reporter.BusinessLayer.Abstract;
using Reporter.DataAccessLayer.Abstract;
using Reporter.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporter.BusinessLayer.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IArticleDal _articleDal;
        private readonly ISlugService _slugService;

        public ArticleManager(IArticleDal articleDal, ISlugService slugService)
        {
            _articleDal = articleDal;
            _slugService = slugService;
        }

        public Article TGetBySlug(string slug)
        {
            return _articleDal.GetBySlug(slug);
        }

        public void TDelete(int id)
        {
            _articleDal.Delete(id);
        }

        public List<Article> TGetArticlesByAuthor(string id)
        {
            return _articleDal.GetArticlesByAuthor(id);
        }

        public List<Article> TGetArticlesByCategoryId1()
        {
            return _articleDal.GetArticlesByCategoryId1();
        }

        public Article TGetArticlesWithAuthorAndCategoriesById(int id)
        {
            return _articleDal.GetArticlesWithAuthorAndCategoriesById(id);
        }

        public List<Article> TGetArticlesWithCategories()
        {
            return _articleDal.GetArticlesWithCategories();
        }

        public List<Article> TGetArticlesWithCategoriesAndAppUsers()
        {
            return _articleDal.GetArticlesWithCategoriesAndAppUsers();
        }

        public List<Article> TGetArticlesWithUser()
        {
            return _articleDal.GetArticlesWithUser();
        }

        public Article TGetById(int id)
        {
            return _articleDal.GetById(id);
        }

        public List<Article> TGetListAll()
        {
            return _articleDal.GetListAll();
        }

        public void TInsert(Article entity)
        {
            // Slug mutlaka üretilecek
            entity.Slug = _slugService.GenerateSlug(entity.Title);

            // Gerekli alanlar kontrol edilerek kayıt yapılır
            if (!string.IsNullOrEmpty(entity.Title) && entity.CategoryId != 0)
            {
                _articleDal.Insert(entity);
            }
        }

        public void TUpdate(Article entity)
        {
            // Güncellenen başlığa göre slug da yenilenmeli
            entity.Slug = _slugService.GenerateSlug(entity.Title);
            _articleDal.Update(entity);
        }

        public List<Article> TGetArticlesWithAppUser()
        {
            throw new NotImplementedException();
        }
    }
}
