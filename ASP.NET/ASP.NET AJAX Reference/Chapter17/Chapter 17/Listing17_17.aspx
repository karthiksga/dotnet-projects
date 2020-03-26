<%@ Page Language="C#" %>

<%@ Register Namespace="CustomComponents3" TagPrefix="custom" %>
<!DOCTYPE html PUBLIC “-//W3C//DTD XHTML 1.0 Transitional//EN"
“http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
  void ClickCallback(object sender, EventArgs e)
  {
    Info.Text = TextBox1.Text;
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
  <style type="text/css">
    .WatermarkCssClass
    {
      background-color: #dddddd;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />
  <custom:ScriptManager runat="server" ID="CustomScriptManager1" />
  <custom:TextBoxWatermarkExtenderControl BehaviorID="Behavior1" ID="TextBoxWatermarkExtender1"
    runat="server" TargetControlID="TextBox1" WatermarkCssClass="WatermarkCssClass"
    WatermarkText="Enter value" />
  <asp:TextBox ID="TextBox1" runat="server" />
  <asp:Button ID="Button1" runat="server" OnClick="ClickCallback" Text="Submit" /><br />
  <br />
  <asp:Label ID="Info" runat="server" />
  </form>
</body>
</html>
