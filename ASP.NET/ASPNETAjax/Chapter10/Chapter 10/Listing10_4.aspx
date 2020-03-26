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
        function displayProperties(instance) {
            var td = Sys.Preview.TypeDescriptor.getTypeDescriptor(instance);
            var properties = td._getProperties();
            var columns = ["Property Name", "Property Type", "ReadOnly", "Property Attributes (Name/Value)"];
            var table = document.createElement("table");
            Sys.UI.DomElement.addCssClass(table, "properties");
            var headerRow = table.insertRow(0);
            Sys.UI.DomElement.addCssClass(headerRow, "header");
            var headerCell = null;

            for (var i = 0, length = columns.length; i < length; i++) {
                headerCell = document.createElement("th");
                headerCell.appendChild(document.createTextNode(columns[i]));
                headerRow.appendChild(headerCell);
            }

            for (var property in properties) {
                insertRow(table, properties[property]);
            }

            var container = $get("myDiv");
            container.innerHTML = "";
            container.appendChild(table);
        }

        function insertRow(table, property) {
            var rowIndex = table.rows.length;
            var row = table.insertRow(rowIndex);
            if (rowIndex % 2 == 1)
                Sys.UI.DomElement.addCssClass(row, "odd");
            insertCell(row, property["name"]);
            insertCell(row, property["type"]);
            insertCell(row, property["readOnly"]);
            var attributesText = "No attributes are defined!";
            if (property["attributes"]) {
                var attributes = property["attributes"];
                var attrBuffer = [];
                for (var attribute in attributes) {
                    attrBuffer.push(String.format("{0}={1}", attribute, attributes[attribute]));
                }
                attributesText = attrBuffer.join();
            }
            insertCell(row, attributesText);
        }

        function insertCell(row, value) {
            var cell = row.insertCell(row.cells.length);
            cell.appendChild(document.createTextNode(value));
        }

        function pageLoad() {
            var instance = new Sys.UI.Control($get("forControl"));
            displayProperties(instance);
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
    <div id="myDiv" />
    <div id="forControl" />
    </form>
</body>
</html>
