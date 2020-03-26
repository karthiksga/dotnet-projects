<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
  <style type="text/css">
    .CssClass1
    {
      background-color: Blue;
      color: Yellow;
      font-weight: bold;
    }
    .CssClass2
    {
      background-color: Yellow;
      color: Blue;
      font-weight: bold;
    }
  </style>

  <script language="javascript" type="text/javascript">
    var myLinkDomElementObj;
    var myList;
    function addCallback()
    {
      var myCssClass = myList.options[myList.selectedIndex].value;
      Sys.UI.DomElement.addCssClass(myLinkDomElementObj, myCssClass);
    }
    
    function removeCallback()
    {
      var myCssClass = myList.options[myList.selectedIndex].value;
      Sys.UI.DomElement.removeCssClass(myLinkDomElementObj, myCssClass);
    }
    
    function pageLoad()
    {
      myLinkDomElementObj = Sys.UI.DomElement.getElementById("myLink");
      myList = document.getElementById("myList");
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />
  <a href="http://www.wrox.com" id="myLink">Wrox Web Site</a>&nbsp;&nbsp;
  <select id="myList">
    <option value="CssClass1">CSS Class 1</option>
    <option value="CssClass2">CSS Class 2</option>
  </select>
  &nbsp;&nbsp;
  <input type="button" value="Add" onclick="addCallback()" />&nbsp;
  <input type="button" value="Remove" onclick="removeCallback()" />
  </form>
</body>
</html>
