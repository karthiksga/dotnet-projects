Type.registerNamespace("Delegates");

Delegates.Mover = function (containerId)
{
  var container = $get(containerId);
  Delegates.Mover.incrementMoversCount();
  if (!container)
  {
    container = document.createElement("div");
    this.containerId = container.id = containerId;
    container.style.position = "absolute";
    document.body.insertBefore(container, document.forms[0]);
    $addHandlers(container, { mousedown: this.mousedowncb }, this);
  }
}

Delegates.Mover.prototype =
{
  addContent : Delegates$Mover$invokeAddContentDelegate,
  mousedowncb : Delegates$Mover$mousedowncb,
  mouseupcb : Delegates$Mover$mouseupcb,
  mousemovecb : Delegates$Mover$mousemovecb
}

Delegates.Mover.incrementMoversCount = function()
{
  if (typeof(this.moversCount) == "undefined")
    this.moversCount = 0;
  this.moversCount++;
}

Delegates.Mover.get_moversCount = function()
{
  return this.moversCount;
}

function Delegates$Mover$invokeAddContentDelegate(addContentDelegate)
{
  addContentDelegate(this.containerId);
}

function Delegates$Mover$mousedowncb(domEvent)
{
  var container = $get(this.containerId);
  this.oldClientX = domEvent.clientX;
  this.oldClientY = domEvent.clientY;
  var events = {mousemove: this.mousemovecb, mouseup: this.mouseupcb}
  $addHandlers(document, events, this);
  container.style.zIndex += Delegates.Mover.get_moversCount();
  domEvent.preventDefault();
}

function Delegates$Mover$mouseupcb(domEvent)
{
  var container = $get(this.containerId);
  $clearHandlers(document);
  container.style.zIndex -= Delegates.Mover.get_moversCount();
  domEvent.preventDefault();
}

function Delegates$Mover$mousemovecb(domEvent)
{
  var container = $get(this.containerId);
  var deltaClientX = domEvent.clientX - this.oldClientX;
  var deltaClientY = domEvent.clientY - this.oldClientY;
  var containerLocation = Sys.UI.DomElement.getLocation(container);
  Sys.UI.DomElement.setLocation(container, containerLocation.x+deltaClientX,
  containerLocation.y+deltaClientY);
  this.oldClientX = domEvent.clientX;
  this.oldClientY = domEvent.clientY;
  domEvent.preventDefault();
}

Delegates.TableProvider = function (headers, rows)
{
  this.headers = headers;
  this.rows = rows;
}

Delegates.TableProvider.prototype =
{
  addTable : Delegates$TableProvider$addTable
}

function Delegates$TableProvider$addTable(containerId)
{
  var container = $get(containerId);
  var table = document.createElement("table");
  Sys.UI.DomElement.addCssClass(table, "myTable");
  var headerRow = table.insertRow(0);
  Sys.UI.DomElement.addCssClass(headerRow, "header");
  
  function renderHeaderCell(dataFieldName, cellIndex, dataFieldNames)
  {
    var headerCell = document.createElement("th");
    headerCell.appendChild(document.createTextNode(dataFieldName));
    headerRow.appendChild(headerCell);
  };
  
  function renderDataCell(dataFieldValue, index, dataFieldValues)
  {
    var dataCell = row.insertCell(row.cells.length);
    dataCell.appendChild(document.createTextNode(dataFieldValue));
  };
  
  Array.forEach(this.headers, renderHeaderCell);
  for (var rowIndex in this.rows)
  {
    var row = table.insertRow(table.rows.length);
    if (rowIndex % 2 == 1)
      Sys.UI.DomElement.addCssClass(row, "odd");
    Array.forEach(this.rows[rowIndex], renderDataCell);
  }
  container.appendChild(table);
}

Delegates.TextProvider = function (text)
{
  this.text = text;
}

Delegates.TextProvider.prototype =
{
  addText : Delegates$TextProvider$addText
}

Delegates.ImageProvider = function (imagePath)
{
  this.imagePath = imagePath;
}

Delegates.ImageProvider.prototype =
{
  addImage : Delegates$ImageProvider$addImage
}

function Delegates$TextProvider$addText(containerId)
{
  var container = $get(containerId);
  container.innerHTML = '<a href="javascript:void(0);" id="myspan"' +
                        ' style="font-weight:bold">' +
  this.text + '</a>';
}

function Delegates$ImageProvider$addImage(containerId)
{
  var container = $get(containerId);
  container.innerHTML = "<img src='" + this.imagePath + "' alt='img' />";
}

Delegates.Mover.registerClass("Delegates.Mover");
Delegates.TextProvider.registerClass("Delegates.TextProvider");
Delegates.ImageProvider.registerClass("Delegates.ImageProvider");
Delegates.TableProvider.registerClass("Delegates.TableProvider");

if (typeof(Sys) !== 'undefined') 
  Sys.Application.notifyScriptLoaded();