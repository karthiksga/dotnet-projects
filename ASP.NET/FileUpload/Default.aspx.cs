using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void UploadComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        AsyncFileUpload1.SaveAs(AsyncFileUpload1.FileName);
        lblComplete.Text = "File uploaded Successfully";
    }
}
