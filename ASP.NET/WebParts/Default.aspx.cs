using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            WebPartManager1.DisplayMode = WebPartManager.DesignDisplayMode;
            foreach (WebPart part in WebPartManager1.WebParts)
            {
                if (part.Controls.Contains(txtTest))
                {
                    if (!part.IsClosed)
                        WebPartManager1.CloseWebPart(part);
                }
            }
        }
    }

    protected void btnCatalog_Click(object sender, EventArgs e)
    {
        WebPartManager1.DisplayMode = WebPartManager.CatalogDisplayMode;
    }

    protected void WebPartManager_Authorize(object sender, WebPartAuthorizationEventArgs e)
    {
        e.IsAuthorized = false;
    }
}
