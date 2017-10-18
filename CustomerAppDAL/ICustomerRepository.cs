using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppDAL.Entities;

namespace CustomerAppDAL
{
    public interface ICustomerRepository
    {
        //C
        Customer CreateCustomer(Customer customer);
        //R
        List<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
        //D
        Customer DeleteCustomer(int id);
    }
}
