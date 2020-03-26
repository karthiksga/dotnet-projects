<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
function pageLoad()
{
var type = Sys.Preview.UI.Image;
var properties = { imageURL: "wroxProgrammerSmall.jpg",
alternateText : "Wrox Programmer’s Reference Series",
width: 155, height: 58 };
var events = null;
var references = null;
var element = $get("myImage");
$create(type, properties, events, references, element);
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
  <img id="myImage" />
  </form>
</body>
</html>
