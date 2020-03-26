<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
var label;
function clickcb(domEvent)
{
var chkbx = $get("chkbx");
label.set_htmlEncode($get("chkbx").checked);
var txtbx = $get("txtbx");
label.set_text(txtbx.value);
}
function pageLoad()
{
var btn = $get("btn");
$addHandler(btn, "click", clickcb);
label = $create(Sys.Preview.UI.Label, null, null, null, $get("myspan"));
}
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
    </Scripts>
  </asp:ScriptManager>
  <input type="checkbox" id="chkbx" />
  <label for="chkbx">
    Enable HTML encoding</label>
  <br />
  <br />
  Enter text:
  <input type="text" id="txtbx" />
  <button id="btn" type="button">
    Submit</button><br />
  <br />
  <span id="myspan"></span>
  <div>
  </div>
  </form>
</body>
</html>
