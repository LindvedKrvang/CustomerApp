using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerAppBLL.BusinessObjects;
using CustomerAppBLL.Converters;
using CustomerAppDAL;

namespace CustomerAppBLL.Services
{
    internal class OrderService : IOrderService
    {
        private readonly OrderConverter _converter = new OrderConverter();
        private readonly DALFacade _facade;

        public OrderService(DALFacade facade)
        {
            _facade = facade;
        }

        public OrderBO CreateOrder(OrderBO order)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var orderEntity = uow.OrderRepository.CreateOrder(_converter.Convert(order));
                uow.Complete();
                return _converter.Convert(orderEntity);
            }
        }

        public List<OrderBO> GetAllOrders()
        {
            using (var uow = _facade.UnitOfWork)
            {
                return uow.OrderRepository.GetAllOrders().Select(o => _converter.Convert(o)).ToList();
            }
        }

        public OrderBO GetOrder(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var orderEntity = uow.OrderRepository.GetOrder(id);
                orderEntity.Customer = uow.CustomerRepository.GetCustomer(orderEntity.CustomerId);
                return _converter.Convert(orderEntity);
            }
        }

        public OrderBO UpdateOrder(OrderBO order)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var orderEntity = uow.OrderRepository.GetOrder(order.Id);

                if(orderEntity == null) throw new InvalidOperationException("Order not found");

                orderEntity.OrderDate = order.OrderDate;
                orderEntity.DeliveryDate = order.DeliveryDate;
                orderEntity.CustomerId = order.CustomerId;

                uow.Complete();
                //BLL choice
                orderEntity.Customer = uow.CustomerRepository.GetCustomer(orderEntity.CustomerId);
                return _converter.Convert(orderEntity);
            }
        }

        public OrderBO DeleteOrder(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var order = uow.OrderRepository.DeleteOrder(id);
                uow.Complete();
                return _converter.Convert(order);
            }
        }
    }
}
