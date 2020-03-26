<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Listbox.aspx.vb" EnableEventValidation="false" Inherits="Listbox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
        function loadListBox() {
            var lstboxLeft = document.getElementById("lstboxLeft");
            var opt = document.createElement("option");            
            opt.text = "Test1";
            opt.value = "1"
            lstboxLeft.options.add(opt);
            var opt = document.createElement("option");
            opt.text = "Test2";
            opt.value = "2"
            lstboxLeft.options.add(opt);
            var opt = document.createElement("option");
            opt.text = "Test3";
            opt.value = "3"
            lstboxLeft.options.add(opt);            
        }
        function shift() {
            var lstboxLeft = document.getElementById("lstboxLeft");
            var lstboxRight = document.getElementById("lstboxRight");
            var txtHiddenText = document.getElementById("txtHiddenText");
            var txtHiddenValue = document.getElementById("txtHiddenValue");    
            for (i = 0; i < lstboxLeft.options.length; i++) {
                if (lstboxLeft.options[i].selected == true) {
                    var opt = document.createElement("option");
                    opt.text = lstboxLeft.options[i].text;
                    opt.value = lstboxLeft.options[i].value;
                    lstboxRight.options.add(opt);
                    txtHiddenText.value = lstboxLeft.options[i].text;
                    txtHiddenValue.value = lstboxLeft.options[i].value;
                }
            }
            return true;
        }        
    </script>
</head>
<body onload="loadListBox()">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:ListBox ID="lstboxLeft" runat="server" Height="100px" Width="100px"></asp:ListBox>                    
                </td>                
                <td>
                    <asp:Button ID="btnLeft" runat="server" Text="<<" />
                    <br />
                    <asp:Button ID="btnRight" OnClientClick="return shift()" runat="server" Text=">>" />
                </td>
                <td>
                    <asp:ListBox ID="lstboxRight" Height="100px" Width="100px" runat="server"
                        AutoPostBack="True"></asp:ListBox>
                    <input type="hidden" id="txtHiddenText" runat="server" />
                    <input type="hidden" id="txtHiddenValue" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
