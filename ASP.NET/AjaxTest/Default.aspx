<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Validator(name) {
            var _name = name;
            this.getName = function() {
                return _name;
            }
        }
        Validator.prototype.validateInput = function(input) {            
            if (input == "") {
                var err = Error.create("Please enter text", { name: "MyError", errorNumber: "24" });
                throw err;
            }
        }            
        
        var validate;
        function pageLoad() {
            validate = new Validator("Test");
            var btn = $get("btnSample");
            //var events = {click:this.clickcb};
            //$addHandlers(btn, events, this);
            $addHandler(btn, "click", clickcb);           
        }
        function clickcb(event) {            
            var txt = $get("txtSample");            
            try {                
                validate.validateInput(txt.value);                
            }
            catch(e){
                alert(e.message + " " + e.name + " " + e.errorNumber);
                event.preventDefault();
            }
        }       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="Server"></asp:ScriptManager>
    <script type="text/javascript">
        function State() {
            throw Error.notImplemented();
        }
        State.prototype = {
            State1: 1,
            State2: 2,
            State3: 3
        }
        State.registerEnum("State");  
    </script>
    <div>
    <asp:TextBox id="txtSample" runat="Server"></asp:TextBox>
    <br />
    <asp:Button ID="btnSample" runat="Server" Text="Test" />
    </div>
    </form>
</body>
</html>
