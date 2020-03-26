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

Shopping.ShoppingCartItem.prototype = {
  get_id : Shopping$ShoppingCartItem$get_id,
  get_name : Shopping$ShoppingCartItem$get_name,
  get_price : Shopping$ShoppingCartItem$get_price
};

Shopping.ShoppingCartItem.registerClass("Shopping.ShoppingCartItem");
Shopping.ShoppingCart = function() { }
function Shopping$ShoppingCart$get_events() {
  if (!this.events)
    this.events = new Sys.EventHandlerList();
  return this.events;
}

function Shopping$ShoppingCart$initialize()
{
  this.shoppingCartItems = {};
  this.onShoppingCartInitialized(Sys.EventArgs.Empty);
}

function Shopping$ShoppingCart$onShoppingCartInitialized(e)
{
  var handler = this.get_events().getHandler("shoppingCartInitialized");
  if (handler)
    handler(this, e);
}

function Shopping$ShoppingCart$addShoppingCartItem(shoppingCartItem)
{
  var e1 = new Shopping.ShoppingCartItemAddingEventArgs(shoppingCartItem);
  this.onShoppingCartItemAdding(e1);
  if (!e1.get_cancel())
  {
    var exception = null;
    var cartItems = this.get_shoppingCartItems();
    var cartItemId = shoppingCartItem.get_id();
    if (cartItems[cartItemId])
      exception = Error.duplicateItem("Duplicate Shopping Cart Item!", {name: shoppingCartItem.get_name()});
    else
      this.shoppingCartItems[cartItemId] = shoppingCartItem;
    var e2 =
      new Shopping.ShoppingCartItemAddedEventArgs(shoppingCartItem, exception);
    this.onShoppingCartItemAdded(e2);
    if (!e2.get_exceptionHandled())
      throw exception;
  }
}

function Shopping$ShoppingCart$onShoppingCartItemAdding(e)
{
  var handler = this.get_events().getHandler("shoppingCartItemAdding");
  if (handler)
    handler(this, e);
}

function Shopping$ShoppingCart$onShoppingCartItemAdded(e)
{
  var handler = this.get_events().getHandler("shoppingCartItemAdded");
  if (handler)
    handler(this, e);
}

function Shopping$ShoppingCart$add_shoppingCartInitialized(handler)
{
  this.get_events().addHandler("shoppingCartInitialized", handler);
}

function Shopping$ShoppingCart$add_shoppingCartItemAdding(handler)
{
  this.get_events().addHandler("shoppingCartItemAdding", handler);
}

function Shopping$ShoppingCart$add_shoppingCartItemAdded(handler)
{
  this.get_events().addHandler("shoppingCartItemAdded", handler);
}

function Shopping$ShoppingCart$remove_shoppingCartInitialized(handler)
{
  this.get_events().removeHandler("shoppingCartInitialized", handler);
}

function Shopping$ShoppingCart$remove_shoppingCartItemAdding(handler)
{
  this.get_events().removeHandler("shoppingCartItemAdding", handler);
}

function Shopping$ShoppingCart$remove_shoppingCartItemAdded(handler)
{
  this.get_events().removeHandler("shoppingCartItemAdded", handler);
}

function Shopping$ShoppingCart$get_shoppingCartItems()
{
  return this.shoppingCartItems;
}

Shopping.ShoppingCart.prototype = {
  addShoppingCartItem : Shopping$ShoppingCart$addShoppingCartItem,
  initialize : Shopping$ShoppingCart$initialize,
  get_shoppingCartItems : Shopping$ShoppingCart$get_shoppingCartItems,
  get_events : Shopping$ShoppingCart$get_events,
  add_shoppingCartInitialized : Shopping$ShoppingCart$add_shoppingCartInitialized,
  remove_shoppingCartInitialized : Shopping$ShoppingCart$remove_shoppingCartInitialized,
  onShoppingCartInitialized : Shopping$ShoppingCart$onShoppingCartInitialized,
  add_shoppingCartItemAdding : Shopping$ShoppingCart$add_shoppingCartItemAdding,
  remove_shoppingCartItemAdding: Shopping$ShoppingCart$remove_shoppingCartItemAdding,
  onShoppingCartItemAdding : Shopping$ShoppingCart$onShoppingCartItemAdding,
  add_shoppingCartItemAdded : Shopping$ShoppingCart$add_shoppingCartItemAdded,
  remove_shoppingCartItemAdded: Shopping$ShoppingCart$remove_shoppingCartItemAdded,
  onShoppingCartItemAdded : Shopping$ShoppingCart$onShoppingCartItemAdded
};

Shopping.ShoppingCart.registerClass("Shopping.ShoppingCart");

Shopping.ShoppingCartItemAddingEventArgs =
function Shopping$ShoppingCartItemAddingEventArgs (shoppingCartItem)
{
  Shopping.ShoppingCartItemAddingEventArgs.initializeBase(this);
  this.shoppingCartItem = shoppingCartItem;
}

function Shopping$ShoppingCartItemAddingEventArgs$get_shoppingCartItem()
{
  return this.shoppingCartItem;
}

Shopping.ShoppingCartItemAddingEventArgs.prototype = {
  get_shoppingCartItem : Shopping$ShoppingCartItemAddingEventArgs$get_shoppingCartItem
};

Shopping.ShoppingCartItemAddingEventArgs.registerClass("Shopping.ShoppingCartItemAddingEventArgs", Sys.CancelEventArgs);
Shopping.ShoppingCartItemAddedEventArgs =
function Shopping$ShoppingCartItemAddedEventArgs (shoppingCartItem, exception)
{
  Shopping.ShoppingCartItemAddedEventArgs.initializeBase(this);
  this.shoppingCartItem = shoppingCartItem;
  this.exception = exception;
  this.exceptionHandled = false;
}

function Shopping$ShoppingCartItemAddedEventArgs$get_shoppingCartItem()
{
  return this.shoppingCartItem;
}

function Shopping$ShoppingCartItemAddedEventArgs$get_exception()
{
  return this.exception;
}

function Shopping$ShoppingCartItemAddedEventArgs$get_exceptionHandled()
{
  return !this.exception || this.exceptionHandled;
}

function Shopping$ShoppingCartItemAddedEventArgs$set_exceptionHandled(value)
{
  this.exceptionHandled = value;
}

Shopping.ShoppingCartItemAddedEventArgs.prototype = {
  get_shoppingCartItem : Shopping$ShoppingCartItemAddedEventArgs$get_shoppingCartItem,
  get_exception : Shopping$ShoppingCartItemAddedEventArgs$get_exception,
  get_exceptionHandled : Shopping$ShoppingCartItemAddedEventArgs$get_exceptionHandled,
  set_exceptionHandled : Shopping$ShoppingCartItemAddedEventArgs$set_exceptionHandled
};

Shopping.ShoppingCartItemAddedEventArgs.registerClass("Shopping.ShoppingCartItemAddedEventArgs", Sys.EventArgs);

if(typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();