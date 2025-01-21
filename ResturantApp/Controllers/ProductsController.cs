using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturantApp.Data;
using ResturantApp.Models;

namespace ResturantApp.Controllers
{
    public class ProductsController : Controller
    {

        private Repository<Product> Product;

        private Repository<Ingredient> Ingredients;

        private Repository<Category> Category;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            this.Product = new Repository<Product>(context);
            this.Ingredients = new Repository<Ingredient>(context);
            this.Category = new Repository<Category>(context);
            this._webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await Product.GetAllAsync());
        }


        [HttpGet]

        public async Task<IActionResult> AddEdit(int id , Product product)
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
                product = Product.GetByIdAsync(id, new QueryOptions<Product> { Includes = "ProductIngredients" }).Result;
                ViewBag.Operation = "Edit";
                return View(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Product product , int[] IngredientIds , int catId)
        {
            if (ModelState.IsValid) 
            {
                if (product.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string FilePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var FileStream = new FileStream(FilePath, FileMode.Create) )

                    {
                        await product.ImageFile.CopyToAsync(FileStream);
                    }

                    product.ImageUrl = uniqueFileName;
                }

                if (product.ProductId == 0 )
                {
                    ViewBag.Ingredients = await Ingredients.GetAllAsync();

                    ViewBag.Category = await Category.GetAllAsync();

                    product.CategoryId = catId;

                    foreach (int i in IngredientIds) 
                    {
                        product.ProductIngredients?.Add(new  ProductIngredient { ProductId = i, IngredientId = i });

                    }

                    await Product.AddAsync(product);
                    return RedirectToAction("Index","Products");

                }
                else
                {
                    var existingProduct = await Product.GetByIdAsync(product.ProductId , new QueryOptions<Product> { Includes = "ProductIngredients" });

                    if (existingProduct == null)
                    {
                        ModelState.AddModelError("", "Product Not Found");

                        ViewBag.Ingredients = await Ingredients.GetAllAsync();

                        ViewBag.Category = await Category.GetAllAsync();

                        return View(product);
                    }
                    else
                    {



                        existingProduct.Name = product.Name;
                        existingProduct.Description = product.Description;
                        existingProduct.Price = product.Price;
                        existingProduct.Stock = product.Stock;
                        existingProduct.CategoryId = catId;

                        // update products Ingredients

                        existingProduct.ProductIngredients?.Clear();

                        foreach (int id in IngredientIds)

                        {
                            product.ProductIngredients?.Add(new ProductIngredient { ProductId = product.ProductId, IngredientId = id });

                        }
                        await Product.UpdateAsync(existingProduct);
                    }
                    return RedirectToAction("Index", "Products");
                }
            }
            else
            {
                return RedirectToAction("Index", "Products");
            }


        }


    }


}








    

