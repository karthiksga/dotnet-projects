﻿<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    var request;
    function onSuccess(result, userContext, methodName)
    {
      userContext.innerHTML = "<b><u>" + result + "</b></u>";
    }
    
    function onFailure(result, userContext, methodName)
    {
      var builder = new Sys.StringBuilder();
      builder.append("timedOut: ");
      builder.append(result.get_timedOut());
      builder.appendLine();
      builder.appendLine();
      builder.append("message: ");
      builder.append(result.get_message());
      builder.appendLine();
      builder.appendLine();
      builder.append("stackTrace: ");
      builder.appendLine();
      builder.append(result.get_stackTrace());
      builder.appendLine();
      builder.appendLine();
      builder.append("exceptionType: ");
      builder.append(result.get_exceptionType());
      builder.appendLine();
      builder.appendLine();
      builder.append("statusCode: ");
      builder.append(result.get_statusCode());
      builder.appendLine();
      builder.appendLine();
      builder.append("methodName: ");
      builder.append(methodName);
      alert(builder.toString());
    }
    
    function add()
    {
      var userContext = $get("result");
      var xValue = $get("firstNumber").value;
      var yValue = $get("secondNumber").value;
      MyNamespace.Math.Add(xValue, yValue, onSuccess, onFailure, userContext);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <Scripts>
      <asp:ScriptReference Path="MicrosoftAjax.debug.js" />
    </Scripts>
    <Services>
      <asp:ServiceReference Path="Math.asmx" InlineScript="true" />
    </Services>
  </asp:ScriptManager>
  <table>
    <tr>
      <td style="font-weight: bold" align="right">
        First Number:
      </td>
      <td align="left">
        <input type="text" id="firstNumber" /></td>
    </tr>
    <tr>
      <td style="font-weight: bold" align="right">
        Second Number:
      </td>
      <td align="left">
        <input type="text" id="secondNumber" /></td>
    </tr>
    <tr>
      <td colspan="2" align="center">
        <button onclick="add()">
          Add</button></td>
    </tr>
    <tr>
      <td style="font-weight: bold" align="right">
        Result:
      </td>
      <td align="left">
        <span id="result" />
      </td>
    </tr>
  </table>
  </form>
</body>
</html>
