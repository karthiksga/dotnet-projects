<%@ Page Language="C#" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script language="JavaScript" type="text/javascript">
    function pageLoad() {
      var a = [1, 2, 3, 4];
      Array.insert(a, 1, 5);
      for (var i = 0; i<a.length; i++)
        alert(a[i]);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  </form>
</body>
</html>
