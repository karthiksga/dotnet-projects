<%@ Page Language="C#" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script language="JavaScript" type="text/javascript">
    function multiply(val,index,ar)
    {
      ar[index] = val * this.get_c();
    }
    
    function myClass(c)
    {
      this.c = c;
      this.get_c = function ()
      {
        return this.c;
      };
    }
    
    function pageLoad() {
      var a = [1, 2, 3, 4];
      var myObj = new myClass(6);
      Array.forEach(a, multiply, myObj);
      for (var j = 0; j<a.length; j++)
        alert(a[j]);
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  </form>
</body>
</html>
