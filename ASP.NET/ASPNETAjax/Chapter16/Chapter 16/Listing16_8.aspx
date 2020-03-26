<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    var clickBehavior1;
    function disposingCallback(sender, args)
    {
      alert("disposing event was raised!");
    }
    
    function propertyChangedCallback(sender, args)
    {
      alert(args.get_propertyName() + " was changed!");
    }
    
    function clickCallback()
    {
      alert("name = " + clickBehavior1.get_name() + "\n" + "id = " + clickBehavior1.get_id());
    }
    
    function pageLoad()
    {
      var events =
      {
        disposing : disposingCallback,
        propertyChanged : propertyChangedCallback,
        click : clickCallback
      };
      
      var properties =
      {
        name : "MyClickBehaviorName",
        id : "MyClickBehaviorID"
      };
      
      clickBehavior1 = $create(Sys.Preview.UI.ClickBehavior, properties, events, null, $get("mydiv"));
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
    </Scripts>
  </asp:ScriptManager>
  <div id="mydiv">
    Click Me</div>
  </form>
</body>
</html>
