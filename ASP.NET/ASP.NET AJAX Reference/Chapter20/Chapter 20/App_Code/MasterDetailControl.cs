using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using System;

namespace CustomComponents
{
  public class MasterDetailControl : BaseMasterDetailControl2
  {
    protected override BaseDataBoundControl CreateBaseDataBoundControlMaster()
    {
      GridView master = new GridView();
      master.AllowPaging = true;
      master.AllowSorting = true;
      master.AutoGenerateColumns = true;
      master.AutoGenerateSelectButton = true;
      master.ID = "MasterGridView";
      return master;
    }
    protected override void RegisterMasterEventHandlers()
    {
      ((GridView)Master).SelectedIndexChanged +=
      new EventHandler(Master_SelectedIndexChanged);
      ((GridView)Master).PageIndexChanged +=
      new EventHandler(Master_ResetSelectedValue);
      ((GridView)Master).Sorted += new EventHandler(Master_ResetSelectedValue);
    }
    public int PageSize
    {
      get
      {
        EnsureChildControls();
        return ((GridView)Master).PageSize;
      }
      set
      {
        EnsureChildControls();
        ((GridView)Master).PageSize = value;
      }
    }
    [TypeConverter(typeof(StringArrayConverter))]
    public string[] DataKeyNames
    {
      get
      {
        EnsureChildControls();
        return ((GridView)Master).DataKeyNames;
      }
      set
      {
        EnsureChildControls();
        ((GridView)Master).DataKeyNames = value;
        ((DetailsView)Detail).DataKeyNames = value;
      }
    }
    protected override void Master_DataBound(object sender, EventArgs e)
    {
      for (int i = 0; i < ((GridView)Master).Rows.Count; i++)
      {
        if (((GridView)Master).DataKeys[i].Value == this.SelectedValue)
        {
          ((GridView)Master).SelectedIndex = i;
          break;
        }
      }
      Master_SelectedIndexChanged(null, null);
    }
    void Master_ResetSelectedValue(object sender, EventArgs e)
    {
      if (((GridView)Master).SelectedIndex != -1)
      {
        ((GridView)Master).SelectedIndex = -1;
        Master_SelectedIndexChanged(null, null);
      }
    }
    protected virtual void Master_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (((GridView)Master).SelectedIndex == -1)
        this.Detail.Visible = false;
      else
        this.Detail.Visible = true;
      this.SelectedValue = ((GridView)Master).SelectedValue;
      UpdateDetail(sender, e);
    }
  }
}