﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            // arrange
            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            // act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            CartLine[] results = target.Lines.ToArray();

            // assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing()
        {
            // arrange
            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            // act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);

            CartLine[] results = target.Lines
                .OrderBy(p => p.Product.ProductID).ToArray();

            // assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // arrange
            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };
            Product p3 = new Product() { ProductID = 3, Name = "P3" };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // act
            target.RemoveLine(p2);

            // assert
            Assert.Equal(0, target.Lines.Count(p => p.Product == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Can_Calculate_Total()
        {
            // arrange
            Product p1 = new Product() { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product() { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 4);

            // act
            decimal result = target.ComputeTotalValue();

            // assert
            Assert.Equal(550M, result);
        }

        [Fact]
        public void Can_Clear_Content()
        {
            // arrange
            Product p1 = new Product() { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product() { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // act
            target.Clear();

            // assert
            Assert.Empty(target.Lines);
        }
    }
}
