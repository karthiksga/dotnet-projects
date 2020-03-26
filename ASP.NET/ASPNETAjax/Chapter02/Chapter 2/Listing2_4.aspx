<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC
"-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script language="javascript" type="text/javascript">
function getStack(err)
{
var a = err.stack.split("\n");
Array.forEach(a, function(item, i, array)
{
array[i] = String.format("a[{0}] = {1}", i, item);
});
alert(a.join("\n"));
}
function validateInput(input)
{
var reg = new RegExp("(\\d\\d)[-/](\\d\\d)[-/](\\d\\d(?:\\d\\d)?)");
var date = reg.exec(input);
if (date == null)
{
var err = Error.create("Please enter a valid date!",
{name : "MyError", errorNumber : 234});
getStack(err);
err.popStackFrame();
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
getStack(e);
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
  <br />
  <span id="span1"></span>
  </form>
</body>
</html>
