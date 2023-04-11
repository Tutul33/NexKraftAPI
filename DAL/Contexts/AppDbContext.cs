using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contexts
{
    public class AppDbContext : DbContext
    {
        #region MyRegion
        public DbSet<Customer> Todos { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">The options to use</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #endregion
        #region Methods

        /// <summary>
        /// Allows the database model to be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        #endregion
    }
}
