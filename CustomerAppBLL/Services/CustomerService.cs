using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerAppBLL.BusinessObjects;
using CustomerAppBLL.Converters;
using CustomerAppDAL;
using CustomerAppDAL.Entities;

namespace CustomerAppBLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerConverter _converter = new CustomerConverter();
        private readonly AddressConverter _addressConverter = new AddressConverter();
        private readonly DALFacade _facade;

        public CustomerService(DALFacade facade)
        {
            _facade = facade;
        }

        public CustomerBO CreateCustomer(CustomerBO customer)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var newCustomer = uow.CustomerRepository.CreateCustomer(_converter.Convert(customer));
                uow.Complete();
                return _converter.Convert(newCustomer);
            }
        }

        public void CreateCustomer(List<CustomerBO> customers)
        {
            using (var uow = _facade.UnitOfWork)
            {
                foreach (var customer in customers)
                {
                    uow.CustomerRepository.CreateCustomer(_converter.Convert(customer));
                }
                uow.Complete();
            }
        }

        public List<CustomerBO> GetAllCustomers()
        {
            using (var uow = _facade.UnitOfWork)
            {
                return uow.CustomerRepository.GetAllCustomers().Select(_converter.Convert).ToList();
            }
        }

        public CustomerBO GetCustomer(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                //1. Get and convert the customer.
                var customer = _converter.Convert(uow.CustomerRepository.GetCustomer(id));
                //2. Get All related Addresses from AddressRepository using addressIds.
                //3. Convert and Add the addresses to the CustomerBO.

                //customer.Addresses = customer.AddressIds?.Select(aId => _addressConverter.Convert(uow.AddressRepository.Get(aId))).ToList();

                customer.Addresses = uow.AddressRepository.GetAllById(customer.AddressIds)
                    .Select(a => _addressConverter.Convert(a)).ToList();
                //4.Return the customer.
                return customer;
            }
        }

        public CustomerBO UpdateCustomer(CustomerBO customer)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var customerFromDb = uow.CustomerRepository.GetCustomer(customer.Id);

                if (customerFromDb == null)
                    throw new NullReferenceException("No customer was found");

                var customerUpdated = _converter.Convert(customer);
                customerFromDb.FirstName = customerUpdated.FirstName;
                customerFromDb.LastName = customerUpdated.LastName;
                //customerFromDb.Addresses = customerUpdated.Addresses;

                //1. Remove All, except the "old" ids we wanna keep
                customerFromDb.Addresses.RemoveAll(ca => !customerUpdated.Addresses
                    .Exists(a => a.AddresId == ca.AddresId && a.CustomerId == ca.CustomerId));

                //2. Remove All ids already in database from customerUpdate
                customerUpdated.Addresses.RemoveAll(ca =>
                    customerFromDb.Addresses.Exists(a => a.AddresId == ca.AddresId && a.CustomerId == ca.CustomerId));

                //3. Add All new customerAddresses not yet seen in the DB
                customerFromDb.Addresses.AddRange(customerUpdated.Addresses);

                uow.Complete();

                return _converter.Convert(customerFromDb);
            }
        }

        public CustomerBO DeleteCustomer(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var deletedCustomer = uow.CustomerRepository.DeleteCustomer(id);
                uow.Complete();
                return _converter.Convert(deletedCustomer);
            }
        }
    }
}