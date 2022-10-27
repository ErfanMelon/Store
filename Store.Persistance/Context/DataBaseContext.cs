using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Roles;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.HomePages;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;

namespace Store.Persistance.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
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
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductLikes> ProductLikes { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductsInCart> ProductsInCarts { get; set; }
        public DbSet<RequestPay> RequestPays { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedDataBase(modelBuilder);

            FilterDataBase(modelBuilder);

            RulesDataBase(modelBuilder);
        }

        private static void RulesDataBase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductFeatures>().HasKey(e => e.ProductId);
            modelBuilder.Entity<ProductImages>().HasKey(e => e.ProductId);
            modelBuilder.Entity<ProductBrand>().HasKey(e => e.BrandId);
            modelBuilder.Entity<ProductLikes>().HasKey(e => e.LikeId);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Cart>().HasMany(c => c.ItemsInCart).WithOne(i => i.Cart);
            modelBuilder.Entity<RequestPay>().HasKey(e => e.PayId);

            modelBuilder.Entity<RequestPay>().HasIndex(r => r.PayId).IsUnique();

            modelBuilder.Entity<Order>().HasOne(e => e.User).WithMany(e => e.Orders).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>().HasMany(e => e.OrderDetails).WithOne(e => e.Order);

        }

        private static void SeedDataBase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { RoleId = ((int)BaseRoles.Admin), RoleName = Enum.GetName(typeof(BaseRoles), BaseRoles.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role { RoleId = ((int)BaseRoles.Operator), RoleName = Enum.GetName(typeof(BaseRoles), BaseRoles.Operator) });
            modelBuilder.Entity<Role>().HasData(new Role { RoleId = ((int)BaseRoles.Customer), RoleName = Enum.GetName(typeof(BaseRoles), BaseRoles.Customer) });
            modelBuilder.Entity<ProductBrand>().HasData(new ProductBrand { BrandId = 1, Brand = "متفرقه" });
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
            modelBuilder.Entity<ProductLikes>().HasQueryFilter(pi => !pi.IsRemoved);
            modelBuilder.Entity<ProductBrand>().HasQueryFilter(pi => !pi.IsRemoved);
            modelBuilder.Entity<Banner>().HasQueryFilter(pi => !pi.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(pi => !pi.IsRemoved);
            modelBuilder.Entity<ProductsInCart>().HasQueryFilter(pi => !pi.IsRemoved);
            modelBuilder.Entity<RequestPay>().HasQueryFilter(pi => !pi.IsRemoved);
            modelBuilder.Entity<Order>().HasQueryFilter(pi => !pi.IsRemoved);
            modelBuilder.Entity<OrderDetail>().HasQueryFilter(pi => !pi.IsRemoved);
        }
    }
}
