<%@ WebService Language="C#" Class="Math" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Math  : System.Web.Services.WebService {

    [WebMethod]
    public double Divide(double x, double y)
    {
        if (y == 0)
            throw new DivideByZeroException();
        return x / y;
    }    
}

