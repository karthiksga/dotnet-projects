<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function transformCallback(sender, e)
    {
      var builder = new Sys.StringBuilder();
      builder.append("old value: ");
      builder.append(e.get_value());
      builder.appendLine();
      builder.append("direction: ");
      builder.append(e.get_direction());
      builder.appendLine();
      builder.append("target property type: ");
      builder.append(e.get_targetPropertyType());
      builder.appendLine();
      builder.append("transformer argument: ");
      builder.append(e.get_transformerArgument());
      builder.appendLine();
      e.set_value(e.get_transformerArgument()+e.get_value());
      builder.append("new value: ");
      builder.append(e.get_value());
      alert(builder.toString());
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
    </Scripts>
  </asp:ScriptManager>
  <input type="text" id="text1" />
  <span id="span1" />
  </form>

  <script type="text/xml-script">
    <page xmlns="http://schemas.microsoft.com/xml-script/2005">
      <components>
        <textBox id="text1" />
        <label id="span1">
          <bindings>
            <binding dataContext="text1" dataPath="text" property="text"
            transform="transformCallback" transformerArgument="MyArg" />
          </bindings>
        </label>
      </components>
    </page>
  </script>

</body>
</html>
