﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PageA.aspx.vb" Inherits="PageA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    This is PageA
    <asp:HyperLink runat="server" NavigateUrl="~/PageB.aspx" Text="Go to PageB" ></asp:HyperLink>
    </div>
    </form>
</body>
</html>