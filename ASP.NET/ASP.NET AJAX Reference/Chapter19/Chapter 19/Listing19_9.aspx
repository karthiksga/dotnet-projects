<%@ Page Language="C#" MasterPageFile="MasterPage.master" %>

<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
    UpdatePanel2Label.Text = "Refreshed at " + DateTime.Now.ToString();
  }
</script>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <table style="background-color: #aaaaaa" cellspacing="20">
    <tr>
      <th>
        Content Page
      </th>
    </tr>
    <tr>
      <td>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
          <contenttemplate>
<table cellspacing="10" style="background-color: #cccccc">
<tr>
<th colspan="2" align="center">
UpdatePanel Server Control
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
</contenttemplate>
        </asp:UpdatePanel>
      </td>
    </tr>
  </table>
</asp:Content>
