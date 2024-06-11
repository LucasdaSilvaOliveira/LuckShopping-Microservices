using LuckShopping.Web.Models;
using LuckShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LuckShopping.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts()!;

            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(model);
                if(response != null) return RedirectToAction("ProductIndex");
            }
            return View(model);
        }

        public async Task<IActionResult> ProductUpdate(long id)
        {
            var model = await _productService.FindProductById(id);
            if(model != null) return View(model);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if(ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(model);

                if(response != null)
                {
                    return RedirectToAction("ProductIndex");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(long id)
        {
            var model = await _productService.FindProductById(id);
            if (model != null) return View(model);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            var response = await _productService.DeleteProductById(model.Id);

            if (response) return RedirectToAction("ProductIndex");

            return View(model);
        }
    }
}
