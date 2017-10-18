using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppBLL.BusinessObjects;

namespace CustomerAppBLL
{
    public interface IAddressService
    {
        AddressBO Create(AddressBO address);

        List<AddressBO> GetAll();
        AddressBO Get(int id);

        AddressBO Update(AddressBO address);

        AddressBO Delete(int id);
    }
}
