using Microsoft.AspNetCore.Mvc;
using ResturantApp.Data;
using ResturantApp.Models;

namespace ResturantApp.Controllers
{
    public class ProductsController : Controller
    {

        private Repository<Product> Product;

        private Repository<Ingredient> Ingredients;

        private Repository<Category> Category;
        public ProductsController(ApplicationDbContext context)
        {
            this.Product = new Repository<Product>(context);
            this.Ingredients = new Repository<Ingredient>(context);
            this.Category = new Repository<Category>(context);
        }
        public async Task<IActionResult> Index()
        {
            return View(await Product.GetAllAsync());
        }


        [HttpGet]

        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.Ingredients = await Ingredients.GetAllAsync();
            ViewBag.Category = await Category.GetAllAsync();
            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new Product());
            }
            else
            {
                ViewBag.Operation = "Edit";
                return View();
            }
        }
    }
}







    

