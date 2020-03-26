using System.Collections.Generic;
namespace CustomComponents3
{
  public interface IScriptControl
  {
    IEnumerable<ScriptDescriptor> GetScriptDescriptors();
    IEnumerable<ScriptReference> GetScriptReferences();
  }
}