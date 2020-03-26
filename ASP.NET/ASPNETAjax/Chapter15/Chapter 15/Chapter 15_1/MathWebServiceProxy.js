/// <reference name="MicrosoftAjax.js"/>
Type.registerNamespace('MyNamespace');
MyNamespace.Math = function()
{
  MyNamespace.Math.initializeBase(this);
  this._timeout = 0;
  this._userContext = null;
  this._succeeded = null;
  this._failed = null;
}

MyNamespace.Math.prototype =
{
  Add : function(x, y, succeededCallback, failedCallback, userContext)
        {
          var servicePath = MyNamespace.Math.get_path();
          var methodName = 'Add';
          var useGet = false;
          var params = {x : x, y : y};
          var onSuccess = succeededCallback;
          var onFailure = failedCallback;
          return this._invoke(servicePath, methodName, useGet, params,
                              onSuccess,onFailure, userContext);
        }
}

MyNamespace.Math.registerClass('MyNamespace.Math', Sys.Net.WebServiceProxy);
MyNamespace.Math._staticInstance = new MyNamespace.Math();

MyNamespace.Math.set_path = function(value)
{
  MyNamespace.Math._staticInstance._path = value;
}

MyNamespace.Math.get_path = function()
{
  return MyNamespace.Math._staticInstance._path;
}

MyNamespace.Math.set_timeout = function(value)
{
  MyNamespace.Math._staticInstance._timeout = value;
}

MyNamespace.Math.get_timeout = function()
{
  return MyNamespace.Math._staticInstance._timeout;
}

MyNamespace.Math.set_defaultUserContext = function(value)
{
  MyNamespace.Math._staticInstance._userContext = value;
}

MyNamespace.Math.get_defaultUserContext = function()
{
  return MyNamespace.Math._staticInstance._userContext;
}

MyNamespace.Math.set_defaultSucceededCallback = function(value)
{
  MyNamespace.Math._staticInstance._succeeded = value;
}

MyNamespace.Math.get_defaultSucceededCallback = function()
{
  return MyNamespace.Math._staticInstance._succeeded;
}

MyNamespace.Math.set_defaultFailedCallback = function(value)
{
  MyNamespace.Math._staticInstance._failed = value;
}

MyNamespace.Math.get_defaultFailedCallback = function()
{
  return MyNamespace.Math._staticInstance._failed;
}

MyNamespace.Math.set_path("Math.asmx");
MyNamespace.Math.Add = function(x, y, onSuccess, onFailed, userContext)
{
  MyNamespace.Math._staticInstance.Add(x, y, onSuccess, onFailed, userContext);
}

