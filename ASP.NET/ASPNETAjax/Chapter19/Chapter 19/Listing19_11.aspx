<%@ Page Language="C#" %>

<%@ Import Namespace="System.Drawing" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
    string text = "Refreshed at " + DateTime.Now.ToString();
    UpdatePanel1Label.Text = text;
    NonPartiallyUpdatableLabel.Text = text;
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <table cellspacing="10" style="background-color: #dddddd">
        <tr>
          <th colspan="2" align="center">
            Partially Updatable Portion (UpdatePanel1)
          </th>
        </tr>
        <tr>
          <td>
            <asp:Label ID="UpdatePanel1Label" runat="server" />
          </td>
          <td>
            <asp:Button ID="UpdatePanelButton" runat="server" Text="Update" />
          </td>
        </tr>
      </table>
    </ContentTemplate>
    <Triggers>
      <asp:AsyncPostBackTrigger ControlID="AsyncPostBackButton" EventName="Click" />
    </Triggers>
  </asp:UpdatePanel>
  <br />
  <br />
  <table cellspacing="10" style="background-color: #dddddd">
    <tr>
      <th colspan="2">
        Non Partially Updatable Portion
      </th>
    </tr>
    <tr>
      <td>
        <asp:Label ID="NonPartiallyUpdatableLabel" runat="server" />
      </td>
      <td>
        <asp:Button ID="Button1" runat="server" Text="Update" />
      </td>
    </tr>
    <tr>
      <td colspan="2" align="center">
        <br />
        <asp:Button ID="AsyncPostBackButton" runat="server" Text="Async Postback Trigger" />
      </td>
    </tr>
  </table>
  </form>
</body>
</html>
