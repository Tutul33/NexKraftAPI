using API.Data.ORM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<UserLogin?> GetUserInfo(string userName, string password);
    }
}
