Type.registerNamespace("CustomComponents");
CustomComponents.GridView =
function Sys$Preview$UI$GridView(associatedElement)
{
CustomComponents.GridView.initializeBase(this, [associatedElement]);
}
function CustomComponents$GridView$onBubbleEvent(source, args)
{
var handled = false;
if (args instanceof CustomComponents.GridViewCommandEventArgs)
{
switch (args.get_commandName())
{
case "Select":
alert(args.get_argument() + " from row number " +
args.get_row().get_rowIndex() + " is selected!");
handled = true;
break;
case "Delete":
alert(args.get_argument() + " from row number " +
args.get_row().get_rowIndex() + " is deleted!");
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