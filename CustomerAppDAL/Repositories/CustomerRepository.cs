using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerAppDAL.Context;
using CustomerAppDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAppDAL.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerAppContext _context;

        public CustomerRepository(CustomerAppContext context)
        {
            _context = context;
        }

        public Customer CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.Include(c => c.Addresses).ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Include(c => c.Addresses).FirstOrDefault(c => c.Id == id);
        }

        public Customer DeleteCustomer(int id)
        {
            var customer = GetCustomer(id);
            _context.Customers.Remove(customer);
            return customer;
        }
    }
}
