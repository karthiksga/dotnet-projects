using System;
using System.Web;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Collections.Generic;
namespace CustomComponents3
{
  [ParseChildren(true), DefaultProperty("Scripts"),
  NonVisualControl, PersistChildren(false)]
  public class ScriptManager : Control
  {
    public event EventHandler<ScriptReferenceEventArgs> ResolveScriptReference
    {
      add
      {
        base.Events.AddHandler(ResolveScriptReferenceEvent, value);
      }
      remove
      {
        base.Events.RemoveHandler(ResolveScriptReferenceEvent, value);
      }
    }
    private ScriptReferenceCollection _scripts;
    [PersistenceMode(PersistenceMode.InnerProperty),
    Editor("System.Web.UI.Design.CollectionEditorBase, System.Web.Extensions.Design, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", typeof(System.Drawing.Design.UITypeEditor)),
    DefaultValue((string)null), MergableProperty(false),
    Category("Behavior")]
    public ScriptReferenceCollection Scripts
    {
      get
      {
        if (this._scripts == null)
          this._scripts = new ScriptReferenceCollection();
        return this._scripts;
      }
    }
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      Page.Items[typeof(ScriptManager)] = this;
      this.Page.PreRenderComplete += new EventHandler(Page_PreRenderComplete);
    }
    public static ScriptManager GetCurrent(Page page)
    {
      return (page.Items[typeof(ScriptManager)] as ScriptManager);
    }
    private static readonly object ResolveScriptReferenceEvent = new object();
    protected virtual void OnResolveScriptReference(ScriptReferenceEventArgs e)
    {
      EventHandler<ScriptReferenceEventArgs> handler =
      (EventHandler<ScriptReferenceEventArgs>)
      base.Events[ResolveScriptReferenceEvent];
      if (handler != null)
        handler(this, e);
    }
    void Page_PreRenderComplete(object sender, EventArgs e)
    {
      List<ScriptReference> list1 = new List<ScriptReference>();
      this.CollectScripts(list1);
      ScriptReferenceEventArgs args;
      foreach (ScriptReference reference3 in list1)
      {
        args = new ScriptReferenceEventArgs(reference3);
        this.OnResolveScriptReference(args);
      }
      foreach (ScriptReference reference4 in list1)
      {
        string url = reference4.Path;
        if (this.LoadScriptsBeforeUI)
          this.Page.ClientScript.RegisterClientScriptInclude(typeof(ScriptManager),
          url, url);
        else
        {
          string script = "\r\n<script src=\"" +
          HttpUtility.HtmlAttributeEncode(url) +
          "\" type=\"text/javascript\"></script>";
          this.Page.ClientScript.RegisterStartupScript(typeof(ScriptManager),
          url, script, false);
        }
      }
    }
    private void CollectScripts(List<ScriptReference> scripts)
    {
      if (this._scripts != null)
      {
        foreach (ScriptReference reference1 in this._scripts)
        {
          reference1.ContainingControl = this;
          reference1.IsStaticReference = true;
          scripts.Add(reference1);
        }
      }
      this.AddScriptReferencesForScriptControls(scripts);
      this.AddScriptReferencesForExtenderControls(scripts);
    }
    private void AddScriptReferencesForScriptControls(
    List<ScriptReference> scriptReferences)
    {
      if (this._scriptControls != null)
      {
        foreach (IScriptControl scriptControl in this._scriptControls.Keys)
        {
          IEnumerable<ScriptReference> enumerable1 =
          scriptControl.GetScriptReferences();
          if (enumerable1 != null)
          {
            using (IEnumerator<ScriptReference> enumerator1 =
            enumerable1.GetEnumerator())
            {
              while (enumerator1.MoveNext())
              {
                ScriptReference reference1 = enumerator1.Current;
                if (reference1 != null)
                {
                  reference1.ContainingControl = (Control)scriptControl;
                  reference1.IsStaticReference = false;
                  scriptReferences.Add(reference1);
                }
              }
            }
          }
        }
      }
    }
    private void AddScriptReferencesForExtenderControls(List<ScriptReference>
    scriptReferences)
    {
      if (this._extenderControls != null)
      {
        foreach (IExtenderControl extenderControl in this._extenderControls.Keys)
        {
          IEnumerable<ScriptReference> enumerable1 =
          extenderControl.GetScriptReferences();
          if (enumerable1 != null)
          {
            using (IEnumerator<ScriptReference> enumerator1 =
            enumerable1.GetEnumerator())
            {
              while (enumerator1.MoveNext())
              {
                ScriptReference reference1 = enumerator1.Current;
                if (reference1 != null)
                {
                  reference1.IsStaticReference = false;
                  reference1.ContainingControl = (Control)extenderControl;
                  scriptReferences.Add(reference1);
                }
              }
            }
          }
        }
      }
    }
    public void RegisterScriptControl<TScriptControl>(TScriptControl scriptControl)
    where TScriptControl : Control, IScriptControl
    {
      int num;
      this.ScriptControls.TryGetValue(scriptControl, out num);
      num++;
      this.ScriptControls[scriptControl] = num;
    }
    private Dictionary<IScriptControl, int> _scriptControls;
    private Dictionary<IScriptControl, int> ScriptControls
    {
      get
      {
        if (this._scriptControls == null)
          this._scriptControls = new Dictionary<IScriptControl, int>();
        return this._scriptControls;
      }
    }
    public void RegisterExtenderControl<TExtenderControl>(TExtenderControl
    extenderControl, Control targetControl) where TExtenderControl :
    Control, IExtenderControl
    {
      List<Control> list;
      if (!this.ExtenderControls.TryGetValue(extenderControl, out list))
      {
        list = new List<Control>();
        this.ExtenderControls[extenderControl] = list;
      }
      list.Add(targetControl);
    }
    private Dictionary<IExtenderControl, List<Control>> _extenderControls;
    private Dictionary<IExtenderControl, List<Control>> ExtenderControls
    {
      get
      {
        if (this._extenderControls == null)
          this._extenderControls = new Dictionary<IExtenderControl,
          List<Control>>();
        return this._extenderControls;
      }
    }
    private bool _loadScriptsBeforeUI;
    [Category("Behavior"), DefaultValue(true)]
    public bool LoadScriptsBeforeUI
    {
      get { return this._loadScriptsBeforeUI; }
      set { this._loadScriptsBeforeUI = value; }
    }
    public void RegisterScriptDescriptors(IExtenderControl extenderControl)
    {
      List<Control> list;
      Control control = extenderControl as Control;
      if (!this.ExtenderControls.TryGetValue(extenderControl, out list))
        throw new ArgumentException("Extender Control Not Registered");
      foreach (Control control2 in list)
      {
        if (control2.Visible)
        {
          IEnumerable<ScriptDescriptor> scriptDescriptors =
          extenderControl.GetScriptDescriptors(control2);
          if (scriptDescriptors != null)
          {
            StringBuilder builder = null;
            foreach (ScriptDescriptor descriptor in scriptDescriptors)
            {
              if (builder == null)
              {
                builder = new StringBuilder();
                builder.AppendLine("Sys.Application.add_init(function() {");
              }
              builder.Append(" ");
              builder.AppendLine(descriptor.GetScript());
              descriptor.RegisterDisposeForDescriptor(this, control);
            }
            if (builder != null)
            {
              builder.AppendLine("});");
              string key = builder.ToString();
              Page.ClientScript.RegisterStartupScript(typeof(ScriptManager),
              key, key, true);
            }
          }
        }
      }
    }
    public void RegisterScriptDescriptors(IScriptControl scriptControl)
    {
      int num;
      Control control = scriptControl as Control;
      if (!this.ScriptControls.TryGetValue(scriptControl, out num))
        throw new ArgumentException("Script Control Not Registered");
      for (int i = 0; i < num; i++)
      {
        IEnumerable<ScriptDescriptor> scriptDescriptors =
        scriptControl.GetScriptDescriptors();
        if (scriptDescriptors != null)
        {
          StringBuilder builder = null;
          foreach (ScriptDescriptor descriptor in scriptDescriptors)
          {
            if (builder == null)
            {
              builder = new StringBuilder();
              builder.AppendLine("Sys.Application.add_init(function() {");
            }
            builder.Append(" ");
            builder.AppendLine(descriptor.GetScript());
            descriptor.RegisterDisposeForDescriptor(this, control);
          }
          if (builder != null)
          {
            builder.AppendLine("});");
            string key = builder.ToString();
            Page.ClientScript.RegisterStartupScript(typeof(ScriptManager),
            key, key, true);
          }
        }
      }
    }
  }
}