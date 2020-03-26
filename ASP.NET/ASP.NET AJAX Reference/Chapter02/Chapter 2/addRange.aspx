<%@ Page Language="C#" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script language="JavaScript" type="text/javascript">
    function pageLoad() {
      var a1 = ['m1','m2'];
      var a2 = ['m3','m4','m5'];
      Array.addRange(a1, a2);
      for (var i = 0; i<a1.length; i++)
        alert(a1[i]);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  </form>
</body>
</html>
