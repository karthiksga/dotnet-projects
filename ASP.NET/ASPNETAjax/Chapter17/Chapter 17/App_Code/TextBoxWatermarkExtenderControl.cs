using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace CustomComponents3
{
  [TargetControlType(typeof(IEditableTextControl))]
  public class TextBoxWatermarkExtenderControl : ExtenderControl
  {
    protected override IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReference reference1 = new ScriptReference();
      reference1.Path = ResolveClientUrl("BehaviorBase.js");
      ScriptReference reference2 = new ScriptReference();
      reference2.Path = ResolveClientUrl("TextBoxWatermarkBehavior.js");
      return new ScriptReference[] { reference1, reference2 };
    }
    protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors(
    Control targetControl)
    {
      ScriptBehaviorDescriptor descriptor =
      new ScriptBehaviorDescriptor("AjaxControlToolkit.TextBoxWatermarkBehavior",
      targetControl.ClientID);
      descriptor.AddProperty("WatermarkText", this.WatermarkText);
      descriptor.AddProperty("WatermarkCssClass", this.WatermarkCssClass);
      descriptor.AddProperty("id", this.BehaviorID);
      return new ScriptDescriptor[] { descriptor };
    }
    private string _clientState;
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ClientState
    {
      get { return _clientState; }
      set { _clientState = value; }
    }
    public string BehaviorID
    {
      get
      {
        return ViewState["BehaviorID"] != null ?
        (string)ViewState["BehaviorID"] : ClientID;
      }
      set
      {
        ViewState["BehaviorID"] = value;
      }
    }
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      Control targetControl = base.FindControl(TargetControlID);
      Control nc = NamingContainer;
      while ((targetControl == null) && (nc != null))
      {
        targetControl = nc.FindControl(TargetControlID);
        nc = nc.NamingContainer;
      }
      if (targetControl.Visible)
      {
        HiddenField hiddenField = null;
        if (string.IsNullOrEmpty(ClientStateFieldID))
          hiddenField = CreateClientStateField();
        else
          hiddenField =
          (HiddenField)NamingContainer.FindControl(ClientStateFieldID);
        if (hiddenField != null)
          hiddenField.Value = ClientState;
      }
    }
    private HiddenField CreateClientStateField()
    {
      HiddenField field = new HiddenField();
      field.ID = string.Format(CultureInfo.InvariantCulture,
      "{0}_ClientState", ID);
      Controls.Add(field);
      ClientStateFieldID = field.ID;
      return field;
    }
    protected override void Render(HtmlTextWriter writer)
    {
      if (Page != null)
        Page.VerifyRenderingInServerForm(this);
      base.Render(writer);
    }
    protected override void OnInit(EventArgs e)
    {
      CreateClientStateField();
      Page.PreLoad += new EventHandler(Page_PreLoad);
      base.OnInit(e);
    }
    void Page_PreLoad(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(ClientStateFieldID))
      {
        HiddenField hiddenField =
        (HiddenField)NamingContainer.FindControl(ClientStateFieldID);
        if ((hiddenField != null) && !string.IsNullOrEmpty(hiddenField.Value))
          ClientState = hiddenField.Value;
      }
    }
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [IDReferenceProperty(typeof(HiddenField))]
    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ClientStateFieldID
    {
      get
      {
        return ViewState["ClientStateFieldID"] != null ?
          (string)ViewState["ClientStateFieldID"] : string.Empty;
      }
      set { ViewState["ClientStateFieldID"] = value; }
    }
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      string key;
      string script;
      key = string.Format(CultureInfo.InvariantCulture, "{0}_onSubmit", ID);
      script = string.Format(CultureInfo.InvariantCulture, "var o = $find('{0}'); if(o) {{ o._onSubmit(); }}", BehaviorID);
      System.Web.UI.ScriptManager.RegisterOnSubmitStatement(this,
      typeof(TextBoxWatermarkExtenderControl), key, script);
      ClientState = (string.Compare(Page.Form.DefaultFocus, TargetControlID,
      StringComparison.InvariantCultureIgnoreCase) == 0) ? "Focused" : null;
    }
    private string watermarkText;
    [DefaultValue("")]
    public string WatermarkText
    {
      get { return this.watermarkText; }
      set { this.watermarkText = value; }
    }
    private string watermarkCssClass;
    [DefaultValue("")]
    public string WatermarkCssClass
    {
      get { return this.watermarkCssClass; }
      set { this.watermarkCssClass = value; }
    }
  }
}