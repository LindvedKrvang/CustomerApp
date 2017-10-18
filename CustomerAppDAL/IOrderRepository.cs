using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppDAL.Entities;

namespace CustomerAppDAL
{
    public interface IOrderRepository
    {
        //C
        Order CreateOrder(Order order);
        //R
        List<Order> GetAllOrders();
        Order GetOrder(int id);
        //D
        Order DeleteOrder(int id);
    }
}
