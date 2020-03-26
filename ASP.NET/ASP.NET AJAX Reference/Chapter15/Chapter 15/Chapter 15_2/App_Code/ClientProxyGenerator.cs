using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Compilation;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.ObjectModel;
namespace CustomComponents
{
  public class ClientProxyGenerator
  {
    private StringBuilder _builder = new StringBuilder();
    private Type _serviceType;
    private string _serviceFullName;
    private string _servicePath;
    private bool _isPageMethod;
    private ArrayList _methodInfos;
    private Dictionary<MethodInfo, ArrayList> _parameterInfos;
    private void PopulateMethodInfos()
    {
      _methodInfos = new ArrayList();
      MethodInfo[] infoArray = _serviceType.GetMethods();
      foreach (MethodInfo info in infoArray)
      {
        object[] objArray = info.GetCustomAttributes(typeof(WebMethodAttribute),
        true);
        if (objArray.Length != 0)
          _methodInfos.Add(info);
      }
    }
    private void PopulateParameterInfos()
    {
      _parameterInfos = new Dictionary<MethodInfo, ArrayList>();
      ParameterInfo[] list;
      ArrayList list2;
      foreach (MethodInfo info in _methodInfos)
      {
        list = info.GetParameters();
        list2 = new ArrayList();
        list2.AddRange(list);
        _parameterInfos.Add(info, list2);
      }
    }
    private void DetermineServiceType()
    {
      _serviceType = BuildManager.GetCompiledType(this._servicePath);
      if (_serviceType == null)
        _serviceType = BuildManager.CreateInstanceFromVirtualPath(_servicePath,
        typeof(Page)).GetType();
    }
    private void DetermineServiceFullName()
    {
      if (this._isPageMethod)
        _serviceFullName = "PageMethods";
      else
        _serviceFullName = _serviceType.FullName;
    }
    public string GetClientProxyScript(string servicePath, bool isPageMethod)
    {
      this._servicePath = servicePath;
      this._isPageMethod = isPageMethod;
      this.DetermineServiceType();
      this.DetermineServiceFullName();
      this.PopulateMethodInfos();
      this.PopulateParameterInfos();
      if (!this._isPageMethod)
        this.GenerateNamespace();
      this.GenerateConstructor();
      this.GeneratePrototype();
      this.GenerateRegisterClass();
      this.GenerateStaticInstance();
      this.GenerateStaticMethods();
      return this._builder.ToString();
    }
    private void GenerateNamespace()
    {
      this._builder.Append("\r\nType.registerNamespace('");
      this._builder.Append(_serviceType.Namespace);
      this._builder.Append("');\r\n\r\n");
    }
    private void GenerateConstructor()
    {
      _builder.Append(_serviceFullName);
      _builder.Append(" = ");
      _builder.Append("function()\r\n{\r\n\t");
      _builder.Append(_serviceFullName);
      _builder.Append(".initializeBase(this);\r\n");
      _builder.Append("\tthis._timeout = 0;\r\n");
      _builder.Append("\tthis._userContext = null;\r\n");
      _builder.Append("\tthis._succeeded = null;\r\n");
      _builder.Append("\tthis._failed = null;\r\n");
      _builder.Append("}\r\n\r\n");
    }
    private void GenerateWebMethodProxy(MethodInfo methodInfo)
    {
      ArrayList parameterList = _parameterInfos[methodInfo];
      _builder.Append(methodInfo.Name);
      _builder.Append(" : ");
      _builder.Append("function(");
      foreach (ParameterInfo pinfo in parameterList)
      {
        _builder.Append(pinfo.Name);
        _builder.Append(", ");
      }
      _builder.Append("succeededCallback, failedCallback, userContext)");
      _builder.Append("\r\n{\r\n");
      _builder.Append("\treturn this._invoke(");
      _builder.Append(_serviceFullName);
      _builder.Append(".get_path(), ");
      _builder.Append("'");
      _builder.Append(methodInfo.Name);
      _builder.Append("', false, ");
      _builder.Append('{');
      int i = 0;
      foreach (ParameterInfo pinfo in parameterList)
      {
        _builder.Append(pinfo.Name);
        _builder.Append(":");
        _builder.Append(pinfo.Name);
        if (i != (parameterList.Count - 1))
          _builder.Append(", ");
        i++;
      }
      _builder.Append("}");
      _builder.Append(", succeededCallback, failedCallback, userContext); " +
      "\r\n}\r\n");
    }
    private void GeneratePrototype()
    {
      this._builder.Append(_serviceFullName);
      this._builder.Append(".prototype");
      this._builder.Append(" = ");
      this._builder.Append("\r\n{");
      bool flag1 = true;
      foreach (MethodInfo methodInfo in _methodInfos)
      {
        if (!flag1)
          _builder.Append(",\r\n");
        flag1 = false;
        this.GenerateWebMethodProxy(methodInfo);
      }
      _builder.Append("}\r\n\r\n");
    }
    protected void GenerateRegisterClass()
    {
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".registerClass('");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("', Sys.Net.WebServiceProxy);\r\n");
    }
    protected void GenerateStaticInstance()
    {
      this._builder.Append(this._serviceFullName);
      this._builder.Append("._staticInstance = new ");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("();\r\n");
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".set_path = function(value) { ");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("._staticInstance._path = value; }\r\n");
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".get_path = function() { return ");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("._staticInstance._path; }\r\n");
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".set_timeout = function(value) { ");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("._staticInstance._timeout = value; }\r\n");
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".get_timeout = function() { return ");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("._staticInstance._timeout; }\r\n");
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".set_defaultUserContext = function(value) { ");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("._staticInstance._userContext = value; }\r\n");
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".get_defaultUserContext = function() { return ");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("._staticInstance._userContext; }\r\n");
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".set_defaultSucceededCallback = function(value) { ");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("._staticInstance._succeeded = value; }\r\n");
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".get_defaultSucceededCallback = function() { return ");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("._staticInstance._succeeded; }\r\n");
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".set_defaultFailedCallback = function(value) { ");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("._staticInstance._failed = value; }\r\n");
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".get_defaultFailedCallback = function() { return ");
      this._builder.Append(this._serviceFullName);
      this._builder.Append("._staticInstance._failed; }\r\n");
      this._builder.Append(this._serviceFullName);
      this._builder.Append(".set_path(\"");
      this._builder.Append(this._servicePath);
      this._builder.Append("\");\r\n");
    }
    protected void GenerateStaticMethods()
    {
      ArrayList parameterList;
      foreach (MethodInfo methodInfo in _methodInfos)
      {
        this._builder.Append(this._serviceFullName);
        this._builder.Append(".");
        this._builder.Append(methodInfo.Name);
        this._builder.Append(" = function(");
        parameterList = this._parameterInfos[methodInfo];
        foreach (ParameterInfo pinfo in parameterList)
        {
          _builder.Append(pinfo.Name);
          _builder.Append(',');
        }
        _builder.Append("onSuccess, onFailed, userContext) \r\n{\r\n\t");
        this._builder.Append(this._serviceFullName);
        this._builder.Append("._staticInstance.");
        this._builder.Append(methodInfo.Name);
        this._builder.Append("(");
        foreach (ParameterInfo pinfo in parameterList)
        {
          _builder.Append(pinfo.Name);
          _builder.Append(',');
        }
        _builder.Append("onSuccess, onFailed, userContext); \r\n}");
      }
    }
  }
}