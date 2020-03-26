<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC
"-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script language="javascript" type="text/javascript">
    function validateInput(input)
    {
      if (arguments.length != arguments.callee.length)
      {
        var err3=Error.parameterCount( "Invalid argument count!");
        throw err3;
      }
      if (input == null || input.trim() == "")
      {
        var er = Error.argumentNull("input", "Date cannot be null!");
        throw er;
      }
      var reg = new RegExp("(\\d\\d)[-](\\d\\d)[-](\\d\\d\\d\\d)");
      var date = reg.exec(input);
      if (date == null)
      {
        var err = Error.argumentUndefined("input", "Undefined value!");
        throw err;
      }
      var ar = input.split("-");
      if (ar[2] < 1900 || ar[2] > 2008)
      {
        var err2=Error.argumentOutOfRange("input",input);
        throw err2;
      }
    }
    
    function clickCallback()
    {
      var date = document.getElementById("date");
      try
      {
        validateInput(date.value, 3);
      }
      catch (e)
      {
        alert(e.message);
        date.value="";
      }
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />
  Enter date:
  <input type="text" id="date" />&nbsp;
  <input type="button" value="Validate" onclick="clickCallback()" />
  </form>
</body>
</html>
