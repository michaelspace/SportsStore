using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            // arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable<Product>());

            var target = new AdminController(mock.Object);

            // act
            Product[] result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

            // assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        [Fact]
        public void Can_Edit_Product()
        {
            // arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable<Product>());

            var target = new AdminController(mock.Object);

            // act
            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));
            Product p3 = GetViewModel<Product>(target.Edit(3));

            // assert
            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }

        [Fact]
        public void Cannot_Edit_Notexisting_Product()
        {
            // arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable<Product>());

            var target = new AdminController(mock.Object);

            // act
            Product p4 = GetViewModel<Product>(target.Edit(4));

            // assert
            Assert.Null(p4);
        }

        [Fact]
        public void Can_Save_Valid_Data()
        {
            // arrange
            var mock = new Mock<IProductRepository>();
            var tempData = new Mock<ITempDataDictionary>();
            var target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };

            Product p = new Product {Name = "P1"};

            // act
            IActionResult result = target.Edit(p);

            // assert
            mock.Verify(m => m.SaveProduct(p)); // sprawdzenie czy wywołano

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Data()
        {
            // arrange
            var mock = new Mock<IProductRepository>();
            var target = new AdminController(mock.Object);

            Product p = new Product {Name = "P1"};

            target.ModelState.AddModelError("error", "error");

            // act
            IActionResult result = target.Edit(p);

            // assert
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Delete_Valid_Product()
        {
            // arrange 
            Product product = new Product
            {
                ProductID = 2,
                Name = "P2"
            };

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                product,
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable<Product>());

            var target = new AdminController(mock.Object);

            // act
            target.Delete(product.ProductID);

            // assert
            mock.Verify(m => m.DeleteProduct(product.ProductID));
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
