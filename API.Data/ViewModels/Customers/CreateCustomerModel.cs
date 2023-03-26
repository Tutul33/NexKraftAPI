using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.ViewModels.Customers
{
    public class CreateCustomerModel
    {
        [Required]
        [MinLength(1)]
        public string? CustomerName { get; set; }
        public string? Country { get; set; }
    }
}
