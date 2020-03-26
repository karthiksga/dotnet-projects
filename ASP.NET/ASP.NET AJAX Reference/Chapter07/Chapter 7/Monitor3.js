Type.registerNamespace("Disposables");
Disposables.Monitor = function()
{
this.id="Monitor1";
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
this.div.innerHTML = "Monitor id: " + this.get_id() + "<br/>" +
"X-Coordinate: " + domEvent.clientX + "<br/>" +
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
},
add_propertyChanged : function(handler)
{
this.get_events().addHandler("propertyChanged", handler);
},
remove_propertyChanged : function(handler)
{
this.get_events().removeHandler("propertyChanged", handler);
},
raisePropertyChanged : function (propertyName)
{
if (!this.events)
return;
var handler = this.events.getHandler("propertyChanged");
if (handler)
handler(this, new Sys.PropertyChangedEventArgs(propertyName));
},
get_id : function()
{
return this.id;
},
set_id : function(value)
{
this.id = value;
this.raisePropertyChanged("id");
}
}
Disposables.Monitor.registerClass("Disposables.Monitor", null, Sys.IDisposable,
Sys.INotifyDisposing, Sys.INotifyPropertyChange);
if(typeof(Sys) !== 'undefined')
Sys.Application.notifyScriptLoaded();