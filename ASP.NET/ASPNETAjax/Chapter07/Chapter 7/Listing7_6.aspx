<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
function disposingcb()
{
alert("The Disposing event was raised!");
}
function pageLoad()
{
var monitor = new Disposables.Monitor();
monitor.add_disposing(disposingcb);
var btn = $get("btn");
var disposeDelegate = Function.createDelegate(monitor, monitor.dispose);
$addHandler(btn, "click", disposeDelegate);
}
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
      <asp:ScriptReference Path="Monitor2.js" />
    </Scripts>
  </asp:ScriptManager>
  <button id="btn">
    Dispose Monitor</button>
  <div>
  </div>
  </form>
</body>
</html>
