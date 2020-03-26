<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function pageLoad()
    {
      var dataRow =
      new Sys.Preview.Data.DataRow({productName: 'p1', unitPrice: 30, distributor: 'd1'});
      alert ("Product Name: " + dataRow.getProperty("productName") +
             "\nUnit Price: " + dataRow.getProperty("unitPrice") +
             "\nDistributor: " + dataRow.getProperty("distributor"));
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="Server">
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
    </Scripts>
  </asp:ScriptManager>
  </form>
</body>
</html>
