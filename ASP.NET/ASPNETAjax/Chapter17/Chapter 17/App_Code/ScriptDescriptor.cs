namespace CustomComponents3
{
  using System.Web.UI;
  public abstract class ScriptDescriptor
  {
    protected internal abstract string GetScript();
    internal virtual void RegisterDisposeForDescriptor(ScriptManager scriptManager,
    Control owner)
    {
    }
  }
}