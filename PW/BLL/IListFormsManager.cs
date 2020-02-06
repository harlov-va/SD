using formDesigner.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formDesigner.BLL
{
    public interface IListFormsManager:IDisposable
    {
        #region fd_listForms
        List<fd_listForms> GetListForms(aspnet_Users user,out string msg);
        fd_listForms GetListForm(int id, aspnet_Users user, out string msg);
        fd_listForms CreateListForm(Dictionary<string, string> parameters, aspnet_Users user,out string msg);
        fd_listForms EditListForm(Dictionary<string, string> parameters, int id, aspnet_Users user, out string msg);
        bool RemoveListForm(int id, aspnet_Users user, out string msg);
        #endregion
       
    }
}
