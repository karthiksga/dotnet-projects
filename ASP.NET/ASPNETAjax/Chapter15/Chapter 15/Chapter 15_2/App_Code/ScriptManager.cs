using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Compilation;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.ObjectModel;
namespace CustomComponents
{
  [ParseChildren(true), DefaultProperty("Scripts"),
  NonVisualControl, PersistChildren(false)]
  public class ScriptManager : Control
  {
    private ServiceReferenceCollection _services;
    [PersistenceMode(PersistenceMode.InnerProperty),
    Editor("System.Web.UI.Design.CollectionEditorBase,System.Web.Extensions.Design, Version=1.0.61025.0, Culture=neutral,PublicKeyToken=31bf3856ad364e35",
    typeof(System.Drawing.Design.UITypeEditor)),
    DefaultValue((string)null), MergableProperty(false),
    Category("Behavior")]
    public ServiceReferenceCollection Services
    {
      get
      {
        if (this._services == null)
          this._services = new ServiceReferenceCollection();
        return this._services;
      }
    }
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.Page.PreRenderComplete += new EventHandler(Page_PreRenderComplete);
    }
    void Page_PreRenderComplete(object sender, EventArgs e)
    {
      if (this._services != null)
      {
        foreach (ServiceReference reference in this._services)
        {
          reference.Register(this);
        }
      }
      if (this.EnablePageMethods)
      {
        ClientProxyGenerator generator2 = new ClientProxyGenerator();
        string script =
        generator2.GetClientProxyScript(this.Page.Request.FilePath, true);
        this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), script,
        script, true);
      }
    }
    public bool EnablePageMethods
    {
      get
      {
        return ViewState["EnablePageMethods"] != null ?
        (bool)ViewState["EnablePageMethods"] : false;
      }
      set
      {
        ViewState["EnablePageMethods"] = value;
      }
    }
  }
}