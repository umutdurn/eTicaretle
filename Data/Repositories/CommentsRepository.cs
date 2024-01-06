using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CommentsRepository : Repository<Comments>, ICommentsRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public CommentsRepository(AppDbContext context) : base(context)
        {
        }

        public List<Comments> GetAllInclude()
        {
            var comments  = _appDbContext.Comments.Where(x => x.MainComment == null && x.Situation == true).Include(x => x.MainComment).Include(x => x.SubComments).ToList();

            return comments;
        }
    }
}
