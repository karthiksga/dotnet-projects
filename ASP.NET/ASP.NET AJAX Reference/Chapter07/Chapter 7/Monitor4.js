Type.registerNamespace("CustomComponents");
CustomComponents.Monitor = function()
{
CustomComponents.Monitor.initializeBase(this);
}
CustomComponents.Monitor.prototype =
{
print : function(domEvent)
{
this.div.innerHTML = String.format(this.printFormat, this.get_id(),
domEvent.clientX, domEvent.clientY)
},
dispose : function()
{
$removeHandler(document, "mousemove", this.delegate);
CustomComponents.Monitor.callBaseMethod(this, "dispose");
},
set_fontSize : function(value)
{
if (value != this.fontSize)
{
this.raisePropertyChanged("fontSize");
this.fontSize = value;
this.div.style.fontSize = this.fontSize + "px";
}
},
get_fontSize : function()
{
return this.fontSize;
},
initialize : function()
{
CustomComponents.Monitor.callBaseMethod(this, "initialize");
this.printFormat = "Monitor id: {0} <br />X-Coordinate: {1}" +
"<br />Y-Coordinate: {2}";
this.div = document.createElement("div");
document.body.insertBefore(this.div,document.forms[0]);
this.delegate = Function.createDelegate(this, this.print);
$addHandler(document, "mousemove", this.delegate);
}
}
CustomComponents.Monitor.registerClass("CustomComponents.Monitor", Sys.Component);
if(typeof(Sys) !== 'undefined')
Sys.Application.notifyScriptLoaded();