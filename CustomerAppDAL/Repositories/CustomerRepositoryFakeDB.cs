using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerAppDAL.Entities;

namespace CustomerAppDAL.Repositories
{
    internal class CustomerRepositoryFakeDb : ICustomerRepository
    {
        //private static int Id = 1;
        private static readonly List<Customer> _customers = new List<Customer>()
        {
            new Customer()
            {
                //Id = Id++,
                FirstName = "Rasmus",
                LastName = "Lindved",
            },

            new Customer()
            {
                //Id = Id++,
                FirstName = "Lars",
                LastName = "Bilde",
            }
        };

        public Customer CreateCustomer(Customer customer)
        {
            Customer newCustomer;
            _customers.Add(newCustomer = new Customer() {
                //Id = Id++,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Addresses = customer.Addresses
            });
            return newCustomer;
        }

        public List<Customer> GetAllCustomers()
        {
            return new List<Customer>(_customers);
        }

        public Customer GetCustomer(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer DeleteCustomer(int id)
        {
            var customerToRemove = GetCustomer(id);
            _customers.Remove(customerToRemove);
            return customerToRemove;
        }
    }
}
