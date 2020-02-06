using PW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.BLL
{
    public class Manager : IManager
    {
        #region System
        private IRepository _db;
        private bool _disposed;
        public Manager(IRepository db)
        {
            _db = db;
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_db != null) _db.Dispose();
                }
                _db = null;
                _disposed = true;
            }
        }
        #endregion
        IUsersManager _Users;
        public IUsersManager Users
        {
            get
            {
                if (_Users == null)
                    _Users = new UsersManager(_db);
                return _Users;
            }
            set
            {
                _Users = value;
            }
        }
        ITransactionsManager _Transactions;
        public ITransactionsManager Transactions
        {
            get
            {
                if (_Transactions == null)
                    _Transactions = new TransactionsManager(_db);
                return _Transactions;
            }
            set
            {
                _Transactions = value;
            }
        }
    }
}