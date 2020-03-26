using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using com.amazon.webservices;
using System.ComponentModel;
namespace CustomComponents3
{
  public class HtmlGeneratorScriptControl : ScriptControl
  {
    protected override Style CreateControlStyle()
    {
      return new TableStyle(ViewState);
    }
    private TableItemStyle rowStyle;
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual TableItemStyle RowStyle
    {
      get
      {
        if (rowStyle == null)
        {
          rowStyle = new TableItemStyle();
          if (IsTrackingViewState)
            ((IStateManager)rowStyle).TrackViewState();
        }
        return rowStyle;
      }
    }
    private TableItemStyle alternatingRowStyle;
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual TableItemStyle AlternatingRowStyle
    {
      get
      {
        if (alternatingRowStyle == null)
        {
          alternatingRowStyle = new TableItemStyle();
          if (IsTrackingViewState)
            ((IStateManager)alternatingRowStyle).TrackViewState();
        }
        return alternatingRowStyle;
      }
    }
    protected override object SaveViewState()
    {
      object[] state = new object[3];
      state[0] = base.SaveViewState();
      if (this.rowStyle != null)
        state[1] = ((IStateManager)rowStyle).SaveViewState();
      if (this.alternatingRowStyle != null)
        state[2] = ((IStateManager)alternatingRowStyle).SaveViewState();
      foreach (object obj in state)
      {
        if (obj != null)
          return state;
      }
      return null;
    }
    protected override void LoadViewState(object savedState)
    {
      if (savedState == null)
      {
        base.LoadViewState(savedState);
        return;
      }
      object[] state = savedState as object[];
      if (state == null || state.Length != 3)
        return;
      base.LoadViewState(state[0]);
      if (state[1] != null)
        ((IStateManager)RowStyle).LoadViewState(state[1]);
      if (state[2] != null)
        ((IStateManager)AlternatingRowStyle).LoadViewState(state[2]);
    }
    protected override void TrackViewState()
    {
      base.TrackViewState();
      if (rowStyle != null)
        ((IStateManager)RowStyle).TrackViewState();
      if (alternatingRowStyle != null)
        ((IStateManager)AlternatingRowStyle).TrackViewState();
    }
    protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptComponentDescriptor descriptor =
      new ScriptComponentDescriptor(this.ClientControlType);
      descriptor.AddProperty("id", this.ClientID);
      CssStyleCollection col;
      if (ControlStyleCreated)
      {
        col = ControlStyle.GetStyleAttributes(this);
        descriptor.AddProperty("tableStyle", col.Value);
      }
      if (this.rowStyle != null)
      {
        col = rowStyle.GetStyleAttributes(this);
        descriptor.AddProperty("rowStyle", col.Value);
      }
      if (this.alternatingRowStyle != null)
      {
        col = alternatingRowStyle.GetStyleAttributes(this);
        descriptor.AddProperty("alternatingRowStyle", col.Value);
      }
      return new ScriptDescriptor[] { descriptor };
    }
    protected override IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReference reference = new ScriptReference();
      reference.Path = this.Path;
      return new ScriptReference[] { reference };
    }
    public string Path
    {
      get
      {
        return ViewState["Path"] != null ?
        (string)ViewState["Path"] : string.Empty;
      }
      set
      {
        ViewState["Path"] = value;
      }
    }
    public string ClientControlType
    {
      get
      {
        return ViewState["ClientControlType"] != null ?
        (string)ViewState["ClientControlType"] : string.Empty;
      }
      set
      {
        ViewState["ClientControlType"] = value;
      }
    }
    public virtual int CellPadding
    {
      get { return ((TableStyle)ControlStyle).CellPadding; }
      set { ((TableStyle)ControlStyle).CellPadding = value; }
    }
    public virtual int CellSpacing
    {
      get { return ((TableStyle)ControlStyle).CellSpacing; }
      set { ((TableStyle)ControlStyle).CellSpacing = value; }
    }
    public virtual HorizontalAlign HorizontalAlign
    {
      get { return ((TableStyle)ControlStyle).HorizontalAlign; }
      set { ((TableStyle)ControlStyle).HorizontalAlign = value; }
    }
    public virtual string BackImageUrl
    {
      get { return ((TableStyle)ControlStyle).BackImageUrl; }
      set { ((TableStyle)ControlStyle).BackImageUrl = value; }
    }
    public virtual GridLines GridLines
    {
      get { return ((TableStyle)ControlStyle).GridLines; }
      set { ((TableStyle)ControlStyle).GridLines = value; }
    }
  }
}