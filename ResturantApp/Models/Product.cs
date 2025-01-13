using System.ComponentModel;

namespace ResturantApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; } // A product belongs to an Category

        public ICollection<OrderItem>? OrderItem { get; set; } // A product can be in many orderitems

        public ICollection<ProductIngredient>? ProductIngredients { get; set; } // A product can have many ingredients
    }
}
