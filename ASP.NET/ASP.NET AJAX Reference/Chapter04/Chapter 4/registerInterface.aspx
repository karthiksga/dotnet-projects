﻿<%@ Page Language="C#" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />

  <script language="JavaScript" type="text/javascript">
    Type.registerNamespace("Department");
    Department.IEmployee = function Department$IEmployee()
    {
      throw Error.notImplemented();
    };
    
    function Department$IEmployee$get_employeeID ()
    {
      throw Error.notImplemented();
    };
    
    function Department$IEmployee$set_employeeID ()
    {
      throw Error.notImplemented();
    };
    
    Department.IEmployee.prototype =
    {
      get_employeeID : Department$IEmployee$get_employeeID,
      set_employeeID: Department$IEmployee$set_employeeID
    }
    
    Department.IEmployee.registerInterface("Department.IEmployee");
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
      set_lastName : function (value) {this._lastName = value;},
      get_employeeID : function () {return this._employeeID;},
      set_employeeID : function (value) {this._employeeID = value;}
    }
    
    Department.Employee.registerClass("Department.Employee", null,
    Department.IEmployee);
  </script>

  </form>
</body>
</html>
