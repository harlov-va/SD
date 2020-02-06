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
    [JWTAuthenticationFilter]
    public class transactionsController : ApiController
    {
        protected IManager mng;
        public transactionsController(IManager _mng)
        {
            this.mng = _mng;
        }
        [Route("api/protected/transactions")]
        public HttpResponseMessage Get()
        {
            var auth = new JWTAuthenticationFilter();
            var identity = auth.GetUserIdentity(this.ActionContext);
            string msg = "";
            var res = mng.Transactions.GetTransactions(identity.UserId, out msg);
            if (res == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, msg, Configuration.Formatters.JsonFormatter);
            }
            else
            {            
                var json = JsonConvert.SerializeObject(new
                {
                    trans_token = res.Select(x => new
                    {
                        id = x.id,
                        date = x.date.ToShortDateString(),
                        username = x.userName,
                        amount = x.amount,
                        balance = x.balance
                    })
                });
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
        }

        // GET: api/transactions/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/protected/transactions")]
        public HttpResponseMessage Post()
        {
            var httpContext = (HttpContextBase)Request.Properties["MS_HttpContext"];
            string name = httpContext.Request.Form["name"];
            string amount = httpContext.Request.Form["amount"];
            var auth = new JWTAuthenticationFilter();
            var identity = auth.GetUserIdentity(this.ActionContext);
            var msg = "";
            Dictionary<string, string> param;
            var res = mng.Transactions.CreateTransaction(identity.UserId, name, amount, out param);
            if (res == null)
            {
                switch (param["code"])
                {
                    case "400": return Request.CreateResponse(HttpStatusCode.BadRequest, param["msg"], Configuration.Formatters.JsonFormatter);
                        break;
                    default: return Request.CreateResponse(HttpStatusCode.Unauthorized, param["msg"], Configuration.Formatters.JsonFormatter);
                        break;
                }
                
            }
            else
            {
                var json = JsonConvert.SerializeObject(new
                {
                    trans_token = new
                    {
                        id = res.id,
                        date = res.date.ToShortDateString(),
                        username = res.userName,
                        amount = res.amount,
                        balance = res.balance
                    }   
                });
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
        }

        // PUT: api/transactions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/transactions/5
        public void Delete(int id)
        {
        }
    }
}
