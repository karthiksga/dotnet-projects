using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
namespace Ext
{
    public class SimpleHandler : IHttpHandler
    {

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.Write("<html><body><h1>Rendered by the SimpleHandler");
            response.Write("</h1></body></html>");
        }

        #endregion
    }
}
