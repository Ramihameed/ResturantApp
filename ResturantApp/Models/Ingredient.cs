namespace ResturantApp.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        public string name { get; set; }

        public ICollection<ProductIngredient> ProductsIngredients { get; set; }

    }
}
