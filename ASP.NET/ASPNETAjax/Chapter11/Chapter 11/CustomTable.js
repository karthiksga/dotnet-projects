Type.registerNamespace("CustomComponents");

CustomComponents.CustomTable = function CustomComponents$CustomTable(associatedElement)
{
  CustomComponents.CustomTable.initializeBase(this, [associatedElement]);
}

function CustomComponents$CustomTable$get_dataSource()
{
  return this._dataSource;
}

function CustomComponents$CustomTable$set_dataSource(value)
{
  this._dataSource = value;
}

function CustomComponents$CustomTable$set_dataFieldNames(value)
{
  this._dataFieldNames = value;
}

function CustomComponents$CustomTable$get_dataFieldNames()
{
  return this._dataFieldNames;
}

function CustomComponents$CustomTable$dataBind()
{
  var isArray = true;
  if (this._dataSource && Sys.Preview.Data.IData.isImplementedBy(this._dataSource))
    isArray = false;
    
  else if (Array.isInstanceOfType(this._dataSource))
    throw Error.createError('Unknown data source type!');
    
  var sb = new Sys.StringBuilder('<table align="center" id="products" ');
  sb.append('style="background-color:LightGoldenrodYellow;');
  sb.append('border-color:Tan;border-width:1px; color:Black"');
  sb.append(' cellpadding="5">');
  var propertyNames = [];
  var length = isArray ? this._dataSource.length : this._dataSource.get_length();
  
  for (var i=0; i<length; i++)
  {
    var dataItem = isArray ? this._dataSource[i] : this._dataSource.getRow(i);
    if (i == 0)
    {
      sb.append('<tr style="background-color:Tan; font-weight:bold">');
      for (var c in this._dataFieldNames)
      {
        sb.append('<td>');
        sb.append(this._dataFieldNames[c]);
        sb.append('</td>');
      }
      sb.append('</tr>');
    }
    
    if (i % 2 == 1)
      sb.append('<tr style="background-color:PaleGoldenrod">');
    else
      sb.append('<tr>');
      
    for (var j in this._dataFieldNames)
    {
      var dataFieldName = this._dataFieldNames[j];
      var dataFieldValue = Sys.Preview.TypeDescriptor.getProperty(dataItem, dataFieldName, null);
      var typeName = Object.getTypeName(dataFieldValue);
      if (typeName !== 'String' && typeName !== 'Number' && typeName !== 'Boolean')
      {
        var convertToStringMethodName = Sys.Preview.TypeDescriptor.getAttribute(dataFieldValue, "convertToStringMethodName");
        if (convertToStringMethodName)
          dataFieldValue = Sys.Preview.TypeDescriptor.invokeMethod(dataFieldValue, convertToStringMethodName);
      }
      sb.append('<td>')
      sb.append(dataFieldValue);
      sb.append('</td>');
    }
    sb.append('</tr>');
  }
  
  sb.append('</table>');
  this.get_element().innerHTML = sb.toString();
}

CustomComponents.CustomTable.prototype =
{
  get_dataSource : CustomComponents$CustomTable$get_dataSource,
  set_dataSource : CustomComponents$CustomTable$set_dataSource,
  get_dataFieldNames : CustomComponents$CustomTable$get_dataFieldNames,
  set_dataFieldNames : CustomComponents$CustomTable$set_dataFieldNames,
  dataBind : CustomComponents$CustomTable$dataBind
}

CustomComponents.CustomTable.registerClass("CustomComponents.CustomTable", Sys.UI.Control);

if(typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();