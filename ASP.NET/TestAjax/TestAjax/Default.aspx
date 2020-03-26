<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestAjax._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    
    
    <script language=javascript type="text/javascript">
        function getServerTime(id)
        {
            TestAjax._Default.GetServerTime(id,getServerTime_callback, false);  // asynchronous call
            //alert('hi');
        }

        function sendJSON() {
            TestAjax._Default.sendJSON('[{"FirstName":"karthik","LastName":"sg"},{"FirstName":"asdf","LastName":"gf"}]', sendString, true);
        }
        // This method will be called after the method has been executed
        // and the result has been sent to the client.

        function getServerTime_callback(res)
        {
            alert(res.value);
            //document.getElementById("Panel1").innerText = res.value;
            //setTimeout(getServerTime, 1000);
            //getServerTime_callback(res);
        }
        
        function sendString(res)
        {
            alert(res.value);
        }

        
        
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" BackColor="AliceBlue" BorderWidth=1>
        </asp:Panel>
        <input type=button onclick="getServerTime('1')" value="ClickAjaxDateTime" />
        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        <input type="button" onclick="sendJSON()" value="Send JSON" />
    </div>
    </form>
</body>
</html>
