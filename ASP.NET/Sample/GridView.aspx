<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GridView.aspx.cs" Inherits="GridView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("input[type='checkbox']").each(function() {
                $("#" + this.id).live("click", function() {
                //console.log($("#" + this.id).parent().parent().parent().find(".fname").html());
                var obj = $("#" + this.id).parent().parent().parent().find(".fname");
                var obj1 = $("#" + this.id).parent().parent().parent().find(".hdnAmount");
                var obj2 = $("#" + this.id).parent().parent().parent().find(".txtAmount");
                Calc(obj,obj1,obj2);
                });
            });
        });
    </script>
    <script type="text/javascript">
        function Calc(obj, obj1, obj2) {
            obj1.val(obj2.val());
            console.log(obj1.val());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView id="gvCustomers" runat="server" AutoGenerateColumns="false" 
            onrowdatabound="gvCustomers_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkName" runat="server" CssClass="chk" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FirstName" HeaderText="FirstName">
                <ItemStyle CssClass="fname" />
            </asp:BoundField>
            <asp:BoundField DataField="LastName" HeaderText="LastName" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox ID="txtAmount" CssClass="txtAmount" runat="server"></asp:TextBox>
                    <input type="hidden" class="hdnAmount" runat="server" id="hdnAmount" value="1" />                    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:DropDownList ID="ddlProfession" AutoPostBack="false" runat="server"></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </form>
</body>
</html>
