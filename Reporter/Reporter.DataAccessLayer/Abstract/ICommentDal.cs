using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporter.EntityLayer.Entities;

namespace Reporter.DataAccessLayer.Abstract
{
    public interface ICommentDal : IGenericDal<Comment>
    {
        List<Comment> GetCommentsByArticleId(int id);
    }
}