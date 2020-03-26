<%@ Page Language="C#" %>

<%@ Register Namespace="CustomComponents5" TagPrefix="custom" %>

<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
    Info.Text = DateTime.Now.ToString();
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />
  <custom:CustomUpdatePanel runat="server" ID="CustomUpatePanel1" 
   BuildTemplateMethodProviderType="CustomComponents5.BuildTemplateMethodProvider"
    BuildTemplateMethodProviderMethod="GetBuildTemplateMethod" />
  <br />
  <asp:Label runat="server" ID="Info" />
  </form>
</body>
</html>
