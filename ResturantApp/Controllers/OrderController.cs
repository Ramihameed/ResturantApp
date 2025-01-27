using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResturantApp.Data;
using ResturantApp.Models;
using Microsoft.AspNetCore.Authorization;
namespace ResturantApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Repository<Product> _products;
        private Repository<Order> _orders;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
            _products = new Repository<Product>(context);
            _orders = new Repository<Order>(context);
        }

        [Authorize]
        [HttpGet]

        
        public async Task<IActionResult> Create()
        {

            var model = HttpContext.Session.Get<OrderViewModel>("OderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _products.GetAllAsync()
            };


            return View(model);


        }

    }
}
