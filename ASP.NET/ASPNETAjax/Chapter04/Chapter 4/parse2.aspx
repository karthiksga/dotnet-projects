<%@ Page Language="C#" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />

  <script language="JavaScript" type="text/javascript">
    Type.registerNamespace("Department");
    Department.Employee = function (firstName, lastName)
    {
      this._firstName = firstName;
      this._lastName = lastName;
    }
    
    Department.Employee.prototype =
    {
      get_firstName : function () {return this._firstName;},
      set_firstName : function (value) {this._firstName = value;},
      get_lastName : function() {return this._lastName;},
      set_lastName : function (value) {this._lastName = value;}
    }
    
    Department.Employee.registerClass("Department.Employee");
    alert (Type.parse("Employee", Department));
  </script>

  </form>
</body>
</html>
