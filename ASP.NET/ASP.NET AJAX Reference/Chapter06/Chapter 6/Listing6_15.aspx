<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script language="javascript" type="text/javascript">
    function mousedowncb(event)
    {
      event = event || window.event;
      document.oldClientX = event.clientX;
      document.oldClientY = event.clientY;
      document.onmousemove = mousemovecb;
      document.onmouseup = mouseupcb;
      return false;
    }
    
    function mouseupcb(event)
    {
      event = event || window.event;
      document.onmousemove = null;
      document.onmouseup = null;
      return false;
    }
    
    function mousemovecb(event)
    {
      event = event || window.event;
      var deltaClientX = event.clientX - document.oldClientX;
      var deltaClientY = event.clientY - document.oldClientY;
      var sender = $get("mydiv");
      var senderLocation = Sys.UI.DomElement.getLocation(sender);
      Sys.UI.DomElement.setLocation(sender, senderLocation.x+deltaClientX,
      senderLocation.y+deltaClientY);
      document.oldClientX = event.clientX;
      document.oldClientY = event.clientY;
      return false;
    }
  </script>

</head>
<body>
  <div id="mydiv" style="position: absolute; left: 0px; top: 0px" onmousedown="mousedowncb(event)">
    <a href="javascript:void(0)" id="myspan" style="font-weight: bold">Wrox Web Site</a>
  </div>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  </form>
</body>
</html>
