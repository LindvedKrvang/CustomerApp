using System.Collections.Generic;
using CustomerAppBLL.BusinessObjects;

namespace CustomerAppBLL
{
    /// <summary>
    /// I made a contract!
    /// </summary>
    public interface ICustomerService
    {
        //C
        CustomerBO CreateCustomer(CustomerBO customer);
        //R
        List<CustomerBO> GetAllCustomers();
        CustomerBO GetCustomer(int id);
        //U
        CustomerBO UpdateCustomer(CustomerBO customer);
        //D
        CustomerBO DeleteCustomer(int id);
    }
}