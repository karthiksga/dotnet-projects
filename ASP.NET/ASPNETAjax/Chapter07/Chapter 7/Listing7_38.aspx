<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript" language="javascript">
        var monitor;
        function disposingcb() {
            alert("The Disposing event was raised!");
        }
        function propertyChangedcb(sender, e) {
            alert(e.get_propertyName() + " property changed!");
        }
        function changeFontSize(domEvent) {
            var fontSizetxt = $get("fontSizetxt");
            monitor.set_fontSize(fontSizetxt.value);
        }
        function changeId(domEvent) {
            var id = $get("id");
            try {
                monitor.set_id(id.value);
            }
            catch (ex) {
                alert(ex.message);
            }
        }
        function pageLoad() {
            var type = CustomComponents.Monitor;
            var properties = { id: "Monitor1" };
            var events = { disposing: disposingcb, propertyChanged: propertyChangedcb };
            var references = null;
            var element = null;
            monitor = $create(type, properties, events, references, element);
            var changeIdbtn = $get("changeIdbtn");
            $addHandler(changeIdbtn, "click", changeId);
            var changeFontSizebtn = $get("changeFontSizebtn");
            $addHandler(changeFontSizebtn, "click", changeFontSize);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="Monitor4.js" />
        </Scripts>
    </asp:ScriptManager>
    Enter new Monitor id:
    <input type="text" id="id" />&nbsp;
    <button id="changeIdbtn" type="button">
        Change Id</button>
    <br />
    <br />
    Enter new font size:
    <input type="text" id="fontSizetxt" />&nbsp;
    <button id="changeFontSizebtn" type="button">
        Change Font Size</button>
    <div>
    </div>
    </form>
</body>
</html>
