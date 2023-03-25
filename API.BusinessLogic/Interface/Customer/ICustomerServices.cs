﻿using API.Data.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Interface.Customer
{
    public interface ICustomerServices
    {
        Task<object?> DeleteCustomer(string param);
        Task<object?> GetCustomerByCustomerID(string param);
        Task<object?> GetCustomerList(string param);
        Task<object?> CreateCustomer(vmCustomer data);
        Task<object?> UpdateCustomer(vmCustomerUpdate data);
    }
}
