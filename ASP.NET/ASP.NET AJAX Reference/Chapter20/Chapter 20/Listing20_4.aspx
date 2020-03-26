<%@ Page Language="C#" Theme="Theme1" %>

<%@ Register Namespace="CustomComponents" TagPrefix="custom" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  <custom:MasterDetailControl2 ID="MasterDetailControl21" runat="server" DataKeyNames="ProductID"
    DetailDataSourceID="DetailDataSource" MasterDataSourceID="MasterDataSource" MasterSkinID="DropDownList1"
    DetailSkinID="DetailsView1" CellSpacing="20" HorizontalAlign="Center" GridLines="both"
    BorderStyle="Ridge" BorderWidth="20" BorderColor="Yellow" BackImageUrl="images.jpg"
    DataTextField="ProductName" DataValueField="ProductID">
    <MasterContainerStyle HorizontalAlign="center" BorderStyle="Ridge" BorderWidth="20"
      BorderColor="Yellow" />
    <DetailContainerStyle BorderStyle="Ridge" BorderWidth="20" BorderColor="Yellow" />
  </custom:MasterDetailControl2>
  <asp:SqlDataSource runat="server" ID="MasterDataSource" ConnectionString="<%$ ConnectionStrings:MyConnectionString %>"
    SelectCommand="Select ProductID, ProductName From Products" />
  <asp:SqlDataSource ID="DetailDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:MyConnectionString %>"
    SelectCommand="Select * From Products where ProductID=@ProductID" 
    UpdateCommand="Update Products Set ProductName=@ProductName,CategoryID=@CategoryID,UnitPrice=@UnitPrice,DistributorName=@DistributorName where ProductID=@ProductID" 
    DeleteCommand="Delete From Products where ProductID=@ProductID"
    InsertCommand="Insert Into Products (ProductName, CategoryID, UnitPrice,DistributorName) Values (@ProductName, @CategoryID, @UnitPrice,@DistributorName)">
    <SelectParameters>
      <asp:ControlParameter ControlID="MasterDetailControl21" Name="ProductID" PropertyName="SelectedValue"
        DefaultValue="1" />
    </SelectParameters>
  </asp:SqlDataSource>
  &nbsp;
  </form>
</body>
</html>
