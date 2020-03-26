Type.registerNamespace("CustomComponents3");

CustomComponents3.HtmlGenerator2 = function CustomComponents3$HtmlGenerator2()
{
  CustomComponents3.HtmlGenerator2.initializeBase(this);
}

function CustomComponents3$HtmlGenerator2$generateHtml(xml)
{
  var title;
  var author;
  var amazonUrl;
  var imageUrl;
  var listPrice;
  var price;
  var builder = new Sys.StringBuilder();
  builder.append("<table cellspacing='10' style='");
  builder.append(this._tableStyle);
  builder.append("'>");
  var xmlDocument = new Sys.Net.XMLDOM(xml);
  var items = xmlDocument.documentElement;
  var item = items.firstChild;
  var i = 0;
  
  while (item != null)
  {
    title = item.getAttribute("title");
    author = item.getAttribute("author");
    amazonUrl = item.getAttribute("amazonUrl");
    imageUrl = item.getAttribute("imageUrl");
    listPrice = item.getAttribute("listPrice");
    price = item.getAttribute("price");
    builder.append("<tr>");
    builder.append("<td valign='top' width='100%'>");
    builder.append("<table cellspacing='10' style='");
    if (i % 2 == 0)
      builder.append(this._rowStyle);
    else
      builder.append(this._alternatingRowStyle);
    builder.append("'>");
    builder.append("<tr>");
    builder.append("<td align='center' valign='top' width='20%'>");
    builder.append("<img alt='' src='");
    builder.append(imageUrl);
    builder.append("'/>");
    builder.append("</td>");
    builder.append("<td align='left' valign='top'>");
    builder.append("<p>");
    builder.append("<a href='");
    builder.append(amazonUrl);
    builder.append("'>");
    builder.append(title);
    builder.append("</a>");
    builder.append("<br/>");
    builder.append("by ");
    builder.append(author);
    builder.append(" (Author)");
    builder.append("</p>");
    builder.append("<p>");
    builder.append(" <b>List Price:</b> <s>");
    builder.append(listPrice);
    builder.append("</s></br>");
    builder.append(" <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Price:</b> ");
    builder.append(price);
    builder.append("</br>");
    builder.append("</td>");
    builder.append("</tr>");
    builder.append("<tr>");
    builder.append("<td colspan='2'>");
    builder.append("</td>");
    builder.append("</tr>");
    builder.append("</table>");
    builder.append("</td>");
    builder.append("</tr>");
    item = item.nextSibling;
    i++;
  }
  builder.append("</table>");
  return builder.toString();
}

function CustomComponents3$HtmlGenerator2$initialize()
{
  CustomComponents3.HtmlGenerator2.callBaseMethod(this, "initialize");
}

CustomComponents3.HtmlGenerator2.prototype =
{
  generateHtml: CustomComponents3$HtmlGenerator2$generateHtml,
  initialize: CustomComponents3$HtmlGenerator2$initialize
}

CustomComponents3.HtmlGenerator2.registerClass("CustomComponents3.HtmlGenerator2", CustomComponents3.HtmlGenerator);

if (typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();