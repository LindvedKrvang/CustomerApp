using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerAppDAL.Context;
using CustomerAppDAL.Entities;

namespace CustomerAppDAL.Repositories
{
    internal class AddressRepository : IAddressRepository
    {
        private readonly CustomerAppContext _context;

        public AddressRepository(CustomerAppContext context)
        {
            _context = context;
        }

        public Address Create(Address address)
        {
            _context.Addresses.Add(address);
            return address;
        }

        public List<Address> GetAll()
        {
            return _context.Addresses.ToList();
        }

        public IEnumerable<Address> GetAllById(List<int> ids)
        {
            return ids == null ? null : _context.Addresses.Where(a => ids.Contains(a.Id));
        }

        public Address Get(int id)
        {
            return _context.Addresses.FirstOrDefault(a => a.Id == id);
        }

        public Address Delete(int id)
        {
            var addressToBeDeleted = Get(id);
            _context.Addresses.Remove(addressToBeDeleted);
            return addressToBeDeleted;
        }
    }
}
