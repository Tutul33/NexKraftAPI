using API.Data.ORM.MsSQLDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerInfo(int customerId);
    }
}
