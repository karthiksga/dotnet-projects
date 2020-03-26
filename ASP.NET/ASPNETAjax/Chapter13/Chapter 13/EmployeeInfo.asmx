<%@ WebService Language="C#" Class="EmployeeInfo" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://www.employees/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class EmployeeInfo : System.Web.Services.WebService
{
  [WebMethod]
  public string GetEmployeeInfo(string username, string password)
  {
    if (password == "password" && username == "username")
      return "Shahram|Khosravi|22223333|Some Department|";
    return "Validation failed";
  }
}