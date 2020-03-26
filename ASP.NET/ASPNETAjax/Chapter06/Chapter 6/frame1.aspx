<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script language="javascript" type="text/javascript">
    function frame1ClickCallback()
    {
      var frame1TextBox = Sys.UI.DomElement.getElementById("frame1TextBox");
      var frame2TextBox = Sys.UI.DomElement.getElementById("frame2TextBox", parent.frame2.document);
      frame2TextBox.value = frame1TextBox.value;
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />
  <input type="text" id="frame1TextBox" />&nbsp;
  <input type="button" onclick="frame1ClickCallback()" value="Send" />
  </form>
</body>
</html>
