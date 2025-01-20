using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ResturantApp.Models
{
    public class Product
    {


        public Product()
        {
            this.ProductIngredients = new List<ProductIngredient>();
        }
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public int CategoryId { get; set; }

        [NotMapped]

        public IFormFile? ImageFile { get; set; }

        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";

        [ValidateNever]
        public Category? Category { get; set; } // A product belongs to an Category

        [ValidateNever]
        public ICollection<OrderItem>? OrderItem { get; set; } // A product can be in many orderitems

        [ValidateNever]
        public ICollection<ProductIngredient>? ProductIngredients { get; set; } // A product can have many ingredients
    }
}
