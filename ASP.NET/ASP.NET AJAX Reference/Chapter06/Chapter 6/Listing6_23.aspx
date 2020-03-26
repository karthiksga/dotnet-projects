<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function pageLoad()
    {
      var mover = new Delegates.Mover();
      var imageProvider = new Delegates.ImageProvider("images.jpg");
      var addImageDelegate = Function.createDelegate(imageProvider, imageProvider.addImage);
      mover. invokeAddContentDelegate(addImageDelegate);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <Scripts>
      <asp:ScriptReference Path="Delegate.js" />
    </Scripts>
  </asp:ScriptManager>
  </form>
</body>
</html>
