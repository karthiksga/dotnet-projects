﻿<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC
"-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script language="javascript" type="text/javascript">
    function validateInput(input)
    {
      if (input == null || input.trim() == "")
      {
        var er = Error.argumentNull("input", "Date cannot be null!");
        throw er;
      }
      var reg = new RegExp("(\\d\\d)[-/](\\d\\d)[-/](\\d\\d(?:\\d\\d)?)");
      var date = reg.exec(input);
      if (date == null)
      {
        var err = Error.argument("input","Invalid date!");
        throw err;
      }
    }
    
    function clickCallback()
    {
      var date = document.getElementById("date");
      try
      {
        validateInput(date.value);
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