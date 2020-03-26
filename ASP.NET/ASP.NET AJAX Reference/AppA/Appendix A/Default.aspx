<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script language="text/javascript" type="text/javascript">
function myEventHandler (sender, eventArgs) { }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <scripts>
<asp:ScriptReference Assembly="Microsoft.Web.Preview"
Name="PreviewScript.js" />
<asp:ScriptReference Path="MyClientTypes.js" />
</scripts>
  </asp:ScriptManager>
  <div id="mydiv" />
  <div id="mydiv2" />
  </form>

  <script type="text/xml-script">
    <page xmlns="http://schemas.microsoft.com/xml-script/2005"
    xmlns:custom="CustomComponents">
      <components>
        <custom:MyCustomType id="myCustomType1" myReferenceProperty="myCustomType2"
        myProperty="mydiv" myProperty2="'valuevvv1','valuevvv2','valuevvv3'"
        myArrayProperty="'value1','value2'"
        myEnumProperty="EnumValue2" myEvent="myEventHandler">
          <myReadOnlyArrayProperty>
            <custom:MyType myTypeProperty="'value1'" />
            <custom:MyType myTypeProperty="'value2'" />
          </myReadOnlyArrayProperty>
          
          <myObjectProperty myObjectPropertyProperty1="'value1'"
          myObjectPropertyProperty2="'value2'" />
          
          <myNonReadOnlyStringProperty>value1</myNonReadOnlyStringProperty>
          
          <myNonObjectNonArrayProperty myTypeProperty="'value1'" />
        </custom:MyCustomType>
        <custom:MyCustomType id="myCustomType2" myProperty="mydiv2" />
      </components>
    </page>
  </script>

</body>
</html>
