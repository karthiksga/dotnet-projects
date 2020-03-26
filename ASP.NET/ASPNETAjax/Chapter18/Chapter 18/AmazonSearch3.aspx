<%@ Page Language="C#" %>

<%@ Register Namespace="CustomComponents3" TagPrefix="custom" %>
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
      <asp:ScriptReference Path="HtmlGenerator.js" />
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
    </Scripts>
    <Services>
      <asp:ServiceReference InlineScript="true" Path="AmazonSearch3.asbx" />
    </Services>
  </asp:ScriptManager>
  <custom:AmazonSearchScriptControl runat="server" ID="MS" SearchMethod="MyServices3.AmazonService3.Search"
    HtmlGeneratorID="MyHtmlGenerator" Path="AspNetAjaxAmazonSearch.js" ClientControlType="CustomComponents3.AspNetAjaxAmazonSearch" />
  <custom:HtmlGeneratorScriptControl runat="server" ID="MyHtmlGenerator" Path="HtmlGenerator2.js"
    ClientControlType="CustomComponents3.HtmlGenerator2">
    <RowStyle BackColor="#eeeeee" Width="100%" />
    <AlternatingRowStyle BackColor="#cccccc" Width="100%" />
  </custom:HtmlGeneratorScriptControl>
  </form>
</body>
</html>
