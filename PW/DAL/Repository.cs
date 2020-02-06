using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PW.DAL
{
    public class Repository : IDisposable, IRepository
    {
        #region System
        private PWEntities _db;
        public PWEntities db
        {
            get
            {
                if (_db == null)
                    _db = new PWEntities();
                return _db;
            }
            set
            {
                _db = value;
            }
        }
        private bool _disposed = false;
        public Repository(PWEntities db)
        {
            if (db == null) this.db = new PWEntities();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                   if (db != null) Dispose(true);
                }
                db = null;
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        #endregion
        #region pw_users
        public IQueryable<pw_users> GetUsers()
        {
            var res = db.pw_users;
            return res;
        }
        public pw_users GetUser(int ID)
        {
            var res = db.pw_users.FirstOrDefault(x => x.id == ID);
            return res;
        }
        public pw_users GetUser(string email)
        {
            var res = db.pw_users.FirstOrDefault(x => x.email == email);
            return res;
        }
        public int SaveUser(pw_users element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.pw_users.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = System.Data.Entity.EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteUser(int ID)
        {
            bool res = false;
            var item = db.pw_users.SingleOrDefault(x => x.id == ID);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region pw_tokens
        public IQueryable<pw_tokens> GetTokens()
        {
            var res = db.pw_tokens;
            return res;
        }
        public pw_tokens GetToken(int ID)
        {
            var res = db.pw_tokens.FirstOrDefault(x => x.id == ID);
            return res;
        }
        public int SaveToken(pw_tokens element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.pw_tokens.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = System.Data.Entity.EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteToken(int ID)
        {
            bool res = false;
            var item = db.pw_tokens.SingleOrDefault(x => x.id == ID);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        //#region pw_senderTransactions
        //public IQueryable<pw_senderTransactions> GetSentTransactions()
        //{
        //    var res = db.pw_senderTransactions;
        //    return res;
        //}
        //public pw_senderTransactions GetSentTransaction(int ID)
        //{
        //    var res = db.pw_senderTransactions.FirstOrDefault(x => x.id == ID);
        //    return res;
        //}
        //public int SaveSentTransaction(pw_senderTransactions element, bool withSave = true)
        //{
        //    if (element.id == 0)
        //    {
        //        db.pw_senderTransactions.Add(element);
        //        if (withSave) Save();
        //    }
        //    else
        //    {
        //        db.Entry(element).State = System.Data.Entity.EntityState.Modified;
        //        if (withSave) Save();
        //    }
        //    return element.id;
        //}
        //public bool DeleteSentTransaction(int ID)
        //{
        //    bool res = false;
        //    var item = db.pw_senderTransactions.SingleOrDefault(x => x.id == ID);
        //    if (item != null)
        //    {
        //        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
        //        Save();
        //        res = true;
        //    }
        //    return res;
        //}
        //#endregion
        //#region pw_recipientTransactions
        //public IQueryable<pw_recipientTransactions> GetReceivedTransactions()
        //{
        //    var res = db.pw_recipientTransactions;
        //    return res;
        //}
        //public pw_recipientTransactions GetReceivedTransaction(int ID)
        //{
        //    var res = db.pw_recipientTransactions.FirstOrDefault(x => x.id == ID);
        //    return res;
        //}
        //public int SaveReceivedTransaction(pw_recipientTransactions element, bool withSave = true)
        //{
        //    if (element.id == 0)
        //    {
        //        db.pw_recipientTransactions.Add(element);
        //        if (withSave) Save();
        //    }
        //    else
        //    {
        //        db.Entry(element).State = System.Data.Entity.EntityState.Modified;
        //        if (withSave) Save();
        //    }
        //    return element.id;
        //}
        //public bool DeleteReceivedTransaction(int ID)
        //{
        //    bool res = false;
        //    var item = db.pw_senderTransactions.SingleOrDefault(x => x.id == ID);
        //    if (item != null)
        //    {
        //        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
        //        Save();
        //        res = true;
        //    }
        //    return res;
        //}
        //#endregion
        //#region pw_senderRecipient
        //public IQueryable<pw_senderRecipient> GetRelationshipsSR()
        //{
        //    var res = db.pw_senderRecipient;
        //    return res;
        //}
        //public pw_senderRecipient GetRelationshipSR(int ID)
        //{
        //    var res = db.pw_senderRecipient.FirstOrDefault(x => x.id == ID);
        //    return res;
        //}
        //public int SaveRelationshipSR(pw_senderRecipient element, bool withSave = true)
        //{
        //    if (element.id == 0)
        //    {
        //        db.pw_senderRecipient.Add(element);
        //        if (withSave) Save();
        //    }
        //    else
        //    {
        //        db.Entry(element).State = System.Data.Entity.EntityState.Modified;
        //        if (withSave) Save();
        //    }
        //    return element.id;
        //}
        //public bool DeleteRelationshipSR(int ID)
        //{
        //    bool res = false;
        //    var item = db.pw_senderRecipient.SingleOrDefault(x => x.id == ID);
        //    if (item != null)
        //    {
        //        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
        //        Save();
        //        res = true;
        //    }
        //    return res;
        //}
        //#endregion
        #region pw_transactions
        public IQueryable<pw_transactions> GetTransactions()
        {
            var res = db.pw_transactions;
            return res;
        }
        public pw_transactions GetTransaction(int ID)
        {
            var res = db.pw_transactions.FirstOrDefault(x => x.id == ID);
            return res;
        }
        public int SaveTransaction(pw_transactions element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.pw_transactions.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = System.Data.Entity.EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteTransaction(int ID)
        {
            bool res = false;
            var item = db.pw_transactions.SingleOrDefault(x => x.id == ID);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
    }
}