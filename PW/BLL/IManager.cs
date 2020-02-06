using PW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.BLL
{
    public interface IManager:IDisposable
    {
        IUsersManager Users { get; set; }
        ITransactionsManager Transactions { get; set; }
    }
}
