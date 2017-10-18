using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerAppBLL.BusinessObjects;
using CustomerAppDAL.Entities;

namespace CustomerAppBLL.Converters
{
    internal class CustomerConverter
    {
        private readonly AddressConverter _addressConverter;

        public CustomerConverter()
        {
            _addressConverter = new AddressConverter();
        }

        internal Customer Convert(CustomerBO cust)
        {
            if (cust == null) return null;
            return new Customer() {
                Id = cust.Id,
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                Addresses = cust.AddressIds?.Select(aId => new CustomerAddress()
                {
                    AddresId = aId,
                    CustomerId = cust.Id
                }).ToList()
            };
        }

        internal CustomerBO Convert(Customer cust)
        {
            if (cust == null) return null;
            return new CustomerBO() {
                Id = cust.Id,
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                AddressIds = cust.Addresses?.Select(ca => ca.AddresId).ToList(),
                //Addresses = cust.Addresses?.Select(ca => new AddressBO()
                //{
                //    Id = ca.CustomerId,
                //    City = ca.Address?.City,
                //    Number = ca.Address?.Number,
                //    Street = ca.Address?.Street
                //}).ToList()
            };
        }
    }
}
