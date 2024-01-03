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
    public class MemberService : Services<Member>, IMemberService
    {
        public MemberService(IUnitOfWork unitOfWork, IRepository<Member> repository) : base(unitOfWork, repository)
        {
        }

        public Member GetMemberId(int id)
        {
            return _unitOfWork.Member.GetMemberId(id);
        }

        public Member GetMemberMail(string mail)
        {
            return _unitOfWork.Member.GetMemberMail(mail);
        }
    }
}
