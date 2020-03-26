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
    button.Click += new EventHandler(button_Click);
    bodyCell.Controls.Add(button);
    dynamicChildUpdatePanel.ContentTemplateContainer.Controls.Add(table);
    PlaceHolder1.Controls.Add(dynamicChildUpdatePanel);
  }
  
  void button_Click(object sender, EventArgs e)
  {
    Label label = (Label)Page.FindControl("DynamicChildUpdatePanelLabel");
    label.Text = "UpdatePanel refreshed at " + DateTime.Now.ToString() + "&nbsp;&nbsp;&nbsp;";
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function pageLoad()
    {
      var prm = Sys.WebForms.PageRequestManager.getInstance();
      prm.remove_initializeRequest(initializeRequestHandler);
      prm.add_initializeRequest(initializeRequestHandler);
    }
    
    function initializeRequestHandler(sender, e)
    {
      var request = e.get_request();
      var postBackElement = e.get_postBackElement();
      var builder = new Sys.StringBuilder();
      builder.append("Postback Element: ");
      builder.append(postBackElement.id);
      builder.appendLine();
      builder.appendLine();
      builder.append("Request Target URL: ");
      builder.appendLine();
      builder.append(request.get_url());
      builder.appendLine();
      builder.appendLine();
      builder.append("Request Headers: ");
      builder.appendLine();
      var headers = request.get_headers();
      var headerValue;
      
      for (var headerName in headers)
      {
        builder.append(headerName);
        builder.append(" = ‘");
        headerValue = headers[headerName];
        builder.append(headerValue);
        builder.append("’");
        builder.appendLine();
      }
      
      builder.appendLine();
      builder.append("Request Timeout: ");
      builder.append(request.get_timeout());
      builder.appendLine();
      builder.appendLine();
      builder.append("Request Body: ");
      builder.appendLine();
      builder.append(request.get_body());
      builder.appendLine();
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
Static Child UpdatePanel Control</th>
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
<asp:AsyncPostBackTrigger
ControlID="StaticChildUpdatePanelTrigger"
EventName="Click" />
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
        <asp:Button ID="ParentUpdatePanelTrigger" runat="server" Text="Parent UpdatePanel Trigger"
          Width="100%" />
      </td>
    </tr>
  </table>
  </form>
</body>
</html>
