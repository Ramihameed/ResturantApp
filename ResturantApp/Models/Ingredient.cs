using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ResturantApp.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        public string name { get; set; }

        [ValidateNever]
        public ICollection<ProductIngredient> ProductsIngredients { get; set; }

    }
}
