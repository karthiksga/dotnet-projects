<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GridCallBack.aspx.cs" Inherits="GridCallBack" %>

<%@ Register Assembly="CustomControl" Namespace="CustomControl" TagPrefix="RefreshPanel" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Grid Page</title>
    <link href="styles/gridstyle.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript">
       function ClientCallback(result, context)
       {
           //Handle client side functionality here...
           //alert("saved");
       }
       function SaveData() {
           call();
           document.getElementById('lnkRefresh').click();           
       }
</script>
</head>
<body>
    <form id="form1" runat="server">    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">    
    <ContentTemplate>
    <div class="divMain">
        <div class="divContent">
            <div class="divControls">
                <div class="divLabel">
                    <asp:Label ID="lblFirstName" runat="server" Text="FirstName"></asp:Label>
                </div>
                <div class="divControl">
                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                </div>
                <div class="divLabel">
                    <asp:Label ID="lblLastName" runat="server" Text="LastName"></asp:Label>
                </div>
                <div class="divControl">
                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                </div>
                <div class="divSave">
                    <%--<asp:Button ID="btnSave" Text="Save" runat="server" />--%>
                    <input type="button" id="btnSave" value="Save" onclick="SaveData()" />          
                </div>
            </div>
            <div class="divGrid">
                <RefreshPanel:DynamicPanel runat="server" ID="GridPanel" OnRefreshing="CustomerGrid_Refreshing">
                    <asp:GridView ID="CustomerGrid" Width="345px" AutoGenerateColumns="false" EmptyDataText="No records to display" runat="server">
                        <Columns>
                            <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                            <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                        </Columns>
                    </asp:GridView>
                </RefreshPanel:DynamicPanel>  
                <RefreshPanel:DynamicPanelRefreshLink runat="server" id="lnkRefresh" OnClientClick="alert('clicked');" PanelID="GridPanel">Click To Refresh</RefreshPanel:DynamicPanelRefreshLink>                
            </div>            
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>  
    </form>      
</body>
</html>

