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
        public DbSet<City> City { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<BankTransfer> BankTransfer { get; set; }
        public DbSet<OrderSituation> OrderSituation { get; set; }
        public DbSet<ReturnOrder> ReturnOrder { get; set; }
        public DbSet<Coupons> Coupons { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasOne(x => x.District).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Order>().HasOne(x => x.District).WithMany().OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
