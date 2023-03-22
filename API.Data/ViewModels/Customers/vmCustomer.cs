using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.ViewModels.Customers
{
    public class vmCustomer
    {
        public int? CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? Country { get; set; }
        public int? RecordCount { get; set; }
    }
}
