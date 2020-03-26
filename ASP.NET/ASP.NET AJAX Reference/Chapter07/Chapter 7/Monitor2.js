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
if (this.events)
{
var handler = this.events.getHandler("disposing");
if (handler)
handler(this, Sys.EventArgs.Empty);
}
delete this.events;
$removeHandler(document, "mousemove", this.delegate);
},
get_events : function()
{
if (!this.events)
this.events = new Sys.EventHandlerList();
return this.events;
},
add_disposing : function(handler)
{
this.get_events().addHandler("disposing", handler);
},
remove_disposing : function(handler)
{
this.get_events().removeHandler("disposing", handler);
}
}
Disposables.Monitor.registerClass("Disposables.Monitor", null, Sys.IDisposable,
Sys.INotifyDisposing);
if(typeof(Sys) !== 'undefined')
Sys.Application.notifyScriptLoaded();
