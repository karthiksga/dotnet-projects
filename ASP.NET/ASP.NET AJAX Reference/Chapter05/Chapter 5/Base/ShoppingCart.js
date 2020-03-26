Type.registerNamespace("Shopping");

Shopping.ShoppingCartItem = function Shopping$ShoppingCartItem(id, name, price)
{
  this.id = id;
  this.name = name;
  this.price = price;
}

function Shopping$ShoppingCartItem$get_id()
{
  return this.id;
}

function Shopping$ShoppingCartItem$get_name()
{
  return this.name;
}

function Shopping$ShoppingCartItem$get_price()
{
  return this.price;
}

Shopping.ShoppingCartItem.prototype =
{
  get_id : Shopping$ShoppingCartItem$get_id,
  get_name : Shopping$ShoppingCartItem$get_name,
  get_price : Shopping$ShoppingCartItem$get_price
};

Shopping.ShoppingCartItem.registerClass("Shopping.ShoppingCartItem");
Shopping.ShoppingCart = function() {
}

function Shopping$ShoppingCart$initialize()
{
  this.shoppingCartItems = {};
}

function Shopping$ShoppingCart$get_shoppingCartItems()
{
  return this.shoppingCartItems;
}

function Shopping$ShoppingCart$addShoppingCartItem(shoppingCartItem)
{
  var cartItems = this.get_shoppingCartItems();
  var cartItemId = shoppingCartItem.get_id();
  if (cartItems[cartItemId])
  {
    var exception = Error.duplicateItem("Duplicate Shopping Cart Item!", {name: shoppingCartItem.get_name()});
    throw exception;
  }
  else
    this.shoppingCartItems[cartItemId] = shoppingCartItem;
}

Shopping.ShoppingCart.prototype = {
  addShoppingCartItem : Shopping$ShoppingCart$addShoppingCartItem,
  initialize : Shopping$ShoppingCart$initialize,
  get_shoppingCartItems : Shopping$ShoppingCart$get_shoppingCartItems
};

Shopping.ShoppingCart.registerClass("Shopping.ShoppingCart");
if(typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();