using System;
using System.Web.UI;
using System.Collections.Generic;
namespace CustomComponents3
{
  public interface IExtenderControl
  {
    IEnumerable<ScriptDescriptor> GetScriptDescriptors(Control targetControl);
    IEnumerable<ScriptReference> GetScriptReferences();
  }
}