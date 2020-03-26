using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
namespace CustomComponents5
{
  public class CustomUpdatePanel : UpdatePanel
  {
    public string BuildTemplateMethodProviderType
    {
      get
      {
        return ViewState["BuildTemplateMethodProviderType"] != null ?
        (string)ViewState["BuildTemplateMethodProviderType"] : string.Empty;
      }
      set
      {
        ViewState["BuildTemplateMethodProviderType"] = value;
      }
    }
    public string BuildTemplateMethodProviderMethod
    {
      get
      {
        return ViewState["BuildTemplateMethodProviderMethod"] != null ?
        (string)ViewState["BuildTemplateMethodProviderMethod"] : string.Empty;
      }
      set
      {
        ViewState["BuildTemplateMethodProviderMethod"] = value;
      }
    }
    protected override void OnInit(EventArgs e)
    {
      if (!string.IsNullOrEmpty(BuildTemplateMethodProviderType) &&
      !string.IsNullOrEmpty(BuildTemplateMethodProviderMethod))
      {
        string typeName = BuildTemplateMethodProviderType.Trim().Split(
        new char[] { ',' })[0];
        string assemblyName = BuildTemplateMethodProviderType.Trim().Remove(
        BuildTemplateMethodProviderType.IndexOf(typeName),
        typeName.Length);
        Assembly assembly;
        if (string.IsNullOrEmpty(assemblyName))
          assembly = Assembly.GetExecutingAssembly();
        else
        {
          assemblyName = assemblyName.Trim().Remove(0, 1);
          assembly = Assembly.Load(assemblyName);
        }
        object provider = assembly.CreateInstance(typeName);
        Type type = provider.GetType();
        MethodInfo methodInfo =
        type.GetMethod(BuildTemplateMethodProviderMethod);
        BuildTemplateMethod method =
        (BuildTemplateMethod)methodInfo.Invoke(provider, null);
        ContentTemplate = new CompiledTemplateBuilder(method);
      }
      base.OnInit(e);
    }
  }
}