<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />

  <script language="javascript" type="text/javascript">
    Type.registerNamespace("MyNamespace");
    MyNamespace.State = function ()
    {
      throw Error.notImplemented();
    }
    
    MyNamespace.State.prototype = {
      State1 : 1,
      State2 : 2,
      State3 : 4
    }
    
    MyNamespace.State.registerEnum("MyNamespace.State");
    alert(Type.isEnum(MyNamespace.State));
  </script>

  </form>
</body>
</html>
