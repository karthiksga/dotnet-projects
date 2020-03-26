using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
namespace CustomComponents
{
  public class MasterDetailControl3 : MasterDetailControl2
  {
    protected override BaseDataBoundControl CreateBaseDataBoundControlMaster()
    {
      ListBox master = new ListBox();
      master.AutoPostBack = true;
      master.ID = "ListBox";
      return master;
    }
  }
}