using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.ViewModels.Customers
{
    public class vmCustomer
    {
        public int? CustomerID { get; set; }
        [Required]
        [MinLength(1)]
        public string? CustomerName { get; set; }
        public string? Country { get; set; }
        public int? RecordCount { get; set; }
    }
    public class vmCustomerUpdate
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Amount between 1 and 2147483647.")]
        public int? CustomerID { get; set; }
        [Required]
        [MinLength(1)]
        public string? CustomerName { get; set; }
        public string? Country { get; set; }
    }
}
