using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PW.BLL
{
    public class JWTAuthenticationFilter : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext filterContext)
        {

            if (!IsUserAuthorized(filterContext))
            {
                ShowAuthenticationError(filterContext);
                return;
            }
            base.OnAuthorization(filterContext);
        }
        public bool IsUserAuthorized(HttpActionContext actionContext)
        {
            var authHeader = FetchFromHeader(actionContext); //fetch authorization token from header


            if (authHeader != null)
            {
                var auth = new AuthenticationModule();
                JwtSecurityToken userPayloadToken = auth.GenerateUserClaimFromJWT(authHeader);

                if (userPayloadToken != null)
                {

                    var identity = auth.PopulateUserIdentity(userPayloadToken);
                    string[] roles = { "All" };
                    var genericPrincipal = new GenericPrincipal(identity, roles);
                    Thread.CurrentPrincipal = genericPrincipal;
                    var authenticationIdentity = Thread.CurrentPrincipal.Identity as JWTAuthenticationIdentity;
                    if (authenticationIdentity != null && !String.IsNullOrEmpty(authenticationIdentity.UserName))
                    {
                        authenticationIdentity.UserId = identity.UserId;
                        authenticationIdentity.UserName = identity.UserName;
                    }
                    return true;
                }

            }
            return false;


        }
        private string FetchFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null)
            {
                requestToken = authRequest.Parameter;
            }

            return requestToken;
        }
        private static void ShowAuthenticationError(HttpActionContext filterContext)
        {
            //var responseDTO = new ResponseDTO() { Code = 401, Message = "Unable to access, Please login again" };
            var response = "UnauthorizedError";
            filterContext.Response =
            filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
        }
        public class ResponseDTO
        {
            public int Code { get; set; }
            public string Message { get; set; }
        }
        public JWTAuthenticationIdentity GetUserIdentity(HttpActionContext filterContext)
        {
            var authHeader = FetchFromHeader(filterContext); //fetch authorization token from header


            if (authHeader != null)
            {
                var auth = new AuthenticationModule();
                JwtSecurityToken userPayloadToken = auth.GenerateUserClaimFromJWT(authHeader);

                if (userPayloadToken != null)
                {

                    var identity = auth.PopulateUserIdentity(userPayloadToken);
                    return identity;
                }

            }
            return null;
        }
    }
}