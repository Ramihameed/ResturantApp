using Microsoft.AspNetCore.Mvc;
using ResturantApp.Data;
using ResturantApp.Models;

namespace ResturantApp.Controllers
{
    public class IngredientController : Controller
    {

        private Repository<Ingredient> ingredients;

        public IngredientController(ApplicationDbContext context)
        {
            this.ingredients = new Repository<Ingredient>(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await ingredients.GetAllAsync());
        }
    }
}
