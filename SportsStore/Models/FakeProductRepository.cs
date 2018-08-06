using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class FakeProductRepository: IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product {Name = "Piłka nożna", Price = 25M},
            new Product {Name = "Deska surfingowa", Price = 180M},
            new Product {Name = "Buty do biegania", Price = 90M}
        }.AsQueryable<Product>();
    }
}
