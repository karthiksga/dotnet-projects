<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DataView.dataProvider and Service Proxy</title>
    <link href="styles/list.css" rel="stylesheet" type="text/css" />
</head>

<body xmlns:dataview="javascript:Sys.UI.DataView" xmlns:sys="javascript:Sys">
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="sm" runat="server">
                <Scripts>
                    <asp:ScriptReference Name="MicrosoftAjax.js" Path="~/MicrosoftAjax/MicrosoftAjax.js" />
                    <asp:ScriptReference ScriptMode="Inherit" Path="~/MicrosoftAjax/MicrosoftAjaxTemplates.js" />
                </Scripts>
                <Services>
                    <asp:ServiceReference Path="~/Services/ImagesWcfService.svc" />
                </Services>
            </asp:ScriptManager>
        </div>

        <!--DataView calls service directly. No code-->
        <ul class="list sys-template"
            sys:attach="dataview"
            dataview:autofetch="true"
            dataview:dataprovider="{{ Uc.ImagesWcfService }}"
            dataview:fetchoperation="GetImages"
            dataview:fetchparameters="{{ {orderby: 'Name'} }}"
        >
            <li>
                <span>{{ Name }}</span>
                <span class="value">{{ Description }}</span>
            </li>
        </ul>
    </form>
</body>
</html>
