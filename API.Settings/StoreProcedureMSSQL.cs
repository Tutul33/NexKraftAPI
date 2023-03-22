using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Settings
{
    public static class StoreProcedureMSSQL
    {
        public static string SP_CreateCustomer = "[dbo].[SP_CreateCustomer]";
        public static string SP_DeleteCustomer = "[dbo].[SP_DeleteCustomer]";
        public static string SP_GetCustomerByCustomerID = "[dbo].[SP_GetCustomerByCustomerID]";
        public static string SP_GetCustomersPageWise = "[dbo].[SP_GetCustomersPageWise]";
        public static string SP_UpdateCustomer = "[dbo].[SP_UpdateCustomer]";
    }
}
