namespace CustomComponents3
{
  using System.Collections.Generic;
  using System.Web.Script.Serialization;
  using System.Web.UI;
  using System.Text;
  using System;
  public class ScriptComponentDescriptor : ScriptDescriptor
  {
    // Fields
    private string _elementIDInternal;
    private SortedList<string, string> _events;
    private string _id;
    private SortedList<string, string> _properties;
    private SortedList<string, string> _references;
    private bool _registerDispose;
    private JavaScriptSerializer _serializer;
    private string _type;
    // Methods
    public ScriptComponentDescriptor(string type)
    {
      this._registerDispose = true;
      this._type = type;
    }
    internal ScriptComponentDescriptor(string type, string elementID)
      : this(type)
    {
      this._elementIDInternal = elementID;
    }
    public void AddComponentProperty(string name, string componentID)
    {
      string value = "\"";
      value += HelperMethods.QuoteString(componentID);
      value += "\"";
      References[name] = value;
    }
    public void AddElementProperty(string name, string elementID)
    {
      string value = "$get(\"";
      value += HelperMethods.QuoteString(elementID);
      value += "\")";
      Properties[name] = value;
    }
    public void AddEvent(string name, string handler)
    {
      this.Events[name] = handler;
    }
    public void AddProperty(string name, object val)
    {
      string value = this.Serializer.Serialize(val);
      Properties[name] = value;
    }
    public void AddScriptProperty(string name, string script)
    {
      Properties[name] = script;
    }
    private void AppendScript(SortedList<string, string> list,
    StringBuilder builder)
    {
      bool flag = true;
      if ((list != null) && (list.Count > 0))
      {
        foreach (KeyValuePair<string, string> pair in list)
        {
          if (flag)
          {
            builder.Append("{");
            flag = false;
          }
          else
            builder.Append(",");
          builder.Append('"');
          builder.Append(HelperMethods.QuoteString(pair.Key));
          builder.Append('"');
          builder.Append(':');
          builder.Append(pair.Value);
        }
      }
      if (flag)
        builder.Append("null");
      else
        builder.Append("}");
    }
    protected internal override string GetScript()
    {
      if (!string.IsNullOrEmpty(this.ID))
        this.AddProperty("id", this.ID);
      StringBuilder builder = new StringBuilder();
      builder.Append("$create(");
      builder.Append(this.Type);
      builder.Append(", ");
      this.AppendScript(this._properties, builder);
      builder.Append(", ");
      this.AppendScript(this._events, builder);
      builder.Append(", ");
      this.AppendScript(this._references, builder);
      if (this.ElementIDInternal != null)
      {
        builder.Append(", ");
        builder.Append("$get(\"");
        builder.Append(HelperMethods.QuoteString(this.ElementIDInternal));
        builder.Append("\")");
      }
      builder.Append(");");
      return builder.ToString();
    }
    public virtual string ClientID
    {
      get { return this.ID; }
    }
    internal string ElementIDInternal
    {
      get { return this._elementIDInternal; }
    }
    private SortedList<string, string> Events
    {
      get
      {
        if (this._events == null)
          this._events = new SortedList<string, string>(StringComparer.Ordinal);
        return this._events;
      }
    }
    public virtual string ID
    {
      get { return (this._id ?? string.Empty); }
      set { this._id = value; }
    }
    private SortedList<string, string> Properties
    {
      get
      {
        if (this._properties == null)
          this._properties =
          new SortedList<string, string>(StringComparer.Ordinal);
        return this._properties;
      }
    }
    private SortedList<string, string> References
    {
      get
      {
        if (this._references == null)
          this._references =
          new SortedList<string, string>(StringComparer.Ordinal);
        return this._references;
      }
    }
    internal bool RegisterDispose
    {
      get { return this._registerDispose; }
      set { this._registerDispose = value; }
    }
    private JavaScriptSerializer Serializer
    {
      get
      {
        if (this._serializer == null)
          this._serializer = new JavaScriptSerializer();
        return this._serializer;
      }
    }
    public string Type
    {
      get { return this._type; }
      set { this._type = value; }
    }
  }
}