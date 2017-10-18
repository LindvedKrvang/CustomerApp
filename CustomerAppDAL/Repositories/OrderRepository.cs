using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerAppDAL.Context;
using CustomerAppDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAppDAL.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly CustomerAppContext _context;

        //private static int Id = 1;

        public OrderRepository(CustomerAppContext context)
        {
            _context = context;
        }

        public Order CreateOrder(Order order)
        {
            //order.Id = Id++;
            _context.Orders.Add(order);
            return order;
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public Order DeleteOrder(int id)
        {
            var order = GetOrder(id);
            _context.Orders.Remove(order);
            return order;
        }
    }
}
