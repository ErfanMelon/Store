﻿using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.HomePages;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;

namespace Store.Application.Interfaces.Context
{
    public interface IDataBaseContext
    {
         DbSet<User> Users { get; set; }
         DbSet<Role> Roles { get; set; }
         DbSet<Category> Categories { get; set; }
         DbSet<Product> Products { get; set; }
         DbSet<ProductFeatures> ProductFeatures { get; set; }
         DbSet<ProductImages> ProductImages { get; set; }
         DbSet<ProductBrand> ProductBrands { get; set; }
         DbSet<Comment> Comments { get; set; }
         DbSet<Banner> Banners { get; set; }
         DbSet<Cart> Carts { get; set; }
         DbSet<ProductsInCart> ProductsInCarts { get; set; }
         DbSet<RequestPay> RequestPays { get; set; }
         DbSet<Order> Orders { get; set; }
         DbSet<OrderDetail> OrderDetails { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
