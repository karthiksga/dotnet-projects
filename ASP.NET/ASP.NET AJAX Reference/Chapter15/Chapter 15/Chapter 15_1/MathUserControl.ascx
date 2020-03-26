<%@ Control Language="C#" ClassName="MathUserControl" %>

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

<asp:ScriptManagerProxy runat="server" ID="ScriptManagerProxy1">
  <Services>
    <asp:ServiceReference InlineScript="true" Path="Math.asmx" />
  </Services>
</asp:ScriptManagerProxy>
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
