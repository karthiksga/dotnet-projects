using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
namespace CustomComponents
{
  public class AsyncMultiPostBackTrigger : UpdatePanelTrigger
  {
    protected Control[] FindTargetControls(bool searchNamingContainers)
    {
      ArrayList list = new ArrayList();
      if (searchNamingContainers)
      {
        Control control2 = null;
        Control control1 = null;
        foreach (string controlID in this._controlIDs)
        {
          control2 = base.Owner;
          control1 = null;
          while ((control1 == null) && (control2 != base.Owner.Page))
          {
            control2 = control2.NamingContainer;
            if (control2 == null)
              break;
            control1 = control2.FindControl(controlID);
          }
          list.Add(control1);
        }
      }
      else
      {
        foreach (string controlID in this._controlIDs)
        {
          list.Add(base.Owner.FindControl(controlID));
        }
      }
      Control[] controls = new Control[list.Count];
      list.CopyTo(controls);
      return controls;
    }
    protected override bool HasTriggered()
    {
      if (this.EventNames != null && this.EventNames.Length > 0 &&
      !String.IsNullOrEmpty(this.EventNames[0]))
        return this._eventHandled;
      System.Web.UI.ScriptManager sm = System.Web.UI.ScriptManager.GetCurrent(this.Owner.Page);
      foreach (Control associatedControl in this._associatedControls)
      {
        if (sm.AsyncPostBackSourceElementID != associatedControl.UniqueID)
          return sm.AsyncPostBackSourceElementID.StartsWith(
          associatedControl.UniqueID + "$", StringComparison.Ordinal);
      }
      return true;
    }
    protected override void Initialize()
    {
      base.Initialize();
      this._associatedControls = this.FindTargetControls(true);
      System.Web.UI.ScriptManager sm = System.Web.UI.ScriptManager.GetCurrent(this.Owner.Page);
      Control associatedControl = null;
      string eventName = "";
      Type associatedControlType;
      EventInfo eventInfo;
      Type eventDelegateType;
      MethodInfo methodInfo;
      Delegate delegate1;
      for (int i = 0; i < this._associatedControls.Length; i++)
      {
        associatedControl = this._associatedControls[i];
        eventName = this.EventNames[i];
        sm.RegisterAsyncPostBackControl(associatedControl);
        associatedControlType = associatedControl.GetType();
        eventInfo = associatedControlType.GetEvent(eventName,
        BindingFlags.Public | BindingFlags.Instance |
        BindingFlags.IgnoreCase);
        eventDelegateType = eventInfo.EventHandlerType;
        methodInfo = eventDelegateType.GetMethod("Invoke");
        delegate1 = Delegate.CreateDelegate(eventDelegateType, this,
        AsyncMultiPostBackTrigger.EventHandler);
        eventInfo.AddEventHandler(associatedControl, delegate1);
      }
    }
    public void OnEvent(object sender, EventArgs e)
    {
      this._eventHandled = true;
    }
    [TypeConverter(typeof(StringArrayConverter))]
    public string[] ControlIDs
    {
      get { return this._controlIDs; }
      set { this._controlIDs = value; }
    }
    private static MethodInfo EventHandler
    {
      get
      {
        if (AsyncMultiPostBackTrigger._eventHandler == null)
          AsyncMultiPostBackTrigger._eventHandler =
          typeof(AsyncMultiPostBackTrigger).GetMethod("OnEvent");
        return AsyncMultiPostBackTrigger._eventHandler;
      }
    }
    [TypeConverter(typeof(StringArrayConverter))]
    public string[] EventNames
    {
      get { return this._eventNames; }
      set { this._eventNames = value; }
    }
    private bool _eventHandled;
    private Control[] _associatedControls;
    private static MethodInfo _eventHandler;
    private string[] _eventNames;
    private string[] _controlIDs;
  }
}