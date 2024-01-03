using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public MemberRepository(AppDbContext context) : base(context)
        {

        }
        
        public Member GetMemberId(int id)
        {
            return _appDbContext.Member.FirstOrDefault(x => x.Id == id);
        }

        public Member GetMemberMail(string mail)
        {
            return _appDbContext.Member.FirstOrDefault(x => x.Email == mail);
        }
    }
}
