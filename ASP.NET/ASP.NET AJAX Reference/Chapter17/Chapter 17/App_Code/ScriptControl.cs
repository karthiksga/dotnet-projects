namespace CustomComponents3
{
  using System;
  using System.Web.UI;
  using System.Collections.Generic;
  using System.Web.UI.WebControls;
  public abstract class ScriptControl : WebControl, IScriptControl
  {
    protected abstract IEnumerable<ScriptDescriptor> GetScriptDescriptors();
    protected abstract IEnumerable<ScriptReference> GetScriptReferences();
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
      scriptManager.RegisterScriptControl<ScriptControl>(this);
    }
    protected override void Render(HtmlTextWriter writer)
    {
      base.Render(writer);
      if (!base.DesignMode)
      {
        ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
        scriptManager.RegisterScriptDescriptors(this);
      }
    }
    IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
    {
      return this.GetScriptDescriptors();
    }
    IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
    {
      return this.GetScriptReferences();
    }
  }
}