<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GridCallBack1.aspx.vb" Inherits="GridCallBack" %>

<%@ Register Assembly="CustomControl" Namespace="CustomControl" TagPrefix="RefreshPanel" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grid Page</title>
    <link href="styles/gridstyle.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
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
                    <asp:Button ID="btnSave" Text="Save" runat="server" />
                </div>
            </div>
            <div class="divGrid">
                <asp:GridView ID="CustomerGrid" EmptyDataText="No records to display" runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="First Name" DataField="First Name" />
                        <asp:BoundField HeaderText="Last Name" DataField="Last Name" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
