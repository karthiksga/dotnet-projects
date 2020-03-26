using System;
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
namespace CustomComponents
{
  public class MasterDetailControl2 : BaseMasterDetailControl2
  {
    protected override BaseDataBoundControl CreateBaseDataBoundControlMaster()
    {
      DropDownList master = new DropDownList();
      master.AutoPostBack = true;
      master.ID = "DropDownList";
      return master;
    }
    protected override void RegisterMasterEventHandlers()
    {
      ((ListControl)Master).SelectedIndexChanged +=
      new EventHandler(Master_SelectedIndexChanged);
    }
    protected override void Master_DataBound(object sender, EventArgs e)
    {
      ListItem selectedItem =
      ((ListControl)Master).Items.FindByValue((string)SelectedValue);
      int selectedIndex = ((ListControl)Master).Items.IndexOf(selectedItem);
      ((ListControl)Master).SelectedIndex = selectedIndex;
      Master_SelectedIndexChanged(null, null);
    }
    protected virtual void Master_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (((ListControl)Master).SelectedIndex == -1)
        this.Detail.Visible = false;
      else
        this.Detail.Visible = true;
      this.SelectedValue = ((ListControl)Master).SelectedValue;
      this.UpdateDetail(sender, e);
    }
    public string DataTextField
    {
      get
      {
        return ((ListControl)Master).DataTextField;
      }
      set
      {
        ((ListControl)Master).DataTextField = value;
      }
    }
    public string DataValueField
    {
      get
      {
        return ((ListControl)Master).DataValueField;
      }
      set
      {
        ((ListControl)Master).DataValueField = value;
      }
    }
    [TypeConverter(typeof(StringArrayConverter))]
    public string[] DataKeyNames
    {
      get
      {
        return ((DetailsView)Detail).DataKeyNames;
      }
      set
      {
        ((DetailsView)Detail).DataKeyNames = value;
      }
    }
  }
}