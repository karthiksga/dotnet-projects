Type.registerNamespace("CustomComponents");
CustomComponents.Product = function CustomComponents$Product(productName,
distributorName, distributorAddress)
{
this._productName = productName;
this._distributorName = distributorName;
this._distributorAddress = distributorAddress;
}
function CustomComponents$Product$get_productName()
{
return this._productName;
}
function CustomComponents$Product$get_distributorName()
{
return this._distributorName;
}
function CustomComponents$Product$get_distributorAddress()
{
return this._distributorAddress;
}
CustomComponents.Product.prototype =
{
get_productName : CustomComponents$Product$get_productName,
get_distributorName : CustomComponents$Product$get_distributorName,
get_distributorAddress : CustomComponents$Product$get_distributorAddress
}
CustomComponents.Product.descriptor =
{
properties : [{name : 'productName', type : String, readOnly : true},
{name : 'distributorName', type : String,
readOnly : true},
{name : 'distributorAddress',
type : CustomComponents.Address, readOnly : true}]
}
CustomComponents.Product.registerClass("CustomComponents.Product");
if(typeof(Sys) !== 'undefined')
Sys.Application.notifyScriptLoaded();