using API.BusinessLogic.Interface.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.UnityOfWork.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        #region Properties

        ICustomerServices CustomerRepository { get; }
        ILoginServices UserLoginRepository { get; }

        #endregion

        #region Methods

        Task CompleteAsync();

        #endregion
    }
}
