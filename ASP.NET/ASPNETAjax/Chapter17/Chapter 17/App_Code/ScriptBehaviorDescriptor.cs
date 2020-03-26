namespace CustomComponents3
{
  public class ScriptBehaviorDescriptor : ScriptComponentDescriptor
  {
    private string _name;
    public ScriptBehaviorDescriptor(string type, string elementID)
      : base(type, elementID)
    {
      base.RegisterDispose = false;
    }
    protected internal override string GetScript()
    {
      if (!string.IsNullOrEmpty(this._name))
        base.AddProperty("name", this._name);
      return base.GetScript();
    }
    private static string GetTypeName(string type)
    {
      int num = type.LastIndexOf('.');
      if (num == -1)
        return type;
      return type.Substring(num + 1);
    }
    public override string ClientID
    {
      get
      {
        if (string.IsNullOrEmpty(this.ID))
          return (this.ElementID + "$" + this.Name);
        return this.ID;
      }
    }
    public string ElementID
    {
      get { return base.ElementIDInternal; }
    }
    public string Name
    {
      get
      {
        if (string.IsNullOrEmpty(this._name))
          return GetTypeName(base.Type);
        return this._name;
      }
      set { this._name = value; }
    }
  }
}