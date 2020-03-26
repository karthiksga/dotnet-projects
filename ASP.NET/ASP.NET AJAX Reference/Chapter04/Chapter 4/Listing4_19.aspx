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
      set_employeeID : function (value) {this._employeeID = value;},
      getEmployeeInfo : function ()
      {
        var info = "First Name: " + this.get_firstName() + "\n";
        info += ("Last Name: " + this.get_lastName() + "\n");
        if (this._employeeID)
          info += ("Employee ID: " + this._employeeID + "\n");
        return info;
      }
    }
    Department.Employee.registerClass("Department.Employee", null, Department.IEmployee);
    Department.Manager = function (firstName, lastName, department)
    {
      Department.Manager.initializeBase(this,[firstName,lastName]);
      this._department = department;
    };
    
    Department.Manager.prototype =
    {
      get_department : function () {return this._department;},
      set_department : function (value) {this._department = value;}
    };
    
    Department.Manager.registerClass("Department.Manager", Department.Employee);
    var mgr = new Department.Manager("SomeFirstName", "SomeLastName", "SomeDepartment");
    mgr.set_employeeID(324);
    alert (mgr.getEmployeeInfo());
  </script>

  </form>
</body>
</html>
