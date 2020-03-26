<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC
"-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>

  <script language="javascript" type="text/javascript">
    Error.duplicateItem = function(e,myitem)
    {
      var a="Sys.DuplicateItemException: " +(e ? e : "Duplicate item!") + "\n";
      if (myitem)
        for (var c in myitem)
          a += (c + ": " + myitem[c] + "\n");
      var f = Error.create(a,{name: "Sys.DuplicateItemException", item: myitem});
      f.popStackFrame();
      return f;
    };

    var products = { "n": { "category": "c", "distributor": "d"} };
    function validateInput(pname, pcategory, pdistributor) {
        //alert(pname);
        if (products[pname]) {
            var err = Error.duplicateItem("Duplicate item!",
                                      { name: pname,
                                          category: pcategory,
                                          distributor: pdistributor
                                      });
            throw err;
        }      
    }
    
    function clickCallback()
    {
      var name = document.getElementById("name");
      var category = document.getElementById("category");
      var distributor = document.getElementById("distributor");
      try
      {
        validateInput(name.value, category.value,
        distributor.value);
        products[name.value] = {name: name.value,
        category: category.value,
        distributor: distributor.value};
      }
      catch (e)
      {
        alert(e.message);
      }
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager runat="server" ID="ScriptManager1" />
  <table style="background-color: LightGoldenrodYellow; border-color: Tan; border-width: 1px;
    color: Black;" cellpadding="2">
    <tr style="background-color: Tan; font-weight: bold">
      <th colspan="2">
        Product Description</th>
    </tr>
    <tr>
      <td align="right">
        Name:</td>
      <td align="left">
        <input type="text" id="name" />
      </td>
    </tr>
    <tr>
      <td align="right">
        Category:</td>
      <td align="left">
        <input type="text" id="category" />
      </td>
    </tr>
    <tr>
      <td align="right">
        Distributor:</td>
      <td align="left">
        <input type="text" id="distributor" />
      </td>
    </tr>
    <tr style="background-color: PaleGoldenrod">
      <td align="center" colspan="2">
        <input type="button" value="Add Product" onclick="clickCallback()" />
      </td>
    </tr>
  </table>
  </form>
</body>
</html>
