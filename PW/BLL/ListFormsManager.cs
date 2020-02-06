using formDesigner.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace formDesigner.BLL
{
    public class ListFormsManager : IListFormsManager
    {
        #region System
        private IRepository _db;
        private bool _disposed;
        public ListFormsManager(IRepository db)
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
        private bool _canAccessToItem(aspnet_Users user)
        {
            //var res = false;
            //var isAdmin = user.aspnet_Roles.Any(x => x.RoleName == "admin");
            //var isManager = user.aspnet_Roles.Any(x => x.RoleName == "guest");
            //if (user != null && isAdmin || isManager)
            //{
            //    res = true;
            //}
            return true;
        }
        private bool _canManageItem(aspnet_Users user)
        {
            //var res = false;
            //var isAdmin = user.aspnet_Roles.Any(x => x.RoleName == "admin");
            //if (user != null && isAdmin)
            //{
            //    res = true;
            //}
            //return res;
            return true;
        }
        #endregion
        #region Forms
        public List<fd_listForms> GetListForms(aspnet_Users user, out string msg)
        {
            msg = "";
            List<fd_listForms> res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение списка элементов!";
                    res = null;
                }
                else
                {
                    res = _db.GetListForms().ToList();
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка документов");
                res = null;
            }
            return res;
        }
        public fd_listForms GetListForm(int id, aspnet_Users user, out string msg)
        {
            msg = "";
            fd_listForms res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение документа";
                    res = null;
                }
                else
                {
                    res = _db.GetListForm(id);
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении одного документа по id");
                res = null;
            }
            return res;
        }
        public fd_listForms CreateListForm(Dictionary<string, string> parameters, aspnet_Users user, out string msg)
        {
            msg = "";
            fd_listForms res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на создание документа";
                    res = null;
                }
                else
                {
                    res = new fd_listForms();
                    res.dateCreate = DateTime.Today;
                    //res.adminLogin = user.UserName;
                    foreach (var key in parameters.Keys)
                    {
                        switch (key)
                        {
                            case "nameForm":
                                res.nameForm = parameters[key];
                                break;
                            case "adminLogin":
                                res.adminLogin = parameters[key];
                                break;
                            case "loginUser":
                                res.loginUser = parameters[key];
                                break;
                            case "groupUsers":
                                res.groupUsers = parameters[key];
                                break;
                            case "approvedByAdmin":
                                res.approvedByAdmin = bool.Parse(parameters[key]);
                                break;
                        }
                    }
                    _db.SaveListForm(res);
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании нового документа");
                res = null;
            }
            return res;
        }
        public fd_listForms EditListForm(Dictionary<string, string> parameters, int id, aspnet_Users user, out string msg)
        {
            msg = "";
            fd_listForms res;
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав на редактирование элемента";
                    res = null;
                }
                else
                {
                    res = _db.GetListForm(id);
                    foreach (var key in parameters.Keys)
                    {
                        switch (key)
                        {
                            case "nameForm":
                                res.nameForm = parameters[key];
                                break;
                            case "dateCreate":
                                res.dateCreate = RDL.Convert.StrToDateTime(parameters[key],DateTime.Today);
                                break;
                            case "adminLogin":
                                res.adminLogin = parameters[key];
                                break;
                            case "loginUser":
                                res.loginUser = parameters[key];
                                break;
                            case "groupUsers":
                                res.groupUsers = parameters[key];
                                break;
                            case "approvedByAdmin":
                                res.approvedByAdmin = bool.Parse(parameters[key]);
                                break;
                        }
                    }
                    _db.SaveListForm(res);
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = null;
            }
            return res;
        }
        
        public bool RemoveListForm(int id, aspnet_Users user, out string msg)
        {
            msg = "";
            bool res;
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав на удаление элемента";
                    res = false;
                }
                else
                {
                    //var item = _db.GetForm(id);
                    //item.isDeleted = true;
                    _db.DeleteListForm(id);
                    res = true;
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при удалении элемента");
                res = false;
            }
            return res;
        }
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