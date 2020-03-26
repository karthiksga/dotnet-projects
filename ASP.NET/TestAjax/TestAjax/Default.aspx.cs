using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AjaxPro.Services;

namespace TestAjax
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(_Default));
        }

        [AjaxPro.AjaxMethod]
        public string GetServerTime(string id)
        {
            //return DateTime.Now.ToString();
            return id;
        }

        [AjaxPro.AjaxMethod]
        public string sendJSON(string json)
        {
            try
            {                
                //Customer objCustomer = (Customer)AjaxPro.JavaScriptDeserializer.DeserializeFromJson("{\"FirstName\":\"karthik\",\"LastName\":\"sg\"}", new Customer().GetType());
                //Customer objCustomer = (Customer)AjaxPro.JavaScriptDeserializer.DeserializeFromJson(json, new Customer().GetType());
                //return objCustomer.LastName;                
                List<Customer> objCustomers = (List<Customer>)AjaxPro.JavaScriptDeserializer.DeserializeFromJson(json, typeof(List<Customer>));
                return objCustomers[0].FirstName;
            }
            catch (Exception ex)
            {
                
                throw;
            }            
        }

    }
}
