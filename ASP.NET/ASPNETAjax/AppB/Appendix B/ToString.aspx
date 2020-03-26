<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Untitled Page</title>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager runat="server" ID="ScriptManager1">
<Scripts>
<asp:ScriptReference Assembly="Microsoft.Web.Preview"
Name="PreviewScript.js" />
</Scripts>
</asp:ScriptManager>
<input type="text" id="text1" />
<span id="span1" />
</form>
<script type="text/xml-script">
<page xmlns="http://schemas.microsoft.com/xml-script/2005">
<components>
<textBox id="text1" />
<label id="span1">
<bindings>
<binding dataContext="text1" dataPath="text" property="text"
transform="ToString" transformerArgument="${0}" />
</bindings>
</label>
</components>
</page>
</script>
</body>
</html>