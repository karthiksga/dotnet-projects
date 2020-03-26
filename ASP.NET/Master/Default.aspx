<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Sample.master"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="Scripts/jquery-1.3.2.min.js"></script>
<script type="text/javascript">
    $(document).ready(function() {
    $('#<%=imgLogin.ClientID%>').live("mouseover",function(){
                this.src = "Images/btn_hoverin.gif";
      });

     $('#<%=imgLogin.ClientID%>').live("mouseout",function() {
                this.src = "Images/btn_hoverout.gif";
      });        
    });
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ImageButton ID="imgLogin" runat="server" 
    ImageUrl="~/Images/btn_hoverout.gif" />
<asp:Button ID="btnOk" runat="server" Text="Submit" />
</asp:Content>
