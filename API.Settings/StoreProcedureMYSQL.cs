using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Settings
{
    public static class StoreProcedureMYSQL
    {
        public static string SP_CreateCustomer = "SP_CreateCustomer";
        public static string SP_DeleteCustomer = "SP_DeleteCustomer";
        public static string SP_GetCustomerByCustomerID = "SP_GetCustomerByCustomerID";
        public static string SP_GetCustomersPageWise = "SP_GetCustomersPageWise";
        public static string SP_UpdateCustomer = "SP_UpdateCustomer";
    }
}
