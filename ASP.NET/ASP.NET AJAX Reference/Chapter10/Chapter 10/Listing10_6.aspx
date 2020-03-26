<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
  <style type="text/css">
    .properties
    {
      background-color: LightGoldenrodYellow;
      color: black;
      border-collapse: collapse;
    }
    .properties td, .properties th
    {
      border: 1px solid Tan;
      padding: 5px;
    }
    .header
    {
      background-color: Tan;
    }
    .odd
    {
      background-color: PaleGoldenrod;
    }
  </style>

  <script type="text/javascript" language="javascript">
function displayMethods(instance)
{
var td = Sys.Preview.TypeDescriptor.getTypeDescriptor(instance);
var methods = td._getMethods();
var columns = ["Method Name", "Parameter (Name/Type)"];
var table = document.createElement("table");
Sys.UI.DomElement.addCssClass(table, "properties");
var headerRow = table.insertRow(0);
Sys.UI.DomElement.addCssClass(headerRow, "header");
var headerCell = null;
for (var i=0, length = columns.length; i<length; i++)
{
headerCell = document.createElement("th");
headerCell.appendChild(document.createTextNode(columns[i]));
headerRow.appendChild(headerCell);
}
for (var m in methods)
{
insertRow(table, methods[m]);
}
var container = $get("myDiv");
container.innerHTML = "";
container.appendChild(table);
}
function insertRow(table, method)
{
var rowIndex = table.rows.length;
var row = table.insertRow(rowIndex);
if (rowIndex % 2 == 1)
Sys.UI.DomElement.addCssClass(row, "odd");
insertCell(row, method["name"]);
var parametersText = "No parameters are defined!";
if (method["parameters"])
{
var parameters = method["parameters"];
var paramBuffer = [];
for(var parameter in parameters)
{
paramBuffer.push(String.format("({0} / {1})",
parameters[parameter].name,
parameters[parameter].type.getName()));
}
parametersText = paramBuffer.join();
}
insertCell(row, parametersText);
}
function insertCell(row, value)
{
var cell = row.insertCell(row.cells.length);
cell.appendChild(document.createTextNode(value));
}
function pageLoad()
{
var instance = new Sys.UI.Control($get("forControl"));
displayMethods(instance);
}
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
    </Scripts>
  </asp:ScriptManager>
  <div id="myDiv">
  </div>
  <div id="forControl">
  </div>
  </form>
</body>
</html>
