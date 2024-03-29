﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetAllInclude();
        Order GetAllIncludeOrderId(string orderId);
        Order GetAllIncludeId(int id);
    }
}
