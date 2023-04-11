using API.RepositoryManagement.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.UnityOfWork.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        #region Properties

        ICustomerRepository CustomerRepository { get; }
        ILoginRepository UserLoginRepository { get; }

        #endregion

        #region Methods

        Task CompleteAsync();

        #endregion
    }
}
