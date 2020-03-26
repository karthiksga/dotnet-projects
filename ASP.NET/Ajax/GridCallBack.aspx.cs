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

public partial class GridCallBack : System.Web.UI.Page,ICallbackEventHandler
{
    public string _eventArgument;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                string callbackRef = Page.ClientScript.GetCallbackEventReference(
                this, "document.getElementById('txtFirstName').value+','+document.getElementById('txtLastName').value", "ClientCallback", "null", true);

                string callbackscript = "function call(){" + callbackRef + ";}";
                //btnSave.Attributes["onclick"] = callbackRef;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClientCall", callbackscript, true);
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void BindGrid()
    {
        try
        {
            CustomerGrid.DataSource = new DataAccess().GetCustomerDetails();
            CustomerGrid.DataBind();
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }

    protected void CustomerGrid_Refreshing(object sender, EventArgs e)
    {
        BindGrid();
    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        new DataAccess().CreateCustomer(txtFirstName.Text.Trim(), txtLastName.Text.Trim());
    //        BindGrid();
    //    }
    //    catch (Exception ex)
    //    {
            
    //        throw;
    //    }
    //}

    #region ICallbackEventHandler Members

    public string GetCallbackResult()
    {
        string[] arguments = _eventArgument.Split(',');
        string _firstName = "";
        string _lastName = "";
        _firstName = arguments[0];
        _lastName = arguments[1];
        return new DataAccess().CreateCustomer(_firstName.Trim(), _lastName.Trim()).ToString();
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        this._eventArgument = eventArgument;
    }

    #endregion
}
