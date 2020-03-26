using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
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
  public class ServiceReference
  {
    private bool _inlineScript;
    public bool InlineScript
    {
      get
      {
        return this._inlineScript;
      }
      set
      {
        this._inlineScript = value;
      }
    }
    private string _path;
    public string Path
    {
      get
      {
        if (this._path == null)
          return string.Empty;
        return this._path;
      }
      set
      {
        this._path = value;
      }
    }
    public void Register(Control control)
    {
      if (this._inlineScript)
      {
        ClientProxyGenerator generator = new ClientProxyGenerator();
        string inlineScript;
        inlineScript = generator.GetClientProxyScript(this.Path, false);
        control.Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
        inlineScript, inlineScript, true);
      }
      else
      {
        string url = this.Path + "/js";
        control.Page.ClientScript.RegisterClientScriptInclude(typeof(Page),
        url, url);
      }
    }
  }
}