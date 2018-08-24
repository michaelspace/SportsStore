using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController: Controller
    {
        private readonly IProductRepository _repository;

        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index() => 
            View(_repository.Products);

        public ViewResult Edit(int productId) =>
            View(_repository.Products.FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
                TempData["msg"] = $"Zapisano {product.Name}.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product productToDelete = _repository.DeleteProduct(productId);
            if (productToDelete != null)
            {
                TempData["msg"] = $"Usunięto {productToDelete.Name}.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Orders()
        {
            if (User?.Identity?.Name != "Admin")
            {
                return Redirect("/Error");
            }
            return Redirect("/Order/List");
        } 
    }
}
