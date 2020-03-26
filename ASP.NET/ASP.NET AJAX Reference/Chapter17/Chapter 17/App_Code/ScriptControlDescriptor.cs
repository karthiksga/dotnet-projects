namespace CustomComponents3
{
  using System;
  public class ScriptControlDescriptor : ScriptComponentDescriptor
  {
    public ScriptControlDescriptor(string type, string elementID)
      : base(type,
        elementID)
    {
      base.RegisterDispose = false;
    }
    public override string ClientID
    {
      get { return this.ElementID; }
    }
    public string ElementID
    {
      get { return base.ElementIDInternal; }
    }
    public override string ID
    {
      get { return base.ID; }
      set { throw new InvalidOperationException("ID Not Settable"); }
    }
  }
}