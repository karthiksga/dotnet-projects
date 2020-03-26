<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
     html,body{margin:0;padding:0;height:100%;}
    .main{border:solid 0px red;height:100%;width:100%; }
    .Row11{float:left;width:33.0%;height:33.3%;border:solid 1px red;}
    .Row12{float:left;width:33.0%;height:33.3%;border:solid 1px red;}
    .Row13{float:left;width:33.0%;height:33.3%;border:solid 1px red;}
    .Row21{float:left;width:33.0%;height:33.3%;border:solid 1px red;}
    .Row22{float:left;width:33.0%;height:33.3%;border:solid 1px red;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:WebPartManager ID="WebPartManager1" runat="server" OnAuthorizeWebPart="WebPartManager_Authorize">
        <Personalization Enabled="true" ProviderName="MyProvider" />
    </asp:WebPartManager>
    <div class="main">
        <div class="Row11">
        <asp:WebPartZone ID="WebPartZone1" runat="server">
            <ZoneTemplate>
                <asp:TextBox ID="txtTest" runat="Server"></asp:TextBox>
            </ZoneTemplate>
        </asp:WebPartZone>
        </div>
        <div class="Row12">
        <asp:WebPartZone ID="WebPartZone2" runat="server">
            <ZoneTemplate>
                <asp:TextBox ID="TextBox1" runat="Server"></asp:TextBox>
            </ZoneTemplate>
        </asp:WebPartZone>
        </div>  
        <div class="Row13">
        <asp:WebPartZone ID="WebPartZone3" runat="server">
            <ZoneTemplate>
                <asp:TextBox ID="TextBox2" runat="Server"></asp:TextBox>
            </ZoneTemplate>
        </asp:WebPartZone>
        </div>
        <div class="Row21">
            <asp:Button runat="Server" ID="btnCatalog" OnClick="btnCatalog_Click" Text="Catalog" />
        </div>
        <div class="Row22">
            <asp:CatalogZone ID="SimpleCatalog" runat="server" BackColor="#F7F6F3" BorderColor="#CCCCCC"
                        BorderWidth="1px" Font-Names="Verdana" Padding="6">
                        <HeaderVerbStyle Font-Bold="False" Font-Size="0.8em" Font-Underline="False" ForeColor="#333333" />
                        <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                        <FooterStyle BackColor="#E2DED6" HorizontalAlign="Right" />
                        <PartChromeStyle BorderColor="#E2DED6" BorderStyle="Solid" BorderWidth="1px" />
                        <PartLinkStyle Font-Size="0.8em" />
                        <InstructionTextStyle Font-Size="0.8em" ForeColor="#333333" />
                        <ZoneTemplate>
                            <asp:PageCatalogPart ID="MyCatalog" runat="server" />
                        </ZoneTemplate>
                        <LabelStyle Font-Size="0.8em" ForeColor="#333333" />
                        <SelectedPartLinkStyle Font-Size="0.8em" />
                        <VerbStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                        <HeaderStyle BackColor="#E2DED6" Font-Bold="True" Font-Size="0.8em" ForeColor="#333333" />
                        <EditUIStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                        <PartStyle BorderColor="#F7F6F3" BorderWidth="5px" />
                        <EmptyZoneTextStyle Font-Size="0.8em" ForeColor="#333333" />
                    </asp:CatalogZone>
        </div>
    </div>
    </form>
</body>
</html>
