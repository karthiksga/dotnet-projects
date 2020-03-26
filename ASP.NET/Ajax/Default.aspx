<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
        var xmlHttp;
        function createXmlHttp() {
            try {                
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");              
            }
            catch(e) {
                try {                    
                    xmlHttp = new XMLHttpRequest();
                }
                catch (e) {
                    xmlHttp = null;
                }
            }
//            if (!xmlHttp && typeof XMLHttpRequest != "undefined") {
//                xmlHttp = new XMLHttpRequest();
//            }            
        }
        function ShowCountry(e) {            
            var name = e.value;            
            createXmlHttp();
            if (xmlHttp) {
                xmlHttp.onreadystatechange = HandleResponse;
                xmlHttp.open("GET", "Content.aspx?name="+name, true);
                xmlHttp.send(null);
                
            }
        }
        function HandleResponse() {
            if (xmlHttp.readyState == 4) {
                if (xmlHttp.status == 200) {
                    document.getElementById('divResponse').innerHTML = xmlHttp.responseText;
                }
            }
        }       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="text" id="txtCountry" onkeyup="ShowCountry(this)" />
    </div>
    <div id="divResponse"></div>
    </form>
</body>
</html>
