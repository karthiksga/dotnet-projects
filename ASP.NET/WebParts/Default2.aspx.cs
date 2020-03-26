using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender,EventArgs e)
    {
        ////if (!Page.IsPostBack)
        ////{
        ////string sUserName = Request.QueryString["id"];
        //string sUserName = txtUserName.Text;
        //HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        //if (authCookie == null)
        //{
        //    //string myCookieName = "User1";
        //    //HttpCookie myCookie = Request.Cookies[myCookieName];
        //    ////string UserId = "";
        //    //if(myCookie==null)
        //    //{
        //    //    myCookie = new HttpCookie(myCookieName);
        //    //}
        //    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, sUserName, DateTime.Now, DateTime.Now.AddDays(2), false, "roles");
        //    string sEncryptedTicket = FormsAuthentication.Encrypt(authTicket);
        //    authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, sEncryptedTicket);
        //    Response.Cookies.Add(authCookie);
        //}
        ////}
        string sUserName = txtUserName.Text;
        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, sUserName, DateTime.Now, DateTime.Now.AddDays(2), false, "roles");
        string sEncryptedTicket = FormsAuthentication.Encrypt(authTicket);
        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, sEncryptedTicket);
        Response.Cookies.Add(authCookie);
        Response.Redirect("Default.aspx?id="+txtUserName.Text,true);        
    }
}
