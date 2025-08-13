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
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        private readonly ArticleContext _context;
        public EfCategoryDal(ArticleContext context) : base(context)
        {
            _context = context;
        }

        public int GetCategoryCount()
        {
            return _context.Categories.Count();
        }
    }
}