<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function pageLoad()
    {
      var products = [];
      var product;
      var distributoraddress;
      for (var i=0; i<4; i++)
      {
        distributoraddress =new CustomComponents.Address("street"+i, "city"+i, "state"+i, "zip"+i);
        product = new CustomComponents.Product("Product"+i, "Distributor"+i,distributoraddress);
        products[i] = product;
      }
      var customTable = $create(CustomComponents.CustomTable,{dataSource : products}, null, null,$get("myDiv"));
      customTable.dataBind();
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
      <asp:ScriptReference Path="CustomTable.js" />
      <asp:ScriptReference Path="Address.js" />
      <asp:ScriptReference Path="Product.js" />
    </Scripts>
  </asp:ScriptManager>
  <div id="myDiv">
  </div>
  </form>
</body>
</html>
