<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
function pageLoad()
{
$create(CustomComponents.GridView, null, null, null, $get("products"));
$create(CustomComponents.GridViewRow,
{rowIndex: 1}, null, {parent: "products"}, $get("row1"));
$create(Sys.Preview.UI.Button,
{ command: "Select", argument: "Product1" }, null,
{ parent: "row1"},
$get("product1Selectbtn1"));
$create(Sys.Preview.UI.Button,
{ command: "Delete", argument: "Product1" },
null,
{ parent: "row1"},
$get("product1Deletebtn1"));
$create(CustomComponents.GridViewRow,
{rowIndex: 2}, null, {parent: "products"}, $get("row2"));
$create(Sys.Preview.UI.Button,
{ command: "Select", argument: "Product2" }, null,
{ parent: "row2"},
$get("product2Selectbtn1"));
$create(Sys.Preview.UI.Button,
{ command: "Delete", argument: "Product2" }, null,
{ parent: "row2"},
$get("product2Deletebtn1"));
}
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
      <asp:ScriptReference Path="GridViewCommandEventArgs.js" />
      <asp:ScriptReference Path="GridViewRow.js" />
      <asp:ScriptReference Path="GridView2.js" />
    </Scripts>
  </asp:ScriptManager>
  <table id="products" style="background-color: LightGoldenrodYellow; border-color: Tan;
    border-width: 1px; color: Black" cellpadding="0">
    <tr style="background-color: Tan; font-weight: bold">
      <th>
        Product Name</th>
      <th>
        Unit Price</th>
    </tr>
    <tr id="row1">
      <td>
        Product1</td>
      <td>
        $100</td>
      <td>
        <button id="product1Selectbtn1">
          Select</button></td>
      <td>
        <button id="product1Deletebtn1">
          Delete</button></td>
    </tr>
    <tr id="row2" style="background-color: PaleGoldenrod">
      <td>
        Product2</td>
      <td>
        $200</td>
      <td>
        <button id="product2Selectbtn1">
          Select</button></td>
      <td>
        <button id="product2Deletebtn1">
          Delete</button></td>
    </tr>
  </table>
  </form>
</body>
</html>
