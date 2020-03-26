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
  internal class RestHandler : IHttpHandler
  {
    private MethodInfo _methodInfo;
    internal static IHttpHandler CreateHandler(HttpContext context)
    {
      string servicePath = context.Request.FilePath;
      Type serviceType = BuildManager.GetCompiledType(servicePath);
      if (serviceType == null)
      {
        object obj = BuildManager.CreateInstanceFromVirtualPath(servicePath,
        typeof(Page));
        serviceType = obj.GetType();
      }
      string methodName = context.Request.PathInfo.Substring(1);
      MethodInfo[] infoArray = serviceType.GetMethods();
      MethodInfo minfo = null;
      foreach (MethodInfo info in infoArray)
      {
        object[] objArray = info.GetCustomAttributes(typeof(WebMethodAttribute),
        true);
        if (objArray.Length != 0 && info.Name == methodName)
        {
          minfo = info;
          break;
        }
      }
      RestHandler handler = new RestHandler();
      handler._methodInfo = minfo;
      return handler;
    }
    public void ProcessRequest(HttpContext context)
    {
      string text = new StreamReader(context.Request.InputStream).ReadToEnd();
      IDictionary<string, object> rawParams;
      JavaScriptSerializer serializer = new JavaScriptSerializer();
      if (string.IsNullOrEmpty(text))
        rawParams = new Dictionary<string, object>();
      else
        rawParams = serializer.Deserialize<IDictionary<string, object>>(text);
      ArrayList parameters = new ArrayList();
      ParameterInfo[] infos = _methodInfo.GetParameters();
      TypeConverter converter;
      foreach (KeyValuePair<string, object> entry in rawParams)
      {
        IDictionary<string, object> dictionary =
        entry.Value as IDictionary<string, object>;
        if (dictionary != null)
          parameters.Add(dictionary);
        else
        {
          for (int i = 0; i < infos.Length; i++)
          {
            if (entry.Key == infos[i].Name)
            {
              converter = TypeDescriptor.GetConverter(infos[i].ParameterType);
              if (converter.CanConvertFrom(entry.Value.GetType()))
                parameters.Add(converter.ConvertFrom(entry.Value));
            }
          }
        }
      }
      object[] methodParameters = new object[parameters.Count];
      parameters.CopyTo(methodParameters);
      object target = Activator.CreateInstance(_methodInfo.DeclaringType);
      object obj3 = _methodInfo.Invoke(target, methodParameters);
      string s = serializer.Serialize(obj3);
      context.Response.ContentType = "application/json";
      if (s != null)
        context.Response.Write(s);
    }
    public bool IsReusable
    {
      get { return false; }
    }
  }
}