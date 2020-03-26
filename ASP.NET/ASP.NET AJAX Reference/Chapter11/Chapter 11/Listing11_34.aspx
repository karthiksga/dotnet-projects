<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function pageLoad()
    {
      var dataColumns = [];
      dataColumns[dataColumns.length] = new Sys.Preview.Data.DataColumn('ProductId', Number, 1, true, true);
      dataColumns[dataColumns.length] = new Sys.Preview.Data.DataColumn('ProductName', String, 'Unknown', true, false);
      dataColumns[dataColumns.length] = new Sys.Preview.Data.DataColumn('UnitPrice', Number, 50, true, false);
      
      var dataTable = new Sys.Preview.Data.DataTable(dataColumns);
      
      var rowObject = {'ProductId': 1, 'ProductName': 'Product1', 'UnitPrice': 60};
      var dataRow = dataTable.createRow(rowObject);
      dataTable.add(dataRow);
      
      rowObject = {'ProductId': 2, 'ProductName': 'Product2', 'UnitPrice': 40};
      dataRow = dataTable.createRow(rowObject);
      dataTable.add(dataRow);
      
      rowObject = {'ProductId': 3, 'ProductName': 'Product3', 'UnitPrice': 20};
      dataRow = dataTable.createRow(rowObject);
      dataTable.add(dataRow);
      
      var customTable = new CustomComponents.CustomTable($get("myDiv"));
      var dataFieldNames = ['ProductName', 'UnitPrice'];
      customTable.set_dataFieldNames(dataFieldNames);
      customTable.set_dataSource(dataTable);
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
    </Scripts>
  </asp:ScriptManager>
  <div id="myDiv">
  </div>
  </form>
</body>
</html>
