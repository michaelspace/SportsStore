using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // arrange
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            var order = new Order();
            var target = new OrderController(mock.Object, cart);

            // act
            ViewResult result = target.Checkout(order) as ViewResult;

            // assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_Shopping_Details()
        {
            // arrange
            var mock = new Mock<IOrderRepository>();

            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            var target = new OrderController(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");

            // act
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            // assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            // arrange
            var mock = new Mock<IOrderRepository>();

            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            var target = new OrderController(mock.Object, cart);

            // act
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            // assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

            Assert.Equal("Completed", result.ActionName);
        }
    }
}
