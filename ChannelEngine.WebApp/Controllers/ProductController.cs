using Microsoft.AspNetCore.Mvc;
using ChannelEngine.Application.Interfaces;

namespace ChannelEngine.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IOrdersService _ordersService;

        public ProductController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        //here we can add error filter or error handling middleware. skip for test project.
        public async Task<IActionResult> Index()
        {
            return View(await _ordersService.GetTopSoldProductsAndChangeStockForRandomOneAsync());
        }
    }
}