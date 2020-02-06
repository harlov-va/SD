using Newtonsoft.Json;
using PW.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PW.Controllers
{
    public class usersController : ApiController
    {
        protected IManager mng;
        public usersController(IManager _mng)
        {
            this.mng = _mng;
        }
        // GET: api/users
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/users/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("users")]
        public HttpResponseMessage Post()
        {
            var httpContext = (HttpContextBase)Request.Properties["MS_HttpContext"];
            string username = httpContext.Request.Form["username"];
            string password = httpContext.Request.Form["password"];
            string email = httpContext.Request.Form["email"];
            var msg = "";
            var user = mng.Users.CreateUser(username, email, password, out msg);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, msg, Configuration.Formatters.JsonFormatter);
            }
            else
            {
                AuthenticationModule authentication = new AuthenticationModule();
                string token = authentication.GenerateTokenForUser(user.userName, user.id);
                var json = JsonConvert.SerializeObject(
                new
                {
                    id_token = token

                }
                );
                return Request.CreateResponse(HttpStatusCode.OK, json, Configuration.Formatters.JsonFormatter);
            }
        }

        // PUT: api/users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/users/5
        public void Delete(int id)
        {
        }
    }
}
