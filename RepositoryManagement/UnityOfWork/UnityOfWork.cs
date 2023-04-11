﻿
using API.RepositoryManagement.Repositories.Interfaces;
using API.Data.ORM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.RepositoryManagement.UnityOfWork.Interfaces;
using API.RepositoryManagement.Repositories;

namespace API.RepositoryManagement.UnityOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties

        private CustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository => _customerRepository ?? (_customerRepository = new CustomerRepository(_dbContext));

        private UserLoginRepository _loginRepository;
        public ILoginRepository UserLoginRepository => _loginRepository ?? (_loginRepository = new UserLoginRepository(_dbContext));

        
        #endregion

        #region Readonlys

        private readonly NexKraftDbContext _dbContext;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">The Database Context</param>
        public UnitOfWork(NexKraftDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #region Methods

        /// <summary>
        /// Completes the unit of work, saving all repository changes to the underlying data-store.
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task CompleteAsync() => await _dbContext.SaveChangesAsync();

        #endregion

        #region Implements IDisposable

        #region Private Dispose Fields

        private bool _disposed;

        #endregion

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        /// <returns><see cref="ValueTask"/></returns>
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);

            // Take this object off the finalization queue to prevent 
            // finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        /// <param name="disposing">Whether or not we are disposing</param>
        /// <returns><see cref="ValueTask"/></returns>
        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    await _dbContext.DisposeAsync();
                }

                // Dispose any unmanaged resources here...

                _disposed = true;
            }
        }

        #endregion
    }
}
