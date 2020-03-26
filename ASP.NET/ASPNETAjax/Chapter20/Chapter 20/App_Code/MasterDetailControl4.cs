namespace CustomComponents
{
  public class MasterDetailControl4 : MasterDetailControl2
  {
    public override object SelectedValue
    {
      get { return this.Page.Session["SelectedValue"]; }
      set { this.Page.Session["SelectedValue"] = value; }
    }
  }
}