using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult List(int productPage = 1) =>
            View(new ProductsListViewModel
                {
                    Products = _repository.Products
                        .OrderBy(p => p.ProductID)
                        .Skip((productPage - 1) * PageSize)
                        .Take(PageSize),

                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemPerPage = PageSize,
                        TotalItems = _repository.Products.Count()
                    }
                }
            );
    }
}
