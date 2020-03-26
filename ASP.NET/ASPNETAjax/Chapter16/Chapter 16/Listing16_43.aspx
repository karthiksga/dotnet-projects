<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
  void ClickCallback(object sender, EventArgs e)
  {
    Info.Text = TextBox1.Text;
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
  <style type="text/css">
    .WatermarkCssClass
    {
      background-color: #dddddd;
    }
  </style>

  <script type="text/javascript" language="javascript">
    var textBoxWatermarkBehavior;
    function submitCallback()
    {
      textBoxWatermarkBehavior._onSubmit();
    }
    
    function pageLoad()
    {
      var properties = {name : "MyTextBoxWatermarkBehaviorName",
                        id : "MyTextBoxWatermarkBehaviorID",
                        WatermarkText : "Enter text here",
                        WatermarkCssClass : "WatermarkCssClass"};
                        
      var textBox1 = $get("TextBox1");
      textBoxWatermarkBehavior = $create(AjaxControlToolkit.TextBoxWatermarkBehavior, 
                                         properties, null, null, textBox1);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server" onsubmit="submitCallback();return true;">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <Scripts>
      <asp:ScriptReference Path="BehaviorBase.js" />
      <asp:ScriptReference Path="TextBoxWatermarkBehavior.js" />
    </Scripts>
  </asp:ScriptManager>
  <asp:TextBox ID="TextBox1" runat="server" />
  <asp:Button ID="Button1" runat="server" OnClick="ClickCallback" Text="Submit" /><br />
  <br />
  <asp:Label ID="Info" runat="server" />
  </form>
</body>
</html>
