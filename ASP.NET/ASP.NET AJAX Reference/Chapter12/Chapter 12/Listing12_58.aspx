﻿<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
    if (Request.Headers["CustomClientClasses_AsyncPostBack"] != null)
    {
      if (Request["passwordtbx"] == "password" &&
      Request["usernametbx"] == "username")
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
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function invokingRequestCallback(sender, args)
    {
      var request = args.get_webRequest();
      var builder = new Sys.StringBuilder();
      builder.append("Default request timeout: ");
      builder.append(sender.get_defaultTimeout());
      builder.append("\r\n\r\nDefault executor type: ");
      builder.append(sender.get_defaultExecutorType());
      builder.append("\r\n\r\nTarget URL: ");
      builder.append(request.get_url());
      builder.append("\r\n\r\nHTTP verb: ");
      builder.append(request.get_httpVerb());
      builder.append("\r\n\r\nRequest body: ");
      builder.append(request.get_body());
      builder.append("\r\n\r\nRequest timeout: ");
      builder.append(request.get_timeout());
      builder.append("\r\n\r\nRequest headers: ");
      var headers = request.get_headers();
      for(var header in headers)
      {
        builder.append("\r\n\t");
        builder.append(header);
        builder.append(": ");
        builder.append(headers[header]);
      }
      builder.append("\r\n\r\nClick the Cancel button to cancel the request");
      builder.append(" or OK button to submit the request.");
      var result = Sys.Preview.UI.Window.messageBox(builder.toString(),
      Sys.Preview.UI.MessageBoxStyle.OKCancel);
      
      if (result == Sys.Preview.UI.DialogResult.Cancel)
        args.set_cancel(true);
    }
      
    function completedCallback(sender, eventArgs)
    {
      if (sender.get_timedOut())
      {
        alert("Request timed out!");
        return;
      }
      
      if (sender.get_aborted())
      {
        alert("Request aborted!");
        return;
      }
      
      if (sender.get_statusCode() !== 200)
      {
        alert("Error occured!");
        return;
      }
      
      var reply = sender.get_responseData();
      var delimiter = "|";
      var replyIndex = 0;
      var delimiterIndex;
      var employeeinfotable = $get("employeeinfo");
      employeeinfotable.style.visibility = "visible";
      delimiterIndex = reply.indexOf(delimiter, replyIndex);
      var firstname = reply.substring(replyIndex, delimiterIndex);
      var firstnamespan = $get("firstname");
      firstnamespan.innerText = firstname;
      replyIndex = delimiterIndex + 1;
      delimiterIndex = reply.indexOf(delimiter, replyIndex);
      var lastname = reply.substring(replyIndex, delimiterIndex);
      var lastnamespan = $get("lastname");
      lastnamespan.innerText = lastname;
      replyIndex = delimiterIndex + 1;
      delimiterIndex = reply.indexOf(delimiter, replyIndex);
      var employeeid = reply.substring(replyIndex, delimiterIndex);
      var employeeidspan = $get("employeeid");
      employeeidspan.innerText = employeeid;
      replyIndex = delimiterIndex + 1;
      delimiterIndex = reply.indexOf(delimiter, replyIndex);
      var departmentname = reply.substring(replyIndex, delimiterIndex);
      var departmentnamespan = $get("departmentname");
      departmentnamespan.innerText = departmentname;
    }
  
    function submitCallback(evt)
    {
      var usernametbx = $get("usernametbx");
      var passwordtbx = $get("passwordtbx");
      var requestBody = new Sys.StringBuilder();
      requestBody.append("usernametbx");
      requestBody.append('=');
      requestBody.append(usernametbx.value);
      requestBody.append('&');
      requestBody.append("passwordtbx");
      requestBody.append('=');
      requestBody.append(passwordtbx.value);
      var request = new Sys.Net.WebRequest();
      request.set_timeout(70000);
      request.set_url(document.form1.action);
      request.get_headers()['CustomClientClasses_AsyncPostBack'] = 'true';
      request.get_headers()['Cache-Control'] = 'no-cache';
      request.set_body(requestBody.toString());
      request.invoke();
    }
    
    function pageLoad()
    {
      var submitbtn = $get("submitbtn");
      $addHandler(submitbtn, "click", submitCallback);
      Sys.Net.WebRequestManager.set_defaultTimeout(90000);
      Sys.Net.WebRequestManager.set_defaultExecutorType("Sys.Net.XMLHttpExecutor");
      Sys.Net.WebRequestManager.add_invokingRequest(invokingRequestCallback);
      Sys.Net.WebRequestManager.add_completedRequest(completedCallback);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
    </Scripts>
  </asp:ScriptManager>
  <strong>Username: </strong>
  <asp:TextBox runat="server" ID="usernametbx" />
  <br />
  <strong>Password: &nbsp;</strong><asp:TextBox runat="server" ID="passwordtbx" TextMode="Password" /><br />
  <button id="submitbtn" type="button">
    Submit</button><br />
  <br />
  <table id="employeeinfo" style="background-color: LightGoldenrodYellow; bordercolor: Tan;
    border-width: 1px; color: Black; visibility: hidden" cellpadding="2">
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
        Department</td>
      <td>
        <span id="departmentname" />
      </td>
    </tr>
  </table>
  </form>
</body>
</html>