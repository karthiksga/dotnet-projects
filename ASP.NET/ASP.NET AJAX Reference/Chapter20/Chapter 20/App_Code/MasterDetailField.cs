namespace CustomComponents
{
  using System;
  using System.Web;
  using System.Web.UI;
  using System.Web.UI.WebControls;
  using System.ComponentModel;
  using System.Collections.Specialized;
  using System.Collections;
  using System.Data;
  public class MasterDetailField : BoundField
  {
    public override string DataField
    {
      get
      {
        return base.DataField;
      }
      set
      {
        throw new global::System.NotImplementedException();
      }
    }
    public virtual string DataTextField
    {
      get
      {
        return base.DataField;
      }
      set
      {
        base.DataField = value;
      }
    }
    public virtual string MasterSkinID
    {
      get
      {
        return (ViewState["MasterSkinID"] != null) ?
        (string)ViewState["MasterSkinID"] : String.Empty;
      }
      set
      {
        ViewState["MasterSkinID"] = value;
      }
    }
    public virtual string DetailSkinID
    {
      get
      {
        return (ViewState["DetailSkinID"] != null) ?
        (string)ViewState["DetailSkinID"] : String.Empty;
      }
      set
      {
        ViewState["DetailSkinID"] = value;
      }
    }
    [TypeConverter(typeof(StringArrayConverter))]
    public virtual string[] DataKeyNames
    {
      get
      {
        return (ViewState["DataKeyNames"] != null) ?
        (string[])ViewState["DataKeyNames"] : null;
      }
      set
      {
        ViewState["DataKeyNames"] = value;
      }
    }
    public virtual bool EnableTheming
    {
      get
      {
        return (ViewState["EnableTheming"] != null) ?
        (bool)ViewState["EnableTheming"] : true;
      }
      set
      {
        ViewState["EnableTheming"] = value;
      }
    }
    public virtual string DataValueField
    {
      get
      {
        return (ViewState["DataValueField"] != null) ?
        (string)ViewState["DataValueField"] : String.Empty;
      }
      set
      {
        ViewState["DataValueField"] = value;
      }
    }
    public virtual string MasterDataSourceID
    {
      get
      {
        return (ViewState["MasterDataSourceID"] != null) ?
        (string)ViewState["MasterDataSourceID"] : String.Empty;
      }
      set
      {
        ViewState["MasterDataSourceID"] = value;
      }
    }
    public virtual string DetailDataSourceID
    {
      get
      {
        return (ViewState["DetailDataSourceID"] != null) ?
        (string)ViewState["DetailDataSourceID"] : String.Empty;
      }
      set
      {
        ViewState["DetailDataSourceID"] = value;
      }
    }
    protected override void OnDataBindField(Object sender, EventArgs e)
    {
      DropDownList ddl = sender as DropDownList;
      if (ddl == null)
      {
        base.OnDataBindField(sender, e);
        return;
      }
      Control parent = ddl.Parent;
      DataControlFieldCell cell = null;
      while (parent != null)
      {
        cell = parent as DataControlFieldCell;
        if (cell != null)
          break;
        parent = parent.Parent;
      }
      IDataItemContainer container = (IDataItemContainer)cell.Parent;
      object dataItem = container.DataItem;
      if (dataItem == null || String.IsNullOrEmpty(DataValueField))
        return;
      object dataValueField = DataBinder.Eval(dataItem, DataValueField);
      if (dataValueField.Equals(DBNull.Value))
        ddl.SelectedIndex = 0;
      else
        ddl.SelectedIndex =
        ddl.Items.IndexOf(ddl.Items.FindByValue(dataValueField.ToString()));
    }
    protected override void InitializeDataCell(DataControlFieldCell cell,
    DataControlRowState rowState)
    {
      if ((rowState & DataControlRowState.Edit) != 0 ||
      (rowState & DataControlRowState.Insert) != 0)
      {
        MasterDetailControl4 mdc = new MasterDetailControl4();
        mdc.MasterSkinID = MasterSkinID;
        mdc.DetailSkinID = DetailSkinID;
        mdc.EnableTheming = EnableTheming;
        mdc.MasterDataSourceID = this.MasterDataSourceID;
        mdc.DetailDataSourceID = this.DetailDataSourceID;
        mdc.DataKeyNames = DataKeyNames;
        ((DropDownList)mdc.Master).DataTextField = DataTextField;
        ((DropDownList)mdc.Master).DataValueField = DataValueField;
        if (DataTextField.Length != 0 && DataValueField.Length != 0)
          ((DropDownList)mdc.Master).DataBound +=
          new EventHandler(OnDataBindField);
        cell.Controls.Add(mdc);
      }
      else
        base.InitializeDataCell(cell, rowState);
    }
    public override void ExtractValuesFromCell(IOrderedDictionary dictionary,
    DataControlFieldCell cell,
    DataControlRowState rowState,
    bool includeReadOnly)
    {
      if (cell.Controls.Count > 0)
      {
        MasterDetailControl4 mdc = cell.Controls[0] as MasterDetailControl4;
        if (mdc == null)
          throw new InvalidOperationException(
          "MasterDetailField could not extract control.");
        string dataValueField = ((DropDownList)mdc.Master).SelectedValue;
        if (dictionary.Contains(DataValueField))
          dictionary[DataValueField] = int.Parse(dataValueField);
        else
          dictionary.Add(DataValueField, int.Parse(dataValueField));
      }
    }
  }
}