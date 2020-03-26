<%@ Page Language="C#" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script language="JavaScript" type="text/javascript">
    function Person (firstName, lastName)
    {
      this.firstName = firstName;
      this.lastName = lastName;
    }
    
    function pageLoad() {
      var p = new Person("Shahram", "Khosravi");
      var b = Object.getType(p);
      var name = Object.getTypeName(b);
      alert(name);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  </form>
</body>
</html>
