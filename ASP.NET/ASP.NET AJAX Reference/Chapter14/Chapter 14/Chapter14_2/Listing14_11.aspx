<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    var request;
    function onSuccess(result, userContext, methodName)
    {
      userContext.innerHTML = " <b><u>" + result + " </b></u>";
    }
    
    function onFailure(result, userContext, methodName) { }
    
    function add()
    {
      var servicePath = "Listing14_12.asmx";
      var methodName = "Add";
      var useGet = false;
      var xValue = $get("firstNumber").value;
      var yValue = $get("secondNumber").value;
      var params = {x : xValue, y : yValue};
      var userContext = $get("result");
      var webServiceProxy = new Sys.Net.WebServiceProxy();
      webServiceProxy.set_timeout(0);
      request = webServiceProxy._invoke(servicePath, methodName, useGet, params, onSuccess, onFailure, userContext);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />
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
