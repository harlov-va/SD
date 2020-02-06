using Newtonsoft.Json;
using PW.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace PW.Controllers
{
    [JWTAuthenticationFilter]
    public class protectedController : ApiController
    {
        protected IManager mng;
        public protectedController(IManager _mng)
        {
            this.mng = _mng;
        }
        // GET: api/protected
        [Route("api/protected/user-info")]
        public HttpResponseMessage Get()
        {
            var auth = new JWTAuthenticationFilter();
            var identity = auth.GetUserIdentity(this.ActionContext);
            string msg = "";
            var res = mng.Users.GetUser(identity.UserId, out msg);
            if (res == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, msg, Configuration.Formatters.JsonFormatter);
            }
            else
            {

            
            var json = JsonConvert.SerializeObject(
                new
                {
                    id = res.id,
                    userName = res.userName,
                    email = res.email,
                    balance = res.balance
                }
            );
            return Request.CreateResponse(HttpStatusCode.OK, json);
            }
        }

        // GET: api/protected/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/protected
        [Route("api/protected/users/list")]
        public HttpResponseMessage Post()
        {
            var httpContext = (HttpContextBase)Request.Properties["MS_HttpContext"];
            string filter = httpContext.Request.Form["filter"];
            var msg = "";
            var users = mng.Users.FilteredUserList(filter, out msg);
            if (users.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, msg, Configuration.Formatters.JsonFormatter);
            }
            else
            {
                var res = users.Select(x => new
                {
                    id = x.id,
                    name = x.userName
                });
                var json = JsonConvert.SerializeObject(res);
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
        }

        // PUT: api/protected/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/protected/5
        public void Delete(int id)
        {
        }
    }
}
