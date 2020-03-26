using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Reflection;
using System.Web.Compilation;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Collections;
using System.Web.Services.Protocols;
using System.IO;
namespace CustomComponents
{
  public class ScriptModule : IHttpModule
  {
    protected virtual void Dispose() { }
    protected virtual void Init(HttpApplication context)
    {
      context.PostAcquireRequestState +=
                  new EventHandler(this.OnPostAcquireRequestState);
    }
    private void OnPostAcquireRequestState(object sender, EventArgs eventArgs)
    {
      HttpApplication application = (HttpApplication)sender;
      HttpRequest request = application.Context.Request;
      if ((application.Context.Handler is Page) &&
      RestHandlerFactory.IsRestMethodCall(request))
      {
        IHttpHandler restHandler = RestHandler.CreateHandler(application.Context);
        restHandler.ProcessRequest(application.Context);
        application.CompleteRequest();
      }
    }
    void IHttpModule.Dispose()
    {
      this.Dispose();
    }
    void IHttpModule.Init(HttpApplication context)
    {
      this.Init(context);
    }
  }
}