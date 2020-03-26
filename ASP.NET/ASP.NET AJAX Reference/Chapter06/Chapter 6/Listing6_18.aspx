<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script language="javascript" type="text/javascript">
    function pageLoad()
    {
      var bounds = Sys.UI.DomElement.getBounds($get("myspan"));
      alert(bounds.width);
    }
  </script>

</head>
<body>
  <span id="myspan" style="font-weight: bold;">Wrox Web Site</span>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  </form>
</body>
</html>
