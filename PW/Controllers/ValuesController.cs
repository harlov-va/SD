using Newtonsoft.Json;
using PW.BLL;
using PW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;

namespace PW.Controllers
{
    
    public class ValuesController : ApiController
    {
        protected IManager mng;
        public ValuesController(IManager _mng)
        {
            this.mng = _mng;
        }
        //public ValuesController()
        //{
        //    this.mng = new Manager(new Repository(new PWEntities()));
        //}
        protected override void Dispose(bool disposing)
        {
            if (mng != null) mng.Dispose();
        }
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        [JWTAuthenticationFilter]
        public HttpResponseMessage Get()
        {

            //var msg = "";
            //var res = mng.Users.GetUsers(out msg);
            //return Request.CreateResponse(HttpStatusCode.OK, res);
            string msg = "";
            var res = mng.Users.GetUsers(out msg);
            var json = JsonConvert.SerializeObject(
                res.Select(x => new
                {
                    id = x.id,
                    userName = x.userName,
                    password = x.password,
                    email = x.email,
                    dateTime = x.dateCreate.ToShortDateString()

                })
            );
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [Route("sessions/create")]
        public HttpResponseMessage Post()
        {
            var httpContext = (HttpContextBase)Request.Properties["MS_HttpContext"];
            string email = httpContext.Request.Form["email"];
            string password = httpContext.Request.Form["password"];
            Dictionary<string, string> param;
            var user = mng.Users.GetUser(email, password, out param);
            if (user == null)
            {
                if(param["code"] == "400")
                { 
                    return Request.CreateResponse(HttpStatusCode.BadRequest, param["msg"], Configuration.Formatters.JsonFormatter);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, param["msg"], Configuration.Formatters.JsonFormatter);
                }
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
            //return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
