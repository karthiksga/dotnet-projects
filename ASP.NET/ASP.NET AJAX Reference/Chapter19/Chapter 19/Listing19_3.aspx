<%@ Page Language="C#" %>

<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
    string text = "Refreshed at " + DateTime.Now.ToString();
    UpdatePanel1Label.Text = text;
    UpdatePanel2Label.Text = text;
    NonPartiallyUpdatableLabel.Text = text;
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
  </asp:UpdatePanel>
  <br />
  <br />
  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <table cellspacing="10" style="background-color: #dddddd">
        <tr>
          <th colspan="2">
            Partially Updatable Portion (UpdatePanel2)
          </th>
        </tr>
        <tr>
          <td>
            <asp:Label ID="UpdatePanel2Label" runat="server" />
          </td>
          <td>
            <asp:Button runat="server" Text="Update" />
          </td>
        </tr>
      </table>
    </ContentTemplate>
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
  </table>
 </form>
</body>
</html>
