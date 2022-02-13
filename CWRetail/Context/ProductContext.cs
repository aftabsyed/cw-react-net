using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWRetail.Model;
using Microsoft.EntityFrameworkCore;

namespace CWRetail.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        { }
        
        public  DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasData(
                    new List<Product>
                    {
                        new Product { Id = 1, Name = "Product A", Price = 9.99, Type = "Electronics", Active = true},
                        new Product { Id = 2, Name = "Product B", Price = 4.99, Type = "Toys", Active = false},
                        new Product { Id = 3, Name = "Product C", Price = 29.99, Type = "Furniture", Active = true},
                        new Product { Id = 4, Name = "Product D", Price = 13.99, Type = "Books", Active = true},
                        new Product { Id = 5, Name = "Product E", Price = 11.50, Type = "Toys", Active = false},
                        new Product { Id = 6, Name = "Product F", Price = 17.10, Type = "Furniture", Active = false},
                        new Product { Id = 7, Name = "Product G", Price = 8.99, Type = "Toys", Active = true},
                        new Product { Id = 8, Name = "Product H", Price = 14.00, Type = "Books", Active = false}
                    }
                );

            base.OnModelCreating(modelBuilder);
        }


    }
}