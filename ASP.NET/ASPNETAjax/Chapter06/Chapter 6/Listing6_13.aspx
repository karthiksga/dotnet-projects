﻿<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script language="javascript" type="text/javascript">
    function clickCallback(myspan)
    {
      var obj = Sys.UI.DomElement.getLocation(myspan);
      alert("x=" + obj.x + "\n" + "y=" + obj.y);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" />
  <span id="myspan" onclick="clickCallback(this)">Click here!</span>
  </form>
</body>
</html>
