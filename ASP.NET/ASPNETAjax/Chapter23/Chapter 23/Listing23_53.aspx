<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function requestCompleted(sender, args)
    {
      var reply = sender.get_responseData();
      alert(reply);
    }
    
    function beginRequestHandler(sender, args)
    {
      var request = args.get_request();
      request.add_completed(requestCompleted);
    }
    
    function pageLoad()
    {
      var prm = Sys.WebForms.PageRequestManager.getInstance();
      prm.add_beginRequest(beginRequestHandler);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <table>
        <tr>
          <td>
            First Name:</td>
          <td>
            <asp:TextBox ID="TextBox1" runat="server">
            </asp:TextBox></td>
        </tr>
        <tr>
          <td>
            Last Name:</td>
          <td>
            <asp:TextBox ID="TextBox2" runat="server">
            </asp:TextBox></td>
        </tr>
        <tr>
          <td colspan="2">
            <asp:Button ID="Button1" runat="server" Text="Submit" /></td>
        </tr>
      </table>
    </ContentTemplate>
  </asp:UpdatePanel>
  <div id="info">
  </div>
  <div>
  </div>
  </form>
</body>
</html>
