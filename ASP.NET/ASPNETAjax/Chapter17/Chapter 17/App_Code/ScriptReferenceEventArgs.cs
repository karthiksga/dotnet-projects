using System;
namespace CustomComponents3
{
  public class ScriptReferenceEventArgs : EventArgs
  {
    private readonly ScriptReference _script;
    public ScriptReferenceEventArgs(ScriptReference script)
    {
      if (script == null)
        throw new ArgumentNullException("script");
      this._script = script;
    }
    public ScriptReference Script
    {
      get { return this._script; }
    }
  }
}