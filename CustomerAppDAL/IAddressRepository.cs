using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppDAL.Entities;

namespace CustomerAppDAL
{
    public interface IAddressRepository
    {
        Address Create(Address address);

        List<Address> GetAll();
        IEnumerable<Address> GetAllById(List<int> ids);
        Address Get(int id);

        Address Delete(int id);
    }
}