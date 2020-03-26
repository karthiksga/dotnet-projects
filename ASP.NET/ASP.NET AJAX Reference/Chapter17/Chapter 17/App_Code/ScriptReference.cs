using System;
using System.Web.UI;
using System.ComponentModel;
namespace CustomComponents3
{
  public class ScriptReference
  {
    private Control _containingControl;
    private bool _isStaticReference;
    private string _path;
    public ScriptReference() { }
    public ScriptReference(string path)
      : this()
    {
      this.Path = path;
    }
    [DefaultValue(""), Category("Behavior")]
    public string Path
    {
      get
      {
        if (this._path != null)
          return this._path;
        return string.Empty;
      }
      set { this._path = value; }
    }
    internal bool IsStaticReference
    {
      get { return this._isStaticReference; }
      set { this._isStaticReference = value; }
    }
    internal Control ContainingControl
    {
      get { return this._containingControl; }
      set { this._containingControl = value; }
    }
  }
}