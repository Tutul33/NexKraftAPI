using API.RepositoryManagement.Base;
using API.RepositoryManagement.Base.Interfaces;
using API.RepositoryManagement.Repositories.Interfaces;
using API.Data.ORM.DataModels;
using API.Data.ViewModels.Customers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>,ICustomerRepository
    {
        private NexKraftDbContext? NexKraftDbContext => _dbContext as NexKraftDbContext;
        public CustomerRepository(NexKraftDbContext dbContext) : base(dbContext)
        {
        } 
        public async Task<Customer?> GetCustomerInfo(int customerId)
        {
            return  (await GetManyAsync(filter: u => u.CustomerId == customerId)).FirstOrDefault();
        }
    }
}
