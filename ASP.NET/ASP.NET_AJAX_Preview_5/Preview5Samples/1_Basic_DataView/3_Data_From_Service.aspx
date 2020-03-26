<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Data from Service ASPX</title>
    <link href="styles/list.css"rel="stylesheet" type="text/css" />
        <script type="text/javascript">
            function pageLoad() {
                // Create DataView
                $create(Sys.UI.DataView, null, null, null, $get("imagesListView"));

                // Call Web service proxy from script
                Uc.ImagesWcfService.GetImages("Name", querySucceeded);
            }

            function querySucceeded(results) {
                // Set returned data on DataView
                $find("imagesListView").set_data(results);
            }
        </script>
</head>
<body>
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

        <!--Client Template used by attached DataView-->
        <ul id="imagesListView" class="sys-template" >
             <li>
                <span class="name">{{ Name }}</span>
                <span class="value">{{ Description }}</span>
            </li>
       </ul>
    </form>
</body>
</html>
