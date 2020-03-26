<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
  <style type="text/css">
    .CssClass1
    {
      background-color: Blue;
      color: Yellow;
      font-size: 40px;
    }
  </style>

  <script language="javascript" type="text/javascript">
    function toggleCssClass(myLink)
    {
      Sys.UI.DomElement.toggleCssClass(myLink, "CssClass1");
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />
  <a href="http://www.wrox.com" onmouseover="toggleCssClass(this)" onmouseout="toggleCssClass(this)">
    Wrox Web Site</a>
  </form>
</body>
</html>
