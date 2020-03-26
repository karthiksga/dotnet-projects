<%@ Page Language="C#" %>

<%@ Import Namespace="System.Drawing" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    window.onload = function ()
    {
      var prm = Sys.WebForms.PageRequestManager.getInstance();
      prm.remove_pageLoaded(pageLoadedHandler);
      prm.add_pageLoaded(pageLoadedHandler);
    }
    
    function pageLoadedHandler(sender, e)
    {
      var updatePanelMover;
      var updatePanelProvider;
      var addUpdatePanelDelegate;
      var panelsCreated = e.get_panelsCreated();
      for (var j in panelsCreated)
      {
        updatePanelMover = new Delegates.Mover("container"+j);
        updatePanelProvider = new Delegates.UpdatePanelProvider(panelsCreated[j]);
        addUpdatePanelDelegate = Function.createDelegate(updatePanelProvider,
        updatePanelProvider.addUpdatePanel);
        updatePanelMover.addContent(addUpdatePanelDelegate);
      }
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
      <asp:ScriptReference Path="Delegate.js" />
    </Scripts>
  </asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <asp:Image ImageUrl="images.jpg" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <a href="Javascript:">Wrox Web Site</a>
    </ContentTemplate>
  </asp:UpdatePanel>
  </form>
</body>
</html>
