using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Roles;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;
using System.Collections;

namespace Store.Persistance.Context
{
    public class DataBaseContext : DbContext,IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedDataBase(modelBuilder);

            FilterDataBase(modelBuilder);

            modelBuilder.Entity<ProductFeatures>().HasKey(e => e.ProductId);
            modelBuilder.Entity<ProductImages>().HasKey(e => e.ProductId);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }

        private static void SeedDataBase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { RoleId = ((int)BaseRoles.Admin), RoleName = Enum.GetName(typeof(BaseRoles), BaseRoles.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role { RoleId = ((int)BaseRoles.Operator), RoleName = Enum.GetName(typeof(BaseRoles), BaseRoles.Operator) });
            modelBuilder.Entity<Role>().HasData(new Role { RoleId = ((int)BaseRoles.Customer), RoleName = Enum.GetName(typeof(BaseRoles), BaseRoles.Customer) });
        }

        private static void FilterDataBase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsRemoved);
            modelBuilder.Entity<UserInRole>().HasQueryFilter(u => !u.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsRemoved);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductFeatures>().HasQueryFilter(pf => !pf.IsRemoved);
            modelBuilder.Entity<ProductImages>().HasQueryFilter(pi => !pi.IsRemoved);
        }
    }
}
