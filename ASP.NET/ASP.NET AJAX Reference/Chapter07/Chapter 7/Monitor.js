Type.registerNamespace("Disposables");
Disposables.Monitor = function()
{
this.div = document.createElement("div");
document.body.insertBefore(this.div,document.forms[0]);
this.registerMonitor();
}
Disposables.Monitor.prototype =
{
registerMonitor : function()
{
this.delegate = Function.createDelegate(this, this.print);
$addHandler(document, "mousemove", this.delegate);
},
print : function(domEvent)
{
this.div.innerHTML = "X-Coordinate: " + domEvent.clientX + "<br/>" +
"Y-Coordinate: " + domEvent.clientY;
},
dispose : function()
{
$removeHandler(document, "mousemove", this.delegate);
}
}
Disposables.Monitor.registerClass("Disposables.Monitor", null, Sys.IDisposable);
if(typeof(Sys) !== 'undefine')
Sys.Application.notifyScriptLoaded();