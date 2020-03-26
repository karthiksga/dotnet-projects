<%@ Page Language="C#" %>

<%@ Import Namespace="System.Drawing" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
    if (Request.Form["OldTime"] != null)
      info.Text = "UpdatePanel refreshed at " + Request.Form["OldTime"];
    Label updatePanelLabel = (Label)Page.FindControl("UpdatePanelLabel");
    updatePanelLabel.Text = "UpdatePanel refreshed at " + DateTime.Now.ToString();
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function pageLoad()
    {
      var prm = Sys.WebForms.PageRequestManager.getInstance();
      prm.remove_beginRequest(beginRequestHandler);
      prm.add_beginRequest(beginRequestHandler);
    }
    
    function beginRequestHandler(sender, e)
    {
      var request = e.get_request();
      var postBackElement = e.get_postBackElement();
      var body = request.get_body();
      var updatePanelLabel = $get("UpdatePanelLabel");
      var oldTime = updatePanelLabel.innerText.slice(25);
      var body2 = body.concat("&OldTime="+oldTime);
      request.set_body(body2);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  <asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>
      <table cellspacing="20" style="background-color: #dddddd">
        <tr>
          <td>
            <asp:Label ID="UpdatePanelLabel" runat="server" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="UpdatePanelButton" runat="server" Text="Update" />
          </td>
        </tr>
        <tr>
          <td>
            <asp:Label runat="server" ID="info" />
          </td>
        </tr>
      </table>
    </ContentTemplate>
  </asp:UpdatePanel>
  </form>
</body>
</html>
