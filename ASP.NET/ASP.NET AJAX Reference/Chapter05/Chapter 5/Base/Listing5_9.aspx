<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript" src="ShoppingCartApp1.js">
  </script>

  <script type="text/javascript" language="javascript">
    function pageLoad()
    {
      var shoppingCart = new Shopping.ShoppingCart();
      shoppingCart.initialize();
      var shoppingCartItem = new Shopping.ShoppingCartItem(1, "item1", 23);
      shoppingCart.addShoppingCartItem(shoppingCartItem);
      var shoppingCartItems = shoppingCart.get_shoppingCartItems();
      for (var id in shoppingCartItems)
      {
        alert(shoppingCartItems[id].get_name());
      }
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
