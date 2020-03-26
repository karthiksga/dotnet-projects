<%@ Page Language="C#" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script language="JavaScript" type="text/javascript">
    function pageLoad() {
      var a1 = ['m1','m2'];
      var a2 = Array.clone(a1);
      alert("a1[0] = " + a1[0] + "\n" +
      "a2[0] = " + a2[0]);
      alert("a1[1] = " + a1[1] + "\n" +
      "a2[1] = " + a2[1]);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  </form>
</body>
</html>
