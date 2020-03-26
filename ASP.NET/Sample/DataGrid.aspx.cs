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

public partial class DataGrid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
        }
    }

    private void BindGrid()
    {
        try
        {
            DataTable objDataTable;
            if (Session["CountryDataGrid"] != null)
            {
                objDataTable = (DataTable)Session["CountryDataGrid"];
            }
            else
            {
                objDataTable = new DataAccess().getData();
                Session["CountryDataGrid"] = objDataTable;
            }

            CountryDataGrid.DataKeyField = "id";
            CountryDataGrid.DataSource = objDataTable;
            CountryDataGrid.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
        }
    }
    protected void CountryDataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            SaveSelection();
            BindGrid();
            CountryDataGrid.CurrentPageIndex = e.NewPageIndex;
            CountryDataGrid.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    public void SaveSelection()
    {
        try
        {
            DataGridItemCollection items = CountryDataGrid.Items;
            for (int i = 0; i < items.Count; i++)
            {
                CheckBox chkBox = (CheckBox)items[i].FindControl("chkBox");
                ArrayList selectedItems = new ArrayList();
                if(chkBox.Checked)
                    selectedItems.Add(CountryDataGrid.DataKeys[i].ToString());
                //Response.Write(CountryDataGrid.DataKeys[i].ToString() + ":"+ chkBox.Checked);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
}
