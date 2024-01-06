using Core.Models;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CommentsService : Services<Comments>, ICommentsService
    {
        public CommentsService(IUnitOfWork unitOfWork, IRepository<Comments> repository) : base(unitOfWork, repository)
        {
        }

        public List<Comments> GetAllInclude()
        {
            return _unitOfWork.Comments.GetAllInclude();
        }
    }
}
