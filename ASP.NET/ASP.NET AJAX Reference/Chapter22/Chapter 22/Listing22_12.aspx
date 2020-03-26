<%@ Page Language="C#" %>

<%@ Import Namespace="System.Drawing" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
    Label parentUpdatePanelLabel = (Label)Page.FindControl("ParentUpdatePanelLabel");
    parentUpdatePanelLabel.Text = "UpdatePanel refreshed at " + DateTime.Now.ToString();
    Label staticChildUpdatePanelLabel = (Label)Page.FindControl("StaticChildUpdatePanelLabel");
    staticChildUpdatePanelLabel.Text = "UpdatePanel refreshed at " + DateTime.Now.ToString();
    UpdatePanel dynamicChildUpdatePanel = new UpdatePanel();
    dynamicChildUpdatePanel.ID = "DynamicChildUpdatePanel";
    Table table = new Table();
    table.BackColor = Color.FromArgb(90, 90, 90);
    table.ForeColor = Color.FromName("White");
    TableRow headerRow = new TableRow();
    table.Rows.Add(headerRow);
    TableHeaderCell headerCell = new TableHeaderCell();
    headerCell.Text = "Dynamic Child UpdatePanel Control";
    headerRow.Cells.Add(headerCell);
    TableRow bodyRow = new TableRow();
    table.Rows.Add(bodyRow);
    TableCell bodyCell = new TableCell();
    bodyRow.Cells.Add(bodyCell);
    Label label = new Label();
    label.ID = "DynamicChildUpdatePanelLabel";
    label.Text = "UpdatePanel refreshed at " + DateTime.Now.ToString() + "&nbsp;&nbsp;&nbsp;";
    bodyCell.Controls.Add(label);
    Button button = new Button();
    button.Text = "Update";
    button.ID = "DynamicChildUpdatePanelButton";
    button.Click += new EventHandler(ClickCallback);
    bodyCell.Controls.Add(button);
    dynamicChildUpdatePanel.ContentTemplateContainer.Controls.Add(table);
    PlaceHolder1.Controls.Add(dynamicChildUpdatePanel);
  }
  void ClickCallback(object sender, EventArgs e)
  {
    Label label = (Label)Page.FindControl("DynamicChildUpdatePanelLabel");
    label.Text = "UpdatePanel refreshed at " + DateTime.Now.ToString() + "&nbsp;&nbsp;&nbsp;";
  }
</script>

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
      var panelsUpdated = e.get_panelsUpdated();
      var panelsCreated = e.get_panelsCreated();
      var dataItems = e.get_dataItems();
      var builder = new Sys.StringBuilder();
      builder.append("panelsUpdated: ");
      builder.appendLine();
      for (var i in panelsUpdated)
      {
        builder.append(panelsUpdated[i].id);
        builder.appendLine();
      }
      
      builder.appendLine();
      builder.append("panelsCreated: ");
      builder.appendLine();
      
      for (var j in panelsCreated)
      {
        builder.append(panelsCreated[j].id);
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
                  <asp:UpdatePanel ID="StaticChildUpdatePanel" runat="server">
                    <contenttemplate>
<table style="background-color: #aaaaaa">
<tr>
<th>
Static Child UpdatePanel Control
</th>
</tr>
<tr>
<td>
<asp:Label ID="StaticChildUpdatePanelLabel"
runat="server" />&nbsp;&nbsp;&nbsp;
<asp:Button ID="StaticChildUpdatePanelButton"
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
ControlID="StaticChildUpdatePanelTrigger" />
</triggers>
                  </asp:UpdatePanel>
                </td>
              </tr>
              <tr>
                <td style="width: 100%">
                  <asp:PlaceHolder runat="server" ID="PlaceHolder1" />
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
      <td style="width: 50%">
        <asp:Button ID="StaticChildUpdatePanelTrigger" runat="server" Text="Static Child UpdatePanel Trigger"
          Width="100%" />
      </td>
      <td>
        <asp:Button ID="ParentUpdatePanelTrigger" runat="server" Text="Parent UpdatePanel Trigger" Width="100%" />
      </td>
    </tr>
  </table>
  </form>
</body>
</html>
