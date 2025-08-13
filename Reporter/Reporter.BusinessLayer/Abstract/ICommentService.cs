using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporter.EntityLayer.Entities;

namespace Reporter.BusinessLayer.Abstract
{
   public interface ICommentService:IGenericService<Comment>
    {
        List<Comment> TGetCommentsByArticleId(int id);
    }
}
