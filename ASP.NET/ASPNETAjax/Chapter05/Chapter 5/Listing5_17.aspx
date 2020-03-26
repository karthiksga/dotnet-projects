<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function shoppingCartInitializedCallback(sender, e)
    {
      alert("Shopping cart is initialized!");
    }
    
    function shoppingCartItemAddingCallback(sender, e)
    {
        e.set_cancel(false);     
        alert("Adding " + e.get_shoppingCartItem().get_name());
    }
    
    function shoppingCartItemAddedCallback(sender, e)
    {
      alert("Added " + e.get_shoppingCartItem().get_name());
      if (e.get_exception())
        alert(e.get_exception());
    }
    
    function pageLoad()
    {
      var shoppingCart = new Shopping.ShoppingCart();
      shoppingCart.add_shoppingCartInitialized(shoppingCartInitializedCallback);
      shoppingCart.add_shoppingCartItemAdding(shoppingCartItemAddingCallback);
      shoppingCart.add_shoppingCartItemAdded(shoppingCartItemAddedCallback);
      shoppingCart.initialize();
      var shoppingCartItem = new Shopping.ShoppingCartItem(1, "item1", 23);
      shoppingCart.addShoppingCartItem(shoppingCartItem);
      shoppingCart.remove_shoppingCartInitialized(shoppingCartInitializedCallback);
      shoppingCart.remove_shoppingCartItemAdding(shoppingCartItemAddingCallback);
      shoppingCart.remove_shoppingCartItemAdded(shoppingCartItemAddedCallback);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScripManager1">
    <Scripts>
      <asp:ScriptReference Path="ShoppingCart.js" />
    </Scripts>
  </asp:ScriptManager>
  </form>
</body>
</html>
