<%@ Page Language="C#" %>

<%@ Import Namespace="System.Drawing" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
    Label parentUpdatePanelLabel =
    (Label)Page.FindControl("ParentUpdatePanelLabel");
    parentUpdatePanelLabel.Text = "UpdatePanel refreshed at " +
    DateTime.Now.ToString();
    Label childUpdatePanelLabel =
    (Label)Page.FindControl("childUpdatePanelLabel");
    childUpdatePanelLabel.Text = "UpdatePanel refreshed at " +
    DateTime.Now.ToString();
  }
  void ClickCallback(object sender, EventArgs e)
  {
    Label label = (Label)Page.FindControl("DynamicChildUpdatePanelLabel");
    label.Text = "UpdatePanel refreshed at " + DateTime.Now.ToString() +
    "&nbsp;&nbsp;&nbsp;";
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function pageLoad()
    {
      var prm = Sys.WebForms.PageRequestManager.getInstance();
      prm.remove_pageLoading(pageLoadingHandler);
      prm.add_pageLoading(pageLoadingHandler);
    }
    
    function pageLoadingHandler(sender, e)
    {
      var panelsUpdating = e.get_panelsUpdating();
      var panelsDeleting = e.get_panelsDeleting();
      var dataItems = e.get_dataItems();
      var builder = new Sys.StringBuilder();
      builder.append("panelsUpdating: ");
      builder.appendLine();
      for (var i in panelsUpdating)
      {
        builder.append(panelsUpdating[i].id);
        builder.appendLine();
      }
      builder.appendLine();
      builder.append("panelsDeleting: ");
      builder.appendLine();
      for (var j in panelsDeleting)
      {
        builder.append(panelsDeleting[j].id);
        builder.appendLine();
      }
      builder.appendLine();
      builder.append("_updatePanelIDs: ");
      builder.append(sender._updatePanelIDs);
      builder.appendLine();
      builder.appendLine();
      builder.append("_updatePanelClientIDs: ");
      builder.append(sender._updatePanelClientIDs);
      builder.appendLine();
      builder.appendLine();
      builder.append("_updatePanelHasChildrenAsTriggers: ");
      builder.append(sender._updatePanelHasChildrenAsTriggers);
      builder.appendLine();
      builder.appendLine();
      builder.append("_asyncPostBackTimeout: ");
      builder.append(sender._asyncPostBackTimeout);
      builder.appendLine();
      builder.appendLine();
      builder.append("_asyncPostBackControlIDs: ");
      builder.append(sender._asyncPostBackControlIDs);
      builder.appendLine();
      builder.appendLine();
      builder.append("_asyncPostBackControlClientIDs: ");
      builder.append(sender._asyncPostBackControlClientIDs);
      builder.appendLine();
      builder.appendLine();
      builder.append("_postBackControlIDs: ");
      builder.append(sender._postBackControlIDs);
      builder.appendLine();
      builder.appendLine();
      builder.append("_postBackControlClientIDs: ");
      builder.append(sender._postBackControlClientIDs);
      alert(builder.toString());
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  <table cellspacing="10">
    <tr>
      <td align="center" colspan="2">
        <asp:UpdatePanel ID="ParentUpdatePanel" UpdateMode="Conditional" runat="server">
          <ContentTemplate>
            <table cellspacing="20" style="background-color: #dddddd">
              <tr>
                <th>
                  Parent UpdatePanel Control</th>
              </tr>
              <tr>
                <td>
                  <asp:Label ID="ParentUpdatePanelLabel" runat="server" />
                  &nbsp;&nbsp;&nbsp;
                  <asp:Button ID="ParentUpdatePanelButton" runat="server" Text="Update" />
                </td>
              </tr>
              <tr>
                <td style="width: 100%">
                  <asp:UpdatePanel ID="ChildUpdatePanel" runat="server">
                    <contenttemplate>
<table style="background-color: #aaaaaa">
<tr>
<th>
Child UpdatePanel Control</th>
</tr>
<tr>
<td>
<asp:Label ID="childUpdatePanelLabel"
runat="server" />&nbsp;&nbsp;&nbsp;
<asp:Button ID="ChildUpdatePanelButton"
runat="server" Text="Update" />
</td>
</tr>
<tr>
<td>
</td>
</tr>
</table>
</contenttemplate>
                    <triggers>
<asp:AsyncPostBackTrigger EventName="Click"
ControlID="ChildUpdatePanelTrigger" />
</triggers>
                  </asp:UpdatePanel>
                </td>
              </tr>
            </table>
          </ContentTemplate>
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ParentUpdatePanelTrigger" EventName="Click" />
          </Triggers>
        </asp:UpdatePanel>
      </td>
    </tr>
    <tr>
      <td width="50%">
        <asp:Button ID="ChildUpdatePanelTrigger" runat="server" Text="Child UpdatePanel Trigger"
          Width="100%" />
      </td>
      <td>
        <asp:Button ID="ParentUpdatePanelTrigger" runat="server" Text="Parent UpdatePanel Trigger"
          Width="100%" />
      </td>
    </tr>
  </table>
  </form>
</body>
</html>
