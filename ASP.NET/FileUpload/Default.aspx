<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function ClientUploadComplete() {
            alert('upload complete');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="Server"></asp:ScriptManager>
    <div>
        <asp:UpdatePanel runat="Server">
            <ContentTemplate>
                <cc1:AsyncFileUpload ID="AsyncFileUpload1" runat="server" UploaderStyle="Modern" OnClientUploadComplete="ClientUploadComplete" OnUploadedComplete="UploadComplete"  />
                <asp:Label runat="Server" id="lblComplete" Text="Teset"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>        
    </div>
    </form>
</body>
</html>
