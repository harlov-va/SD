using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.DAL
{
    public interface IRepository:IDisposable
    {
        #region System
        void Save();
        #endregion
        #region pw_users
        IQueryable<pw_users> GetUsers();
        pw_users GetUser(int ID);
        pw_users GetUser(string email);
        int SaveUser(pw_users element, bool withSave = true);
        bool DeleteUser(int ID);
        #endregion
        #region pw_tokens
        IQueryable<pw_tokens> GetTokens();
        pw_tokens GetToken(int ID);
        int SaveToken(pw_tokens element, bool withSave = true);
        bool DeleteToken(int ID);
        #endregion
        //#region pw_senderTransactions
        //IQueryable<pw_senderTransactions> GetSentTransactions();
        //pw_senderTransactions GetSentTransaction(int ID);
        //int SaveSentTransaction(pw_senderTransactions element, bool withSave = true);
        //bool DeleteSentTransaction(int ID);
        //#endregion
        //#region pw_recipientTransactions
        //IQueryable<pw_recipientTransactions> GetReceivedTransactions();
        //pw_recipientTransactions GetReceivedTransaction(int ID);
        //int SaveReceivedTransaction(pw_recipientTransactions element, bool withSave = true);
        //bool DeleteReceivedTransaction(int ID);
        //#endregion
        #region pw_transactions
        IQueryable<pw_transactions> GetTransactions();
        pw_transactions GetTransaction(int ID);
        int SaveTransaction(pw_transactions element, bool withSave = true);
        bool DeleteTransaction(int ID);
        #endregion
        //IQueryable<pw_senderRecipient> GetRelationshipsSR();
        //pw_senderRecipient GetRelationshipSR(int ID);
        //int SaveRelationshipSR(pw_senderRecipient element, bool withSave = true);
        //bool DeleteRelationshipSR(int ID);

    }
}
