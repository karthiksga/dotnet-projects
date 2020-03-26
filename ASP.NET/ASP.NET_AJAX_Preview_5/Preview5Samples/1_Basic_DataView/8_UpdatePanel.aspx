<%@ Page Language="C#" AutoEventWireup="true" CodeFile="8_UpdatePanel.aspx.cs" Inherits="_1_Basic_DataView_8_UpdatePanel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Basic UpdatePanel Functionality with Preview bits on 3.5 SP1</title>
    <link href="styles/list.css" rel="stylesheet" type="text/css" />
</head>
<body xmlns:sys="javascript:Sys"
        xmlns:dataview="javascript:Sys.UI.DataView">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
        <Scripts>
            <asp:ScriptReference Name="MicrosoftAjax.js" Path="~/MicrosoftAjax/MicrosoftAjax.js" ScriptMode="Inherit" />
            <asp:ScriptReference Name="MicrosoftAjaxWebForms.js" Path="~/MicrosoftAjax/MicrosoftAjaxWebForms.js" ScriptMode="Inherit" />
            <asp:ScriptReference Path="~/MicrosoftAjax/MicrosoftAjaxTemplates.js" />
        </Scripts>
    </asp:ScriptManager>    
    <div>
    <script type="text/javascript">
        var images1 = [
            { Name: "Crashing water", Description: "A splash of waves captured." },
            { Name: "Dazed", Description: "Mid-day heat?" },
            { Name: "Close Zoom on Giraffe", Description: "Closeup of a Giraffe at Wild Animal Park." },
            { Name: "Pier", Description: "A pier in Morro Bay." },
            { Name: "Seagull reflections", Description: "Seagulls at peace." },
            { Name: "Spain", Description: "In Balboa Park, in downtown San Diego." },
            { Name: "Sumatran Tiger", Description: "Restful." }
        ];
        function setData() {
            $find("imagesListView").set_data(images1);
        }
    </script>
    <!--DataView with external template and placeHolder -->
    <ul id="imagesListView" class="list hidden"
        sys:attach="dataview"
        dataview:itemplaceholder="imagesPlaceholder"
        dataview:itemtemplate="imagesTemplate"
    >
    </ul>
    <ul id="imagesTemplate" class="sys-template">
        <li>
            <span>{{ Name }}</span>
            <span class="value">{{ Description }}</span>
        </li>
    </ul>
    <asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
        The current time is: <asp:Label ID="theTime" runat="server"></asp:Label>
        <asp:Button ID="myUpdate" Text="Update" runat="server"/>
    </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <input type="button" onclick="setData()" value="Set Data"/>
    <!-- Note that the data is rendered where the placeholder is -->
    <ul>
        <li id="imagesPlaceholder">Placeholder: Data will render here when "Set Data" button is clicked</li>
    </ul>
    </div>
    </form>
</body>
</html>

