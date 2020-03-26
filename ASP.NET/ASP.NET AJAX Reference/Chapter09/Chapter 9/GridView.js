Type.registerNamespace("CustomComponents");
CustomComponents.GridView =
function CustomComponents$GridView(associatedElement)
{
CustomComponents.GridView.initializeBase(this, [associatedElement]);
}
function CustomComponents$GridView$onBubbleEvent(source, args)
{
var handled = false;
if (args instanceof Sys.Preview.UI.CommandEventArgs)
{
switch (args.get_commandName())
{
case "Select":
alert(args.get_argument() + " is selected!");
handled = true;
break;
case "Delete":
alert(args.get_argument() + " is deleted!");
handled = true;
break;
}
}
return handled;
}
CustomComponents.GridView.prototype =
{
onBubbleEvent : CustomComponents$GridView$onBubbleEvent
}
CustomComponents.GridView.registerClass("CustomComponents.GridView",
Sys.UI.Control);
if(typeof(Sys) !== 'undefined')
Sys.Application.notifyScriptLoaded();