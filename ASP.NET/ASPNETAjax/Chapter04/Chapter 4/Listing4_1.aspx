﻿<%@ Page Language="C#" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
</head>
<body>    
  <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1" />
    <script language="JavaScript" type="text/javascript">
        Employee = function(firstName, lastName) {
            this._firstName = firstName;
            this._lastName = lastName;
        }

        Employee.prototype =
        {
            get_firstName: function() { return this._firstName; },
            set_firstName: function(value) { this._firstName = value; },
            get_lastName: function() { return this._lastName; },
            set_lastName: function(value) { this._lastName = value; }
        }

        Employee.registerClass("Employee");
        var e = new Employee("Shahram", "Khosravi");
        alert(e.get_firstName());
        e.set_firstName("Shahram1");
        alert(e.get_firstName());

        //    window.onload = function()
        //    {
        //      var e = new Employee ("Shahram", "Khosravi");
        //      alert(e.get_firstName());
        //      e.set_firstName("Shahram1");
        //      alert(e.get_firstName());
        //    }
  </script>
  </form>
</body>
</html>