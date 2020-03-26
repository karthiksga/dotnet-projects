<%@ Page Language="C#" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />

  <script language="JavaScript" type="text/javascript">
    Type.registerNamespace("Department");
    Type.registerNamespace("MyNamespace");
    Type.registerNamespace("Department.Section");
    var ar = Type.getRootNamespaces();
    var str = "";
    for (var i = 0; i<ar.length; i++)
      str += (ar[i].getName() + "\n");
    alert(str);
  </script>

  </form>
</body>
</html>
