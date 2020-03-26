<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function blurcb(domEvent)
    {
      alert("blurcb was invoked!");
    }
    function mousedowncb(domEvent)
    {
      alert("mousedowncb was invoked!");
    }
    function pageLoad()
    {
      var mybtn = $get("mybtn");
      $addHandler (mybtn, "blur", blurcb);
      $addHandler (mybtn, "mousedown", mousedowncb);
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
