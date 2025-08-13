using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporter.DataAccessLayer.Abstract;
using Reporter.DataAccessLayer.Context;
using Reporter.DataAccessLayer.Repositories;
using Reporter.EntityLayer.Entities;

namespace Reporter.DataAccessLayer.EntityFramework
{
    public class EfSliderDal : GenericRepository<Slider>, ISliderDal
    {
        public EfSliderDal(ArticleContext context) : base(context)
        {
        }
    }
}
