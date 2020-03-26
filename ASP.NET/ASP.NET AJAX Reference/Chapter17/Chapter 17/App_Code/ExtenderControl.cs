namespace CustomComponents3
{
  using System;
  using System.Web.UI;
  using System.ComponentModel;
  using System.Collections.Generic;
  [DefaultProperty("TargetControlID"), ParseChildren(true),
  NonVisualControl,
  PersistChildren(false)]
  public abstract class ExtenderControl : Control, IExtenderControl
  {
    private ScriptManager _scriptManager;
    private string _targetControlID;
    protected abstract IEnumerable<ScriptDescriptor> GetScriptDescriptors(
    Control targetControl);
    protected abstract IEnumerable<ScriptReference> GetScriptReferences();
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      Control control = this.FindControl(this.TargetControlID);
      ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
      scriptManager.RegisterExtenderControl<ExtenderControl>(this, control);
    }
    protected override void Render(HtmlTextWriter writer)
    {
      base.Render(writer);
      if (!base.DesignMode)
      {
        ScriptManager mgr = ScriptManager.GetCurrent(Page);
        mgr.RegisterScriptDescriptors(this);
      }
    }
    IEnumerable<ScriptDescriptor> IExtenderControl.GetScriptDescriptors(
    Control targetControl)
    {
      return this.GetScriptDescriptors(targetControl);
    }
    IEnumerable<ScriptReference> IExtenderControl.GetScriptReferences()
    {
      return this.GetScriptReferences();
    }
    [DefaultValue(""),
    IDReferenceProperty, Category("Behavior")]
    public string TargetControlID
    {
      get
      {
        if (this._targetControlID != null)
          return this._targetControlID;
        return string.Empty;
      }
      set { this._targetControlID = value; }
    }
    [Browsable(false),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
    EditorBrowsable(EditorBrowsableState.Never)]
    public override bool Visible
    {
      get { return base.Visible; }
      set { throw new NotImplementedException(); }
    }
  }
}