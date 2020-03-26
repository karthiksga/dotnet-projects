<%@ WebService Language="C#" Class="MyNamespace.Math" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
namespace MyNamespace
{
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [ScriptService]
  public class Math
  {
    [WebMethod]
    public double Add(double x, double y)
    {
      return x + y;
    }
  }
}