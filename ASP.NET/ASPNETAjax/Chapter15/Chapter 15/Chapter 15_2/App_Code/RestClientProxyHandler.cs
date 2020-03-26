using System;
using System.Web;
namespace CustomComponents
{
  internal class RestClientProxyHandler : IHttpHandler
  {
    public void ProcessRequest(HttpContext context)
    {
      ClientProxyGenerator generator = new ClientProxyGenerator();
      string script = generator.GetClientProxyScript(context.Request.FilePath,
      false);
      context.Response.ContentType = "application/x-javascript";
      context.Response.Write(script);
    }
    public bool IsReusable
    {
      get { return false; }
    }
  }
}
