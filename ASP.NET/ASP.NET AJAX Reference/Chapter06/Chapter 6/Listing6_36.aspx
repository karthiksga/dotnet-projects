<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
  <style type="text/css">
    .myTable
    {
      background-color: LightGoldenrodYellow;
      border-color: Tan;
      border-width: 1px;
      color: Black;
    }
    .myTable th, .myTable td
    {
      padding: 2px 5px;
    }
    .header
    {
      background-color: Tan;
      font-weight: bold;
    }
    .odd
    {
      background-color: PaleGoldenrod;
    }
  </style>

  <script type="text/javascript" language="javascript">
    function pageLoad()
    {
      var imageMover = new Delegates.Mover("container1");
      var imageProvider = new Delegates.ImageProvider("images.jpg");
      var addImageDelegate = Function.createDelegate(imageProvider,
      imageProvider.addImage);
      imageMover.addContent(addImageDelegate);
      var textMover = new Delegates.Mover("container2");
      var textProvider = new Delegates.TextProvider("Wrox Web Site");
      var addTextDelegate = Function.createDelegate(textProvider, textProvider.addText);
      textMover.addContent(addTextDelegate);
      var headers = ["Product", "Distributor", "Producer"];
      var rows = [];
      for (var i=0; i<10; i++)
      {
        rows[i] = ["Product"+i, "Distributor"+i, "Producer"+i];
      }
      var tableMover = new Delegates.Mover("container3");
      var tableProvider = new Delegates.TableProvider(headers, rows);
      var addTableDelegate = Function.createDelegate(tableProvider, tableProvider.addTable);
      tableMover.addContent(addTableDelegate);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <Scripts>
      <asp:ScriptReference Path="Delegate2.js" />
    </Scripts>
  </asp:ScriptManager>
  </form>
</body>
</html>
