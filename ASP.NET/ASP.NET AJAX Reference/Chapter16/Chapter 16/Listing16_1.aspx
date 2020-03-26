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

  <script type="text/javascript" language="javascript">
    function toggleCssClass(domEvent)
    {
      Sys.UI.DomElement.toggleCssClass(domEvent.target, "CssClass1");
    }
    
    function pageLoad()
    {
      var label1 = $get("label1");
      $addHandler(label1, "mouseover", toggleCssClass);
      $addHandler(label1, "mouseout", toggleCssClass);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />
  <span id="label1">Wrox Web Site</span>
  </form>
</body>
</html>
