<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
  <style type="text/css">
    .itemCssClass
    {
      background-color: #eeeeee;
    }
    .alternatingItemCssClass
    {
      background-color: #bbbbbb;
    }
    .selectedItemCssClass
    {
      background-color: #777777;
      color: #ffffff;
    }
  </style>

  <script language="javascript" type="text/javascript">
    function onSuccess(result, userContext, methodName)
    {
      userContext.set_data(result);
      if (firstTime)
      {
        firstTime = false;
        selectionChangedCallback(userContext);
      }    }
    
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
    
    function selectionChangedCallback(sender, eventArgs)
    {
      var authorID = sender.get_selectedValue();
      var listView = Sys.Application.findComponent("listView");
      MyWebService.GetBooks(authorID, onSuccess, onFailure, listView);
    }
    
    var firstTime = true;
    function pageLoad()
    {
      var listView = Sys.Application.findComponent("listView");
      listView.set_itemCssClass("itemCssClass");
      listView.set_alternatingItemCssClass("alternatingItemCssClass");
      listView.set_selectedItemCssClass("selectedItemCssClass");
      var authorList = Sys.Application.findComponent("authorList");
      if (!authorList.get_data())
        MyWebService.GetAuthors(onSuccess, onFailure, authorList);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
      <asp:ServiceReference InlineScript="true" Path="WebService.asmx" />
    </Services>
    <Scripts>
      <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
    </Scripts>
  </asp:ScriptManager>
  <center>
    <b>Select an author: </b>
    <select id="authorList">
    </select>
    <br />
    <br />
  </center>
  <div id="listView" />
  <div style="display: none;">
    <div id="layout">
      <table width="100%">
        <tr>
          <th style="background-color: Tan">
            <b>Title, <i>Publisher</i>, <i>Price</i></b>
          </th>
        </tr>
        <tr>
          <td>
            <ul id="itemContainer">
              <li id="item"><b><span id="title"></span>,</b> <i><span id="publisher"></span></i>
                , <i><span id="price"></span></i></li>
            </ul>
          </td>
        </tr>
      </table>
    </div>
  </div>
 </form>

  <script type="text/xml-script">
    <page xmlns="http://schemas.microsoft.com/xml-script/2005">
      <components>
        <selector id="authorList" textProperty="AuthorName"
        valueProperty="AuthorID" selectionChanged="selectionChangedCallback" />
      
        <listView id="listView" itemTemplateParentElementId="itemContainer">
          <layoutTemplate>
            <template layoutElement="layout"/>
          </layoutTemplate>
          
          <itemTemplate>
            <template layoutElement="item">
              <label id="title">
                <bindings>
                  <binding dataPath="Title" property="text" />
                </bindings>
              </label>
              <label id="publisher">
                <bindings>
                  <binding dataPath="Publisher" property="text" />
                </bindings>
              </label>
              <label id="price">
                <bindings>
                  <binding dataPath="Price" property="text" transform="ToString"
                  transformerArgument="${0}" />
                </bindings>
              </label>
            </template>
          </itemTemplate>
        </listView>
      </components>
    </page>
  </script>

</body>
</html>
