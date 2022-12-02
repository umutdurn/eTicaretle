using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<HomeColumn>? HomeColumns { get; set; }
        public DbSet<ColumnDetail>? columnDetail { get; set; }
        public DbSet<Screen>? Screen { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<Galleries> Galleries { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<District> District { get; set; }
    }
}
