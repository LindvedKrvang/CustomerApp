using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerAppBLL.BusinessObjects

{
    public class CustomerBO
    {
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Id { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public List<int> AddressIds { get; set; }
        public List<AddressBO> Addresses { get; set; }
    }
}
