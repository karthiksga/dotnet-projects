<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function clickCallback(domEvent)
    {
      alert("Click event was raised!");
    }
    function pageLoad()
    {
      var type = Sys.Preview.UI.HyperLink;
      var properties = { text: "<b> Click here!</b> ", htmlEncode: false };
      var events = { click: clickCallback };
      var references = null;
      var element = $get("myHyperLink");
      $create(type, properties, events, references, element);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
    </Scripts>
  </asp:ScriptManager>
  <a id="myHyperLink" />
  </form>
</body>
</html>
