<%@ Page Language="C#" MasterPageFile="~/Enterprise.master" AutoEventWireup="true" CodeFile="ContentPage1.aspx.cs" Inherits="ContentPage1" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:TextBox ID="txtDate" runat="server" class="iMask" alt="{  
                         type: 'fixed',  
                         mask: '99/99/9999',  
                         stripMask: false  
                     }" value="__/__/____"></asp:TextBox>
   
   <asp:TextBox ID="txtTime" runat="server" class="iMask" alt="{
                         type:'fixed',
                         mask:'99:99 xx',
                         stripMask:false
                         }" Text="__:__ __"></asp:TextBox>
</asp:Content>

