using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporter.DataAccessLayer.Abstract;
using Reporter.DataAccessLayer.Context;

namespace Reporter.DataAccessLayer.Repositories 
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly ArticleContext _context;
        public GenericRepository(ArticleContext context)
        {
            _context = context;
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetListAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Delete(int id)
        {
            var value = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(value);
            _context.SaveChanges();
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
