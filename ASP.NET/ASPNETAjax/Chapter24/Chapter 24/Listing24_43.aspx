<%@ Page Language="C#" %>

<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Net.Mail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
    if (Request.Form["ErrorName"] != null)
    {
      MailMessage mail = new MailMessage();
      MailAddress fromAddress = new MailAddress("problem@somesite.com");
      mail.From = fromAddress;
      MailAddress toAddress = new MailAddress("admin@somesite.com");
      mail.To.Add(toAddress);
      mail.Subject = "Asynchronous Page Postback Request Error at " + DateTime.Now;
      mail.Body = "Error Name: " + Request.Form["ErrorName"];
      if (Request.Form["HttpStatusCode"] != null)
        mail.Body += ("<br/>" + "HTTP Status Code: " +
        Request.Form["HttpStatusCode"]);
      mail.IsBodyHtml = true;
      SmtpClient smtp = new SmtpClient();
      smtp.Host = "Host";
      smtp.Send(mail);
      Response.End();
    }
    Label parentUpdatePanelLabel =
    (Label)Page.FindControl("ParentUpdatePanelLabel");
    parentUpdatePanelLabel.Text = "UpdatePanel refreshed at " +
    DateTime.Now.ToString();
    Label childUpdatePanelLabel = (Label)Page.FindControl("ChildUpdatePanelLabel");
    childUpdatePanelLabel.Text = "UpdatePanel refreshed at " +
    DateTime.Now.ToString();
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function customErrorHandler(error)
    {
      var formBody = new Sys.StringBuilder();
      formBody.append('ErrorName=');
      if (!error)
        formBody.append(encodeURIComponent('Sys.WebForms.PageRequestManagerRequestAbortedException'));
      else
        formBody.append(encodeURIComponent(error.name));
        
      formBody.append('&');
      if (error && error.name == 'Sys.WebForms.PageRequestManagerServerErrorException')
      {
        formBody.append('HttpStatusCode=');
        formBody.append(encodeURIComponent(error.httpStatusCode));
        formBody.append('&');
      }
      var request = new Sys.Net.WebRequest();
      request.set_url(document.form1.action);
      request.set_body(formBody.toString());
      request.invoke();
    }
    
    function pageLoad ()
    {
      var prm = Sys.WebForms.PageRequestManager.getInstance();
      prm.remove_endRequest(endRequestHandler);
      prm.add_endRequest(endRequestHandler);
    }
    
    function endRequestHandler(sender, e)
    {
      var error = e.get_error();
      if (error)
      {
        customErrorHandler(error);
        return;
      }
      var response = e.get_response();
      if (response.get_aborted())
      {
        customErrorHandler(null);
        return;
      }
      var dataItems = e.get_dataItems();
      var builder = new Sys.StringBuilder();
      builder.append("dataItems: ");
      builder.appendLine();
      for (var controlID in dataItems)
      {
        builder.append("Control ID: ");
        builder.append(controlID);
        builder.appendLine();
        builder.append("Data Item: ");
        builder.append(dataItem[controlID]);
        builder.appendLine();
      }
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
<asp:Label ID="ChildUpdatePanelLabel"
runat="server" />
&nbsp;&nbsp;&nbsp;
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
<asp:AsyncPostBackTrigger
ControlID="ChildUpdatePanelTrigger" EventName="Click" />
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
      <td style="width: 50%">
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
