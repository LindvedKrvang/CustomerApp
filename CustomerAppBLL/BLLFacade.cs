using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppBLL.Services;
using CustomerAppDAL;

namespace CustomerAppBLL
{
    public class BLLFacade
    {   
        public ICustomerService CustomerService => new CustomerService(new DALFacade());
        public IOrderService OrderService => new OrderService(new DALFacade());
        public IAddressService AddressService => new AddressService(new DALFacade());
    }
}