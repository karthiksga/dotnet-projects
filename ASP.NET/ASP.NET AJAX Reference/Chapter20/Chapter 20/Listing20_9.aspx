<%@ Page Language="C#" Theme="Theme1" %>

<%@ Register TagPrefix="custom" Namespace="CustomComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<script runat="server">
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />
  <asp:GridView ID="gv2" runat="Server" AutoGenerateColumns="false" AllowSorting="true"
    DataSourceID="GridViewSource" AutoGenerateEditButton="true" DataKeyNames="ProductID"
    BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2"
    ForeColor="Black" GridLines="None">
    <FooterStyle BackColor="Tan" />
    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
    <HeaderStyle BackColor="Tan" Font-Bold="True" />
    <AlternatingRowStyle BackColor="PaleGoldenrod" />
    <Columns>
      <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName" />
      <custom:MasterDetailField MasterSkinID="RainyDay2" DetailSkinID="RainyDay" EnableTheming="true"
        DataValueField="CategoryID" DataKeyNames="CategoryID, DateCreated" DataTextField="CategoryName"
        MasterDataSourceID="MasterSource" SortExpression="CategoryName" HeaderText="Category Name"
        NullDisplayText="Unknown" DetailDataSourceID="DetailSource" />
    </Columns>
  </asp:GridView>
  <asp:ObjectDataSource ID="GridViewSource" runat="Server" SortParameterName="sortExpression"
    TypeName="Product" SelectMethod="Select" UpdateMethod="Update" />
  <asp:SqlDataSource ID="MasterSource" runat="Server" ConnectionString="<%$ ConnectionStrings:MyConnectionString %>"
    SelectCommand="Select * From Categories" />
  <asp:SqlDataSource ID="DetailSource" runat="Server" ConnectionString="<%$ ConnectionStrings:MyConnectionString %>"
    SelectCommand="Select * From Categories Where CategoryID=@CategoryID" 
    UpdateCommand="Update Categories Set CategoryName=@CategoryName,CategoryDescription=@CategoryDescription,DateCreated=@DateCreated Where CategoryID=@CategoryID" 
    InsertCommand="Insert Into Categories (CategoryName, CategoryDescription) Values (@CategoryName, @CategoryDescription)" 
    DeleteCommand="Delete From Categories Where CategoryID=@CategoryID2">
    <SelectParameters>
      <asp:SessionParameter Name="CategoryID" SessionField="SelectedValue" />
    </SelectParameters>
    <UpdateParameters>
      <asp:SessionParameter Name="CategoryID" SessionField="SelectedValue" />
    </UpdateParameters>
    <DeleteParameters>
      <asp:SessionParameter Name="CategoryID2" SessionField="SelectedValue" />
    </DeleteParameters>
  </asp:SqlDataSource>
  </form>
</body>
</html>
