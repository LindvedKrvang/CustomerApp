using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAppDAL.Entities
{
    public class CustomerAddress
    {
        public int CustomerId { get; set; }
        public int AddresId { get; set; }

        public Customer Customer { get; set; }
        public Address Address { get; set; }
    }
}
