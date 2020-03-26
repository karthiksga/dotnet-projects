<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
  <style type="text/css">
    .myCssClass
    {
      background-color: #dddddd;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
    </Scripts>
  </asp:ScriptManager>
  <button id="button1">
    Toggle CSS Class</button>
  <span id="myspan">Wrox Web Site</span>
  </form>

  <script type="text/xml-script">
<page xmlns="http://schemas.microsoft.com/xml-script/2005">
<components>
<label id="myspan"/>
<button id="button1">
<click>
<InvokeMethodAction target="myspan" method="toggleCssClass" >
<parameters className="myCssClass" />
</InvokeMethodAction>
</click>
</button>
</components>
</page>
  </script>

</body>
</html>
