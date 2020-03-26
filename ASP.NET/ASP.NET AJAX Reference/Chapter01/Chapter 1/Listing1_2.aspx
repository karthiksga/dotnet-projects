<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
    if (Request.Headers["MyCustomHeader"] != null)
    {
      if (Request.Form["passwordtbx"] == "password" &&
      Request.Form["usernametbx"] == "username")
      {
        Response.Write("Shahram|Khosravi|22223333|Some Department|");
        Response.End();
      }
      else
        throw new Exception("Wrong credentials");
    }
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    var request;
    if (!window.XMLHttpRequest)
    {
      window.XMLHttpRequest = function window$XMLHttpRequest()
      {
        var progIDs = [ 'Msxml2.XMLHTTP', 'Microsoft.XMLHTTP' ];
        for (var i = 0; i < progIDs.length; i++)
        {
          try
          {
            var xmlHttp = new ActiveXObject(progIDs[i]);
            return xmlHttp;
          }
          catch (ex) {}
        }
        return null;
      }
    }
    
    window.employee = function window$employee(firstname, lastname,
    employeeid, departmentname)
    {
      this.firstname = firstname;
      this.lastname = lastname;
      this.employeeid = employeeid;
      this.departmentname = departmentname
    }
    
    function deserialize()
    {
      var delimiter="|";
      var responseIndex = 0;
      var delimiterIndex;
      var response = request.responseText;
      delimiterIndex = response.indexOf(delimiter, responseIndex);
      var firstname = response.substring(responseIndex, delimiterIndex);
      responseIndex = delimiterIndex + 1;
      delimiterIndex = response.indexOf(delimiter, responseIndex);
      var lastname = response.substring(responseIndex, delimiterIndex);
      responseIndex = delimiterIndex + 1;
      delimiterIndex = response.indexOf(delimiter, responseIndex);
      var employeeid = response.substring(responseIndex, delimiterIndex);
      responseIndex = delimiterIndex + 1;
      delimiterIndex = response.indexOf(delimiter, responseIndex);
      var departmentname = response.substring(responseIndex, delimiterIndex);
      return new employee(firstname, lastname, employeeid, departmentname);
    }
    
    function readyStateChangeCallback()
    {
      if (request.readyState == 4 && request.status == 200)
      {
        var credentials = document.getElementById("credentials");
        credentials.style.display="none";
        var employeeinfotable = document.getElementById("employeeinfo");
        employeeinfotable.style.display="block";
        var employee = deserialize();
        var firstnamespan = document.getElementById("firstname");
        firstnamespan.innerText = employee.firstname;
        var lastnamespan = document.getElementById("lastname");
        lastnamespan.innerText = employee.lastname;
        var employeeidspan = document.getElementById("employeeid");
        employeeidspan.innerText = employee.employeeid;
        var departmentnamespan = document.getElementById("departmentname");
        departmentnamespan.innerText = employee.departmentname;
      }
    }
    
    window.credentials = function window$credentials(username, password)
    {
      this.username = username;
      this.password = password;
    }
    
    function serialize(credentials)
    {
      var requestBody="";
      requestBody += "usernametbx";
      requestBody += "=";
      requestBody += encodeURIComponent(credentials.username);
      requestBody += "&";
      requestBody += "passwordtbx";
      requestBody += "=";
      requestBody += encodeURIComponent(credentials.password);
      return requestBody;
    }
    
    function submitCallback()
    {
      var usernametbx = document.getElementById("usernametbx");
      var passwordtbx = document.getElementById("passwordtbx");
      var credentials1= new credentials(usernametbx.value, passwordtbx.value);
      var body = serialize(credentials1);
      request = new XMLHttpRequest();
      request.open("POST", document.form1.action);
      request.onreadystatechange = readyStateChangeCallback;
      request.setRequestHeader("MyCustomHeader", "true");
      request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
      request.send(body);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <table id="credentials">
    <tr>
      <td align="right" style="font-weight: bold">
        Username:
      </td>
      <td align="left">
        <asp:TextBox runat="server" ID="usernametbx" /></td>
    </tr>
    <tr>
      <td align="right" style="font-weight: bold">
        Password:
      </td>
      <td align="left">
        <asp:TextBox runat="server" ID="passwordtbx" TextMode="Password" />
      </td>
    </tr>
    <tr>
      <td align="center" colspan="2">
        <button id="Button1" type="button" onclick="submitCallback()">
          Submit</button>
      </td>
    </tr>
  </table>
  <table id="employeeinfo" style="background-color: LightGoldenrodYellow; border-color: Tan;
    border-width: 1px; color: Black; display: none" cellpadding="2">
    <tr style="background-color: Tan; font-weight: bold">
      <th colspan="2">
        Your Information</th>
    </tr>
    <tr>
      <td style="font-weight: bold">
        First Name</td>
      <td>
        <span id="firstname" />
      </td>
    </tr>
    <tr style="background-color: PaleGoldenrod">
      <td style="font-weight: bold">
        Last Name</td>
      <td>
        <span id="lastname" />
      </td>
    </tr>
    <tr>
      <td style="font-weight: bold">
        Employee ID</td>
      <td>
        <span id="employeeid" />
      </td>
    </tr>
    <tr style="background-color: PaleGoldenrod">
      <td style="font-weight: bold">
        Department
      </td>
      <td>
        <span id="departmentname" />
      </td>
    </tr>
  </table>
  </form>
</body>
</html>
