using System;
using System.Collections;
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

namespace ProAspNet
{
    public partial class Chapter1_DynamicControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button objButton = new Button();
            objButton.ID = "btnNew";
            objButton.Text = "New Button";
            panel1.Controls.Add(objButton);

            
        }
    }
}
