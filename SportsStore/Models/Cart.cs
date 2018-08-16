using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Cart
    {
        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = _lineCollection
                .FirstOrDefault(p => p.Product.ProductID == product.ProductID);

            if (line == null)
            {
                _lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) =>
            _lineCollection.RemoveAll(p => p.Product.ProductID == product.ProductID);

        public virtual decimal ComputeTotalValue() =>
            _lineCollection.Sum(p => p.Product.Price * p.Quantity);

        public virtual void Clear() => 
            _lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => _lineCollection;
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
