﻿<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
  <style type="text/css">
    .cssClass
    {
      background-color: LightGoldenrodYellow;
      border-color: Tan;
      border-width: 1px;
    }
    .headerCssClass
    {
      background-color: Tan;
      font-weight: bold;
    }
    .alternatingItemCssClass
    {
      background-color: PaleGoldenrod;
    }
    .hoverCssClass
    {
      background-color: DarkSlateBlue;
      color: GhostWhite;
    }
  </style>

  <script type="text/javascript" language="javascript">
    function onSuccess(result, userContext, methodName)
    {
      userContext.set_data(result);
    }
    
    function onFailure(result, userContext, methodName)
    {
      var builder = new Sys.StringBuilder();
      builder.append("timedOut: ");
      builder.append(result.get_timedOut());
      builder.appendLine();
      builder.appendLine();
      builder.append("message: ");
      builder.append(result.get_message());
      builder.appendLine();
      builder.appendLine();
      builder.append("stackTrace: ");
      builder.appendLine();
      builder.append(result.get_stackTrace());
      builder.appendLine();
      builder.appendLine();
      builder.append("exceptionType: ");
      builder.append(result.get_exceptionType());
      builder.appendLine();
      builder.appendLine();
      builder.append("statusCode: ");
      builder.append(result.get_statusCode());
      builder.appendLine();
      builder.appendLine();
      builder.append("methodName: ");
      builder.append(methodName);
      alert(builder.toString());
    }
    
    function pageLoad()
    {
      var customTable = Sys.Application.findComponent("customTable");
      MyWebService.GetBooks(onSuccess, onFailure, customTable);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1">
    <Services>
      <asp:ServiceReference InlineScript="true" Path="WebService.asmx" />
    </Services>
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
      <asp:ScriptReference Path="CustomTable.js" />
      <asp:ScriptReference Path="TemplateField.js" />
    </Scripts>
  </asp:ScriptManager>
  <div id="customTable" />
  <div style="display: none;">
    <div id="field1">
      <span id="title"></span>
    </div>
    <div id="field2">
      <span id="publisher"></span>
    </div>
    <div id="field3">
      <span id="price"></span>
    </div>
  </div>
  </form>

  <script type="text/xml-script">
    <page xmlns="http://schemas.microsoft.com/xml-script/2005"
    xmlns:custom="CustomComponents">
      <components>
        <custom:CustomTable id="customTable" cssClass="cssClass"
        headerCssClass="headerCssClass" hoverCssClass="hoverCssClass"
        alternatingItemCssClass="alternatingItemCssClass" >
          <fields>
            <custom:TemplateField layoutElement="field1" headerText="Title">
              <label id="title">
                <bindings>
                  <binding dataPath="Title" property="text" />
                </bindings>
              </label>
            </custom:TemplateField>
            
            <custom:TemplateField layoutElement="field2" headerText="Publisher">
              <label id="publisher">
                <bindings>
                  <binding dataPath="Publisher" property="text" />
                </bindings>
              </label>
            </custom:TemplateField>
            
            <custom:TemplateField layoutElement="field3" headerText="Price">
              <label id="price">
                <bindings>
                  <binding dataPath="Price" property="text" transform="ToString"
                  transformerArgument="${0}" />
                </bindings>
              </label>
            </custom:TemplateField>
          </fields>
        </custom:CustomTable>
      </components>
    </page>
  </script>
</body>
</html>