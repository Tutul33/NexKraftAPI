using API.RepositoryManagement.Base;
using API.RepositoryManagement.Repositories.Interfaces;
using API.Data.ORM.MsSQLDataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories
{
    public class UserLoginRepository : BaseRepository<UserLogin>,ILoginRepository
    {
        private NexKraftDbContext? NexKraftDbContext => _dbContext as NexKraftDbContext;
        public UserLoginRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public async Task<UserLogin?> GetUserInfo(string userName,string password)
        {
            return (await GetManyAsync(filter: u => u.UserName == userName && u.Password==password)).FirstOrDefault();
        }
    }
}
