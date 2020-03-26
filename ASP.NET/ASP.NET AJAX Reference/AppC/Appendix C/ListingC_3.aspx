<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
    </Scripts>
  </asp:ScriptManager>
  <button id="button1">
    Print Message</button>
  <span id="myspan" />
  </form>

  <script type="text/xml-script">
<page xmlns="http://schemas.microsoft.com/xml-script/2005">
<components>
<label id="myspan"/>
<button id="button1">
<click>
<SetPropertyAction target="myspan" property="text"
value="This is a message!" />
</click>
</button>
</components>
</page>
  </script>

</body>
</html>
