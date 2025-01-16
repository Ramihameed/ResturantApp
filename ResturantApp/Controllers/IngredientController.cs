using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

        public async Task<IActionResult> Details(int id)
        {
            return View(await ingredients.GetByIdAsync(id, new QueryOptions<Ingredient>() {Includes = "ProductsIngredients.Product" }));
                                                                  
        }

        // Ingredient/Create

        [HttpGet]

        public IActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create([Bind("IngredientId,name")] Ingredient Ingredient)
        {
            if (ModelState.IsValid)
            {
                await ingredients.AddAsync(Ingredient);
                return RedirectToAction("Index");
            }
            return View(Ingredient);
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await ingredients.GetByIdAsync(id, new QueryOptions<Ingredient> { Includes ="ProductsIngredients"} ));
        }
    }
}

        
    

