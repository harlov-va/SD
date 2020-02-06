using PW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.BLL
{
    public interface IUsersManager:IDisposable
    {
        #region pw_users
        List<pw_users> GetUsers(out string msg);
        pw_users GetUser(string login, string password, out Dictionary<string,string> parameters);
        pw_users GetUser(int id, out string msg);
        pw_users CreateUser(string name, string login,string password, out string msg);
        string Login(string login, string password, out string msg);
        pw_users LoggedUserInfo(string token, out string msg);
        List<pw_users> FilteredUserList(string filter, out string msg);
        #endregion
       
    }
}
