Type.registerNamespace("CustomComponents");
CustomComponents.GridViewRow =
function CustomComponents$GridViewRow(associatedElement)
{
CustomComponents.GridViewRow.initializeBase(this, [associatedElement]);
}
function CustomComponents$GridViewRow$set_rowIndex(value)
{
if (this._rowIndexSet)
throw Error.invalidOperation("rowIndex property cannot be set twice!");
this._rowIndexSet = true;
this._rowIndex = value;
}
function CustomComponents$GridViewRow$get_rowIndex()
{
return this._rowIndex;
}
function CustomComponents$GridViewRow$onBubbleEvent(source, args)
{
var handled = false;
if (args instanceof Sys.Preview.UI.CommandEventArgs)
{
var args2=new CustomComponents.GridViewCommandEventArgs(this, source, args);
this.raiseBubbleEvent(this, args2);
handled = true;
}
return handled;
}
CustomComponents.GridViewRow.prototype =
{
get_rowIndex : CustomComponents$GridViewRow$get_rowIndex,
set_rowIndex : CustomComponents$GridViewRow$set_rowIndex,
onBubbleEvent : CustomComponents$GridViewRow$onBubbleEvent
}
CustomComponents.GridViewRow.descriptor =
{
properties : [{name : 'rowIndex', type : Number}]
};
CustomComponents.GridViewRow.registerClass( "CustomComponents.GridViewRow",
Sys.UI.Control);
if(typeof(Sys) !== 'undefined')
Sys.Application.notifyScriptLoaded();