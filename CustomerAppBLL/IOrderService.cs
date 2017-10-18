using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppBLL.BusinessObjects;

namespace CustomerAppBLL
{
    public interface IOrderService
    {
        //C
        OrderBO CreateOrder(OrderBO order);
        //R
        List<OrderBO> GetAllOrders();
        OrderBO GetOrder(int id);
        //U
        OrderBO UpdateOrder(OrderBO order);
        //D
        OrderBO DeleteOrder(int id);
    }
}
