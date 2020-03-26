Type.registerNamespace("CustomComponents3");

CustomComponents3.HtmlGenerator = function CustomComponents3$HtmlGenerator()
{
  CustomComponents3.HtmlGenerator.initializeBase(this);
}

function CustomComponents3$HtmlGenerator$get_tableStyle()
{
  return this._tableStyle;
}

function CustomComponents3$HtmlGenerator$set_tableStyle(value)
{
  this._tableStyle = value;
}

function CustomComponents3$HtmlGenerator$get_rowStyle()
{
  return this._rowStyle;
}

function CustomComponents3$HtmlGenerator$set_rowStyle(value)
{
  this._rowStyle = value;
}

function CustomComponents3$HtmlGenerator$get_alternatingRowStyle()
{
  return this._alternatingRowStyle;
}

function CustomComponents3$HtmlGenerator$set_alternatingRowStyle(value)
{
  this._alternatingRowStyle = value;
}

function CustomComponents3$HtmlGenerator$generateHtml(items)
{
  var title;
  var author;
  var amazonUrl;
  var imageUrl;
  var listPrice;
  var price;
  var item;
  var results = items.Item;
  if (!results)
  return;
  var builder = new Sys.StringBuilder();
  builder.append("<table cellspacing='10' style='");
  builder.append(this._tableStyle);
  builder.append("'>");
  
  for (var i=0; i<results.length-1; i++)
  {
    item = results[i];
    if (!item)
      continue;
    if (item.ItemAttributes.Title)
      title = item.ItemAttributes.Title;
    if (item.ItemAttributes.Author)
      author = item.ItemAttributes.Author[0];
    if (item.DetailPageURL)
      amazonUrl = item.DetailPageURL;
    if (item.MediumImage)
      imageUrl = item.MediumImage.URL;
    if (item.ItemAttributes.ListPrice)
      listPrice = item.ItemAttributes.ListPrice.FormattedPrice;
    if (item.Offers)
    {
      var offerArray = item.Offers.Offer;
      if (offerArray)
      {
        if (offerArray[0].OfferListing)
        {
          if (offerArray[0].OfferListing[0].Price)
            price = item.Offers.Offer[0].OfferListing[0].Price.FormattedPrice;
        }
      }
    }
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
  }
  builder.append("</table>");
  return builder.toString();
}

function CustomComponents3$HtmlGenerator$initialize()
{
  CustomComponents3.HtmlGenerator.callBaseMethod(this, "initialize");
}

CustomComponents3.HtmlGenerator.prototype =
{
  get_tableStyle: CustomComponents3$HtmlGenerator$get_tableStyle,
  set_tableStyle: CustomComponents3$HtmlGenerator$set_tableStyle,
  get_rowStyle: CustomComponents3$HtmlGenerator$get_rowStyle,
  set_rowStyle: CustomComponents3$HtmlGenerator$set_rowStyle,
  get_alternatingRowStyle: CustomComponents3$HtmlGenerator$get_alternatingRowStyle,
  set_alternatingRowStyle: CustomComponents3$HtmlGenerator$set_alternatingRowStyle,
  generateHtml: CustomComponents3$HtmlGenerator$generateHtml,
  initialize: CustomComponents3$HtmlGenerator$initialize
}

CustomComponents3.HtmlGenerator.registerClass("CustomComponents3.HtmlGenerator", Sys.Component);

if (typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();