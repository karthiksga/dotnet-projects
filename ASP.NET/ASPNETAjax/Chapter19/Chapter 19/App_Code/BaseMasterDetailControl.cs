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
  public abstract class BaseMasterDetailControl : CompositeControl
  {
    Control master;
    Control detail;
    UpdatePanel masterUpdatePanel;
    UpdatePanel detailUpdatePanel;
    MasterDetailContainer masterContainer;
    MasterDetailContainer detailContainer;
    protected abstract Control CreateMaster();
    protected abstract Control CreateDetail();
    protected abstract void RegisterMasterEventHandlers();
    protected abstract void RegisterDetailEventHandlers();
    public Control Master
    {
      get { EnsureChildControls(); return this.master; }
    }
    public Control Detail
    {
      get { EnsureChildControls(); return this.detail; }
    }
    public string MasterSkinID
    {
      get
      {
        EnsureChildControls();
        return master.SkinID;
      }
      set
      {
        EnsureChildControls();
        master.SkinID = value;
      }
    }
    public string DetailSkinID
    {
      get
      {
        EnsureChildControls();
        return detail.SkinID;
      }
      set
      {
        EnsureChildControls();
        detail.SkinID = value;
      }
    }
    public virtual object SelectedValue
    {
      get { return ViewState["SelectedValue"]; }
      set { ViewState["SelectedValue"] = value; }
    }
    protected override Style CreateControlStyle()
    {
      return new TableStyle(ViewState);
    }
    public virtual GridLines GridLines
    {
      get { return ((TableStyle)ControlStyle).GridLines; }
      set { ((TableStyle)ControlStyle).GridLines = value; }
    }
    public virtual int CellSpacing
    {
      get { return ((TableStyle)ControlStyle).CellSpacing; }
      set { ((TableStyle)ControlStyle).CellSpacing = value; }
    }
    public virtual int CellPadding
    {
      get { return ((TableStyle)ControlStyle).CellPadding; }
      set { ((TableStyle)ControlStyle).CellPadding = value; }
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
    protected virtual void CreateContainerChildControls(
    MasterDetailContainer container)
    {
      switch (container.ContainerType)
      {
        case ContainerType.Master:
          masterUpdatePanel = new UpdatePanel();
          masterUpdatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
          master = this.CreateMaster();
          if (string.IsNullOrEmpty(master.ID))
            master.ID = "MasterServerControl";
          this.RegisterMasterEventHandlers();
          masterUpdatePanel.ContentTemplateContainer.Controls.Add(master);
          container.Controls.Add(masterUpdatePanel);
          break;
        case ContainerType.Detail:
          detailUpdatePanel = new UpdatePanel();
          detailUpdatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
          detail = this.CreateDetail();
          if (string.IsNullOrEmpty(detail.ID))
            detail.ID = "DetailServerControl";
          this.RegisterDetailEventHandlers();
          detailUpdatePanel.ContentTemplateContainer.Controls.Add(detail);
          container.Controls.Add(detailUpdatePanel);
          break;
      }
    }
    protected void UpdateMaster(object sender, EventArgs e)
    {
      master.DataBind();
      masterUpdatePanel.Update();
    }
    protected void UpdateDetail(object sender, EventArgs e)
    {
      detail.DataBind();
      detailUpdatePanel.Update();
    }
    protected virtual void AddContainer(MasterDetailContainer container)
    {
      Controls.Add(container);
    }
    protected virtual void RenderContainer(MasterDetailContainer container,
    HtmlTextWriter writer)
    {
      container.RenderControl(writer);
    }
    protected virtual MasterDetailContainer CreateContainer
    (ContainerType containerType)
    {
      return new MasterDetailContainer(containerType);
    }
    private TableItemStyle masterContainerStyle;
    [DefaultValue((string)null)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TableItemStyle MasterContainerStyle
    {
      get
      {
        if (masterContainerStyle == null)
        {
          masterContainerStyle = new TableItemStyle();
          if (IsTrackingViewState)
            ((IStateManager)masterContainerStyle).TrackViewState();
        }
        return masterContainerStyle;
      }
    }
    private TableItemStyle detailContainerStyle;
    [DefaultValue((string)null)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TableItemStyle DetailContainerStyle
    {
      get
      {
        if (detailContainerStyle == null)
        {
          detailContainerStyle = new TableItemStyle();
          if (IsTrackingViewState)
            ((IStateManager)detailContainerStyle).TrackViewState();
        }
        return detailContainerStyle;
      }
    }
    protected override void TrackViewState()
    {
      base.TrackViewState();
      if (masterContainerStyle != null)
        ((IStateManager)masterContainerStyle).TrackViewState();
      if (detailContainerStyle != null)
        ((IStateManager)detailContainerStyle).TrackViewState();
    }
    protected override object SaveViewState()
    {
      object[] state = new object[3];
      state[0] = base.SaveViewState();
      if (masterContainerStyle != null)
        state[1] = ((IStateManager)masterContainerStyle).SaveViewState();
      if (detailContainerStyle != null)
        state[2] = ((IStateManager)detailContainerStyle).SaveViewState();
      foreach (object obj in state)
      {
        if (obj != null)
          return state;
      }
      return null;
    }
    protected override void LoadViewState(object savedState)
    {
      if (savedState != null)
      {
        object[] state = savedState as object[];
        if (state != null && state.Length == 3)
        {
          base.LoadViewState(state[0]);
          if (state[1] != null)
            ((IStateManager)MasterContainerStyle).LoadViewState(state[1]);
          if (state[2] != null)
            ((IStateManager)DetailContainerStyle).LoadViewState(state[2]);
        }
      }
      else
        base.LoadViewState(savedState);
    }
    protected virtual void ApplyContainerStyles()
    {
      foreach (MasterDetailContainer container in Controls)
      {
        switch (container.ContainerType)
        {
          case ContainerType.Master:
            if (masterContainerStyle != null)
              container.ApplyStyle(masterContainerStyle);
            break;
          case ContainerType.Detail:
            if (detailContainerStyle != null)
              container.ApplyStyle(detailContainerStyle);
            break;
        }
      }
    }
    protected override void CreateChildControls()
    {
      Controls.Clear();
      masterContainer = CreateContainer(ContainerType.Master);
      CreateContainerChildControls(masterContainer);
      AddContainer(masterContainer);
      detailContainer = CreateContainer(ContainerType.Detail);
      CreateContainerChildControls(detailContainer);
      AddContainer(detailContainer);
      ChildControlsCreated = true;
    }
    protected override HtmlTextWriterTag TagKey
    {
      get { return HtmlTextWriterTag.Table; }
    }
    protected override void RenderContents(HtmlTextWriter writer)
    {
      ApplyContainerStyles();
      writer.RenderBeginTag(HtmlTextWriterTag.Tr);
      RenderContainer(masterContainer, writer);
      writer.RenderEndTag();
      writer.RenderBeginTag(HtmlTextWriterTag.Tr);
      RenderContainer(detailContainer, writer);
      writer.RenderEndTag();
    }
  }
}
