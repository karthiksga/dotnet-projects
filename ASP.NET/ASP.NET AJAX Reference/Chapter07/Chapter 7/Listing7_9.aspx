<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
var monitor;
function disposingcb()
{
alert("The Disposing event was raised!");
}
function propertyChangedcb(sender,e)
{
alert(e.get_propertyName() + " property changed!");
}
function changeProperty(domEvent)
{
var id = $get("id");
monitor.set_id(id.value);
}
function pageLoad()
{
monitor = new Disposables.Monitor();
monitor.add_disposing(disposingcb);
monitor.add_propertyChanged(propertyChangedcb);
var disposebtn = $get("disposebtn");
var disposeDelegate = Function.createDelegate(monitor, monitor.dispose);
$addHandler(disposebtn, "click", disposeDelegate);
var changePropertybtn = $get("changePropertybtn");
$addHandler(changePropertybtn, "click", changeProperty);
}
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
      <asp:ScriptReference Path="Monitor3.js" />
    </Scripts>
  </asp:ScriptManager>
  Enter new Monitor id:
  <input type="text" id="id" />&nbsp;
  <button id="changePropertybtn" type="button">
    Change Property
  </button>
  <br />
  <br />
  <button id="disposebtn" type="button">
    Dispose Monitor</button>
  </form>
</body>
</html>
