Type.registerNamespace("CustomComponents");
CustomComponents.CustomTable =
function CustomComponents$CustomTable(associatedElement) {
    CustomComponents.CustomTable.initializeBase(this, [associatedElement]);
}
function CustomComponents$CustomTable$get_dataSource() {
    return this._dataSource;
}
function CustomComponents$CustomTable$set_dataSource(value) {
    this._dataSource = value;
}
function CustomComponents$CustomTable$dataBind() {
    var sb = new Sys.StringBuilder('<table align="center" id="products" ');
    sb.append('style="background-color:LightGoldenrodYellow;');
    sb.append('border-color:Tan;border-width:1px; color:Black"');
    sb.append(' cellpadding="5">');
    var propertyNames = [];
    for (var i = 0; i < this._dataSource.length; i++) {
        var dataItem = this._dataSource[i];
        if (i == 0) {
            var td = Sys.Preview.TypeDescriptor.getTypeDescriptor(dataItem);
            var properties = td._getProperties();
            sb.append('<tr style="background-color:Tan; font-weight:bold">');
            for (var c in properties) {
                var propertyObj = properties[c];
                var propertyName = propertyObj.name;
                propertyNames[propertyNames.length] = propertyName;
                sb.append('<td>');
                sb.append(propertyName);
                sb.append('</td>');
            }
            sb.append('</tr>');
        }
        if (i % 2 == 1)
            sb.append('<tr style="background-color:PaleGoldenrod">');
        else
            sb.append('<tr>');
        for (var j in propertyNames) {
            var propertyName = propertyNames[j];
            var propertyValue = Sys.Preview.TypeDescriptor.getProperty(dataItem, propertyName, null);
            var typeName = Object.getTypeName(propertyValue);
            if (typeName !== 'String' && typeName !== 'Number' && typeName !== 'Boolean') {
                var convertToStringMethodName = Sys.Preview.TypeDescriptor.getAttribute(propertyValue,"convertToStringMethodName");
                if (convertToStringMethodName)
                    propertyValue = Sys.Preview.TypeDescriptor.invokeMethod(propertyValue,convertToStringMethodName, null);
            }
            sb.append('<td>')
            sb.append(propertyValue);
            sb.append('</td>');
        }
        sb.append('</tr>');
    }
    sb.append('</table>');
    this.get_element().innerHTML = sb.toString();
}
CustomComponents.CustomTable.prototype =
{
    get_dataSource: CustomComponents$CustomTable$get_dataSource,
    set_dataSource: CustomComponents$CustomTable$set_dataSource,
    dataBind: CustomComponents$CustomTable$dataBind
}
CustomComponents.CustomTable.registerClass("CustomComponents.CustomTable",Sys.UI.Control);
if (typeof (Sys) !== 'undefined')
    Sys.Application.notifyScriptLoaded();