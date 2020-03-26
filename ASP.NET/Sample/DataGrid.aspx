<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataGrid.aspx.cs" Inherits="DataGrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:DataGrid ID="CountryDataGrid" runat="server" AllowPaging="True" 
            onpageindexchanged="CountryDataGrid_PageIndexChanged">
            <PagerStyle Mode="NumericPages" />
            <Columns>
                <asp:TemplateColumn HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkBox" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    
    </div>
    </form>
</body>
</html>
