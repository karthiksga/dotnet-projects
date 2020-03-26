<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script language="javascript" type="text/javascript">
    function validateInput(input)
    {
      var reg = new RegExp("(\\d\\d)[-/](\\d\\d)[-/](\\d\\d(?:\\d\\d)?)");
      var date = reg.exec(input);
      if (date == null)
      {
        var err = Error.create("Please enter a valid date!",
        {name : "MyError", errorNumber : 234});
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
        alert("Error Message: " + e.message + "\nError Number: " + e.errorNumber);
        date.value = "";
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
