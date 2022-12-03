﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Country : EntityBase
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public ICollection<Cargo> Cargos { get; set; } = new HashSet<Cargo>();
    }
}
