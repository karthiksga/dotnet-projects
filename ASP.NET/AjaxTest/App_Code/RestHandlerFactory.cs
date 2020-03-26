using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace CustomComponents
{

    /// <summary>
    /// Summary description for RestHandlerFactory
    /// </summary>
    public class RestHandlerFactory : IHttpHandlerFactory
    {
        public RestHandlerFactory()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public virtual IHttpHandler GetHandler(HttpContext context, string requestType,string url, string pathTranslated)
        {
            //if (IsClientProxyRequest(context.Request.PathInfo))
            //  return new RestClientProxyHandler();        
            return RestHandler.CreateHandler(context);
        }
        internal static bool IsRestRequest(HttpContext context)
        {
            if (!IsRestMethodCall(context.Request))
                return IsClientProxyRequest(context.Request.PathInfo);
            return true;
        }
        internal static bool IsRestMethodCall(HttpRequest request)
        {
            if (string.IsNullOrEmpty(request.PathInfo))
                return false;
            if (!request.ContentType.StartsWith("application/json;",
            StringComparison.OrdinalIgnoreCase))
                return string.Equals(request.ContentType, "application/json",
                StringComparison.OrdinalIgnoreCase);
            return true;
        }
        internal static bool IsClientProxyRequest(string pathInfo)
        {
            return string.Equals(pathInfo, "/js", StringComparison.OrdinalIgnoreCase);
        }
        public virtual void ReleaseHandler(IHttpHandler handler) { }
    }
}
