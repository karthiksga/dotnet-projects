Type.registerNamespace("CustomComponents");
CustomComponents.GridViewCommandEventArgs =
function CustomComponents$GridViewCommandEventArgs(row, source, args) {
    CustomComponents.GridViewCommandEventArgs.initializeBase(this,
[args.get_commandName(), args.get_argument()]);
    this._commandSource = source;
    this._row = row;
}
function CustomComponents$GridViewCommandEventArgs$get_commandSource() {
    return this._commandSource;
}
function CustomComponents$GridViewCommandEventArgs$get_row() {
    return this._row;
}
CustomComponents.GridViewCommandEventArgs.prototype =
{
    get_commandSource: CustomComponents$GridViewCommandEventArgs$get_commandSource,
    get_row: CustomComponents$GridViewCommandEventArgs$get_row
};
CustomComponents.GridViewCommandEventArgs.descriptor =
{
    properties: [{ name: 'commandSource', type: Sys.Preview.UI.Control, readOnly: true },
{ name: 'row', type: CustomComponents.GridViewRow, readOnly: true}]
}
CustomComponents.GridViewCommandEventArgs.registerClass(
"CustomComponents.GridViewCommandEventArgs",
Sys.Preview.UI.CommandEventArgs);
if (typeof (Sys) !== 'undefined')
    Sys.Application.notifyScriptLoaded();