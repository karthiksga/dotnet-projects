<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LiveValidation.aspx.cs" Inherits="LiveValidation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/style.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="scripts/jquery-1.3.2.js"></script>
    <script type="text/javascript" src="scripts/livevalidation.js"></script>    
    <script type="text/javascript" src="scripts/jquery.maskedinput-1.2.2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%=btnSubmit.ClientID%>").click(function() {
                if ($("#<%=hdnValid.ClientID%>").val() == "0") {
                    $("#<%=lblMessage.ClientID%>").html("");
                    return true;
                }
                else {
                    $("#<%=lblMessage.ClientID%>").html("Please correct the errors");
                    return false;
                }
            });
            $("#txtFirstName").mask("99/99/9999");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                FirstName:
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                LastName:
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Email:
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>           
            <td colspan="2">
            <asp:HiddenField ID="hdnValid" runat="server" />
                <asp:Button ID="btnSubmit" Text="Submit" runat="server" />
            </td>
        </tr>
    </table>
    </div>
    <script type="text/javascript">
        var txtEmail = new LiveValidation('<%=txtEmail.ClientID%>', {
            onlyOnBlur: "true",
            onInvalid: function() {
                this.insertMessage(this.createMessageSpan());
                this.addFieldClass();
                $("#<%=hdnValid.ClientID%>").val("1");
            },
            onValid: function() {
                $("#<%=hdnValid.ClientID%>").val("0");
            }
        });
        txtEmail.add(Validate.Email, {failureMessage:"Enter EmailID"});
    </script>
    </form>
</body>
</html>
