﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ICartRepository: IRepository<Cart>
    {
        ICollection<Cart> GetAllCartInclude(string cookieId);
    }
}
