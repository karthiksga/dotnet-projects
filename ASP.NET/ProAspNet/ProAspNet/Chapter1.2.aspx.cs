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
	public partial class Chapter12_Default : System.Web.UI.Page
	{
		private void Page_Load(object sender, EventArgs e)
		{
            lblInfo.Text += "Page.Load Event handled.<br/>";
            if (Page.IsPostBack)
                lblInfo.Text += "<b>This is the second time you've seen this page </b><br/>";
		}

        private void Page_Init(object sender, EventArgs e)
        {
            lblInfo.Text += "Page.Init Event handled.<br/>";
        }

        private void Page_PreRender(object sender, EventArgs e)
        {
            lblInfo.Text += "Page.PreRender Event handled.<br/>";
        }

        private void Page_Unload(object sender, EventArgs e)
        {
            lblInfo.Text += "Page.UnLoad Event handled.<br/>";
        }
	}
}
