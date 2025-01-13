using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResturantApp.Models;

namespace ResturantApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<ProductIngredient> ProductIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define Composite Key
            builder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });

            // Define one to many relationship and a foreign key
            builder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);
            // Define one to many relationship and a foreign key
            builder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(i => i.ProductsIngredients)
                .HasForeignKey(pi => pi.IngredientId);

            // Seeding Data in the non user tables

            builder.Entity<Category>().HasData(
                
             new Category { CategoryId = 1, CategoryName = "Appetizer"},
             new Category { CategoryId = 2, CategoryName = "Entree" },
             new Category { CategoryId = 3, CategoryName = "Side Dish" },
             new Category { CategoryId = 4, CategoryName = "Dessert" },
             new Category { CategoryId = 5, CategoryName = "Beverage" }
             );

            builder.Entity<Ingredient>().HasData(
             new Ingredient { IngredientId = 1, name = "Beef"},
             new Ingredient { IngredientId = 2, name = "Chicken" },
             new Ingredient { IngredientId = 3, name = "Fish" },
             new Ingredient { IngredientId = 4, name = "Tortilla" },
             new Ingredient { IngredientId = 5, name = "Lettuce" },
             new Ingredient { IngredientId = 6, name = "Tomato" }
             );

            builder.Entity<Product>().HasData(
             new Product { ProductId = 1,
                 Name = "Beef Taco",
                 Description="A Delicious Beef Taco",
                 Price=3.5m,
                 Stock=100,
                 CategoryId=2},
             new Product { ProductId = 2,
                 Name = "Chicken Taco",
                 Description = "A Delicious Chicken Taco",
                 Price = 1.99m,
                 Stock = 120,
                 CategoryId = 2},
             new Product { ProductId = 3,
                 Name = "Fish Taco",
                 Description = "A Delicious Fish Taco",
                 Price = 4.99m,
                 Stock = 90,
                 CategoryId = 2}
             );

            builder.Entity<ProductIngredient>().HasData(

             new ProductIngredient { ProductId = 1, IngredientId = 1},
             new ProductIngredient { ProductId = 1, IngredientId = 4 },
             new ProductIngredient { ProductId = 1, IngredientId = 5 },
             new ProductIngredient { ProductId = 1, IngredientId = 6 },
             new ProductIngredient { ProductId = 2, IngredientId = 2 },
             new ProductIngredient { ProductId = 2, IngredientId = 4 },
             new ProductIngredient { ProductId = 2, IngredientId = 5 },
             new ProductIngredient { ProductId = 2, IngredientId = 6 },
             new ProductIngredient { ProductId = 3, IngredientId = 3 },
             new ProductIngredient { ProductId = 3, IngredientId = 4 },
             new ProductIngredient { ProductId = 3, IngredientId = 5 },
             new ProductIngredient { ProductId = 3, IngredientId = 6 }
             );
        }

    }


}
