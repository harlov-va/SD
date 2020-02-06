using PW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.BLL
{
    public class UsersManager : IUsersManager
    {
        #region System
        private IRepository _db;
        private bool _disposed;
        public UsersManager(IRepository db)
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
        private bool _canAccessToItem(string tkn)
        {
            var res = false;
            //if (_db.GetToken()) res = true;
            //var isAdmin = user.aspnet_Roles.Any(x => x.RoleName == "admin");
            //var isManager = user.aspnet_Roles.Any(x => x.RoleName == "guest");
            //if (user != null && isAdmin || isManager)
            //{
            //    res = true;
            //}
            return true;
        }
        //private bool _canManageItem(aspnet_Users user)
        //{
        //    //var res = false;
        //    //var isAdmin = user.aspnet_Roles.Any(x => x.RoleName == "admin");
        //    //if (user != null && isAdmin)
        //    //{
        //    //    res = true;
        //    //}
        //    //return res;
        //    return true;
        //}
        #endregion
        #region Users
        public List<pw_users> GetUsers(out string msg)
        {
            msg = "";
            List<pw_users> res;
            try
            {
                res = _db.GetUsers().ToList();
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении одного документа по id");
                res = null;
            }
            return res;
        }
        public pw_users GetUser(string email, string password, out Dictionary<string, string> parameters)
        {
            parameters = new Dictionary<string, string>();
            pw_users res;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                res = null;
                parameters.Add("code", "400");
                parameters.Add("msg", "You must send email and password");
                return res;
            }
            try
            {
                res = _db.GetUser(email);
                if (res == null || res.password != password)
                {
                    parameters.Add("code", "401");
                    parameters.Add("msg", "Invalid email or password");
                    res = null;
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении одного документа по id");
                res = null;
            }
            return res;
        }
        public pw_users GetUser(int id, out string msg)
        {
            msg = "";
            pw_users res;
            try
            {
                res = _db.GetUser(id);
                if (res == null)
                {
                    msg = "user not found";
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении одного документа по id");
                res = null;
            }
            return res;
        }
        public List<pw_users> FilteredUserList(string filter, out string msg)
        {
            msg = "";
            IEnumerable<pw_users> users;
            IEnumerable<pw_users> res;
            try
            {
                    users = _db.GetUsers().ToList();
                    res = users.OfType<pw_users>().Where(s => s.userName.ToLower().Contains(filter));
                if(res == null || !res.Any())
                {
                    msg = "No search string";                    
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка документов");
                res = null;
            }
            return res.ToList();
        }
        public pw_users CreateUser(string name, string login, string password, out string msg)
        {
            msg = "";
            pw_users res ;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                res = null;
                msg = "You must send username and password";
                return res;
            }
            try
            {
                if ((_db.GetUser(login))==null)
                { 
                    res = new pw_users() {
                        userName = name,
                        password = password,
                        email = login,
                        balance = 500,
                        dateCreate = DateTime.Now
                    };
                    _db.SaveUser(res);
                }
                else
                {
                    res = null;
                    msg = "A user with that email already exists";
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании нового документа");
                res = null;
            }
            return res;
        }
        public string Login(string login, string password, out string msg)
        {
            msg = "";
            return "";
        }
        public pw_users LoggedUserInfo(string token, out string msg)
        {
            msg = "";
            return new pw_users();
        }
        
        //public pw_users EditUser(Dictionary<string, string> parameters, int id, aspnet_Users user, out string msg)
        //{
        //    msg = "";
        //    pw_users res;
        //    try
        //    {
        //        if (!_canManageItem(user))
        //        {
        //            msg = "Нет прав на редактирование элемента";
        //            res = null;
        //        }
        //        else
        //        {
        //            res = _db.GetUser(id);
        //            foreach (var key in parameters.Keys)
        //            {
        //                switch (key)
        //                {

        //                }
        //            }
        //            _db.SaveUser(res);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _debug(e, new { }, "Ошибка возникла при изменении элемента");
        //        res = null;
        //    }
        //    return res;
        //}

        //public bool RemoveUser(int id, aspnet_Users user, out string msg)
        //{
        //    msg = "";
        //    bool res;
        //    try
        //    {
        //        if (!_canManageItem(user))
        //        {
        //            msg = "Нет прав на удаление элемента";
        //            res = false;
        //        }
        //        else
        //        {
        //            //var item = _db.GetUser(id);
        //            //item.isDeleted = true;
        //            _db.DeleteUser(id);
        //            res = true;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _debug(e, new { }, "Ошибка возникла при удалении элемента");
        //        res = false;
        //    }
        //    return res;
        //}
        #endregion

        //"Нет прав на получение элемента по id"
        //"Ошибка возникла при получении элемента по id"
        //msg = "Нет прав создавать элемента";
        //msg = "Нет прав редактировать элемента";
        //"Ошибка возникла при изменении элемента"
        //msg = "Нет прав на удаление элемента";
        //"Ошибка возникла при удалении элемента"
    }
}