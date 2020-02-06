using PW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.BLL
{
    public class TransactionsManager: ITransactionsManager
    {
        #region System
        private IRepository _db;
        private bool _disposed;
        public TransactionsManager(IRepository db)
        {
            _db = db;
            _disposed = false;
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
                    if (_db != null)
                        _db.Dispose();
                _db = null;
                _disposed = true;
            }
        }
        private void _debug(Exception ex, Object parameters = null, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }
        #endregion
        #region pw_transactions
        public List<pw_transactions> GetTransactions(int id,out string msg)
        {
            msg = "";
            List<pw_transactions> res;
            if(_db.GetUser(id) == null)
            {
                msg = "Invalid user";
                res = null;
                return res;
            }
            try
            {
                   res = _db.GetTransactions().Where(x => x.usersID == id).Take(5).ToList();
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка документов");
                res = null;
            }
            return res;
        }
        public pw_transactions CreateTransaction(int id, string userName, string amount, out Dictionary<string, string> parameters)
        {
            pw_transactions res = new pw_transactions();
            //400: user not found
            //400: balance exceeded
            //401: Invalid user
            parameters = new Dictionary<string, string>();
            try
            {
                var userRecipient = _db.GetUsers().FirstOrDefault(u => u.userName == userName);
                var userSender = _db.GetUser(id);
                decimal amountDec = RDL.Convert.StrToDecimal(amount, 0);
                if (userSender == null)
                {
                    res = null;
                    parameters.Add("code", "401");
                    parameters.Add("msg", "Invalid user");
                    return res;
                }
                else
                {
                    if (userSender.balance < amountDec)
                    {
                        res = null;
                        parameters.Add("code", "400");
                        parameters.Add("msg", "balance exceeded");
                        return res;
                    }
                }
                if (userRecipient == null)
                {
                    res = null;
                    parameters.Add("code", "400");
                    parameters.Add("msg", "user not found");
                    return res;
                }
                
                var resRecipient = new pw_transactions()
                {
                    date = DateTime.Now,
                    amount = amountDec,
                    balance = userRecipient.balance + amountDec,
                    usersID = userRecipient.id,
                    userName = userSender.userName
                };
                _db.SaveTransaction(resRecipient);
                userRecipient.balance = resRecipient.balance;
                _db.SaveUser(userRecipient);
                res = new pw_transactions()
                {
                    date = DateTime.Now,
                    amount = -amountDec,
                    balance = userSender.balance - amountDec,
                    usersID = userSender.id,
                    userName = userRecipient.userName
                };
                _db.SaveTransaction(res);
                userSender.balance = res.balance;
                _db.SaveUser(userSender);
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании нового документа");
                res = null;
            }
            return res;
        }

        #endregion
    }
}