/// <reference name="MicrosoftAjax.js"/>
PageMethods = function()
{
  PageMethods.initializeBase(this);
  this._timeout = 0;
  this._userContext = null;
  this._succeeded = null;
  this._failed = null;
}

PageMethods.prototype =
{
  Add : function(x, y, succeededCallback, failedCallback, userContext) 
        {
          return this._invoke(PageMethods.get_path(), 'Add', false, {x:x, y:y},
          succeededCallback, failedCallback, userContext);
        }
}

PageMethods.registerClass('PageMethods', Sys.Net.WebServiceProxy);
PageMethods._staticInstance = new PageMethods();

PageMethods.set_path = function(value)
{
  PageMethods._staticInstance._path = value;
}

PageMethods.get_path = function()
{
  return PageMethods._staticInstance._path;
}

PageMethods.set_timeout = function(value)
{
  PageMethods._staticInstance._timeout = value;
}

PageMethods.get_timeout = function()
{
  return PageMethods._staticInstance._timeout;
}

PageMethods.set_defaultUserContext = function(value)
{
  PageMethods._staticInstance._userContext = value;
}

PageMethods.get_defaultUserContext = function()
{
  return PageMethods._staticInstance._userContext;
}

PageMethods.set_defaultSucceededCallback = function(value)
{
  PageMethods._staticInstance._succeeded = value;
}

PageMethods.get_defaultSucceededCallback = function()
{
  return PageMethods._staticInstance._succeeded;
}

PageMethods.set_defaultFailedCallback = function(value)
{
  PageMethods._staticInstance._failed = value;
}

PageMethods.get_defaultFailedCallback = function()
{
  return PageMethods._staticInstance._failed;
}

PageMethods.set_path("PageMethods.aspx");
PageMethods.Add = function(x, y, onSuccess, onFailed, userContext)
{
  PageMethods._staticInstance.Add(x, y, onSuccess, onFailed, userContext);
};