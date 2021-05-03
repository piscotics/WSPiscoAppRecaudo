using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
namespace WebApiPiscoTicsMobile
{
    public class RequireHttpsAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<p>User Https instead of Http");

                UriBuilder uribuilder = new UriBuilder(actionContext.Request.RequestUri);
                uribuilder.Scheme = Uri.UriSchemeHttps;
                uribuilder.Port = 9035;
                actionContext.Response.Headers.Location = uribuilder.Uri;
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
            
        }
    }
}