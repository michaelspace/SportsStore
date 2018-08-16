using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class EFOrderRepository: IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public EFOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders
            .Include(c => c.Lines)
            .ThenInclude(d => d.Product);

        public void SaveOrder(Order order)
        {

            _context.AttachRange(order.Lines.Select(s => s.Product));
            if (order.OrderID == 0)
            {
                _context.Orders.Add(order);
            }

            _context.SaveChanges();
        }
    }
}
