<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC
"-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script language="javascript" type="text/javascript">
    function MyErrorCreate(d, b)
    {
      var a = new Error(d);
      a.message = d;
      if(b)
        for(var c in b)
          a[c] = b[c];
      //a.popStackFrame();
      return a
    };
    
    function validateInput(input)
    {
      var reg = new RegExp("(\\d\\d)[-/](\\d\\d)[-/](\\d\\d(?:\\d\\d)?)");
      var date = reg.exec(input);
      if (date == null)
      {
        var err = MyErrorCreate("Please enter a valid date!",
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
        alert("Error Message: " + e.message + "\nError Number: " + e.errorNumber +
        "\nDocument: " + e.fileName + "\nLine Number: " + e.lineNumber);
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
