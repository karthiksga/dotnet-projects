<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function clickcb(domEvent)
    {
      var msg = "altKey ----> " + domEvent.altKey;
      msg += ("\nbutton ----> " + domEvent.button);
      msg += ("\ntype ----> " + domEvent.type);
      msg += ("\nctrlKey ----> " + domEvent.ctrlKey);
      msg += ("\ntarget ----> " + domEvent.target);
      msg += ("\noffsetX ----> " + domEvent.offsetX);
      msg += ("\noffsetY ----> " + domEvent.offsetY);
      msg += ("\nclientX ----> " + domEvent.clientX);
      msg += ("\nclientY ----> " + domEvent.clientY);
      msg += ("\nscreenX ----> " + domEvent.screenX);
      msg += ("\nscreenY ----> " + domEvent.screenY);
      msg += ("\nshiftKey ----> " + domEvent.shiftKey);
      alert (msg);
    }
    
    function pageLoad()
    {
      var mybtn = $get("mybtn");
      $addHandler (mybtn, "click", clickcb);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />
  <button id="mybtn" type="button">
    Click Here</button>
  </form>
</body>
</html>
