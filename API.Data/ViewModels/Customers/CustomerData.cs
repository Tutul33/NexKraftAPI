using API.Data.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.ViewModels.Customers
{
    public class CustomerData:Paging
    {
        public string Search { get; set; } = "";
    }
}
