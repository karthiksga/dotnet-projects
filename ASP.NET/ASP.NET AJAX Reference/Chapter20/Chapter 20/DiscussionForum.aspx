<%@ Page Language="C#" %>
<%@ Register TagName="DiscussionForum" TagPrefix="custom"
Src="DiscussionForum.ascx" %>
<!DOCTYPE html PUBLIC “-//W3C//DTD XHTML 1.0 Transitional//EN"
“http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Untitled Page</title>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager runat="server" ID="ScriptManager1" />
<custom:DiscussionForum runat="server" ID="DiscussionForum1"
DataFile="~/App_Data/Messages.xml" />
</form>
</body>
</html>
