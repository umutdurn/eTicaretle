using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IMemberRepository : IRepository<Member>
    {
        Member GetMemberMail(string mail);
        Member GetMemberId(int id);
    }
}
