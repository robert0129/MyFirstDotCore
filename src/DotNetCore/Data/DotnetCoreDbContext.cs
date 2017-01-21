using DotNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.Data
{
    public class DotnetCoreDbContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //}
        public DotnetCoreDbContext(DbContextOptions<DotnetCoreDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Account> Accounts {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
        }
    }
}
