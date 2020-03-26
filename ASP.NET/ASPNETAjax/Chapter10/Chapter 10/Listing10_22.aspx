<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>

  <script type="text/javascript" language="javascript">
    function pageLoad()
    {
      var sb = new Sys.StringBuilder();
      sb.append("s1");
      sb.append("s2");
      sb.append("s3");
      sb.append("s4");
      alert(sb.toString(",") + "\n\n" + sb.toString("||"));
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  </form>
</body>
</html>
