using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.Infrastructure
{
    public class AdoNetUnitOfWork : IUnitOfWork
    {
        #region Fields and Properties

        private readonly IAdoNetDbFactory _dbFactory;

        private AdoNetDbContext _dbContext;

        private IDbTransaction _transaction;

        public AdoNetDbContext DbContext
        {
            get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        }

        #endregion


        #region Constructors

        public AdoNetUnitOfWork(IAdoNetDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
            //_transaction = DbContext.Connection.BeginTransaction();
        }

        #endregion

        #region Methods

        public IDbCommand CreateCommand()
        {
            var command = DbContext.Connection.CreateCommand();

            command.Transaction = _transaction;

            return command;
        }

        public void BeginTransaction()
        {
            _transaction = _transaction ?? (_transaction = DbContext.Connection.BeginTransaction());
        }

        public void SaveChanges()
        {
            if (_transaction == null) return;

            _transaction.Commit();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            if (DbContext.Connection != null)
            {
                DbContext.Connection.Dispose();
                //DbContext.Connection.Close();
                //DbContext.Connection = null;
            }
        }

        #endregion

    }
}
