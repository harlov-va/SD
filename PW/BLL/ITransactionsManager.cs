using PW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.BLL
{
    public interface ITransactionsManager : IDisposable
    {
        #region pw_transactions
        List<pw_transactions> GetTransactions(int id,out string msg);
        pw_transactions CreateTransaction(int id, string userName, string amount, out Dictionary<string, string> parameters);
        #endregion
    }
}
