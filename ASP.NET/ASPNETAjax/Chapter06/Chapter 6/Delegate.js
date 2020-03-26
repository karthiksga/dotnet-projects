Type.registerNamespace("Delegates");
function Delegates$Mover$invokeAddContentDelegate(addContentDelegate)
{
  addContentDelegate("container1");
}

function mousedowncb(event)
{
  event = event || window.event;
  document.oldClientX = event.clientX;
  document.oldClientY = event.clientY;
  document.onmousemove = mousemovecb;
  document.onmouseup = mouseupcb;
  return false;
}

function mouseupcb(event)
{
  event = event || window.event;
  document.onmousemove = null;
  document.onmouseup = null;
  return false;
}

function mousemovecb(event)
{
  event = event || window.event;
  var deltaClientX = event.clientX - document.oldClientX;
  var deltaClientY = event.clientY - document.oldClientY;
  var container = document.getElementById("container1");
  var containerLocation = Sys.UI.DomElement.getLocation(container);
  Sys.UI.DomElement.setLocation(container,
  containerLocation.x + deltaClientX,
  containerLocation.y + deltaClientY);
  document.oldClientX = event.clientX;
  document.oldClientY = event.clientY;
  return false;
}

function Delegates$TextProvider$addText(containerId)
{
var container = document.getElementById(containerId);
container.innerHTML = '<a href="javascript:void(0)" id="myspan"' +
                      ' style="font-weight: bold">' + this.text + '</a>';
}

function Delegates$ImageProvider$addImage(containerId)
{
  var container = document.getElementById(containerId);
  container.innerHTML = "<img src='" + this.imagePath + "' alt='img' />";
}

Delegates.TextProvider = function (text) {
  this.text = text;
}

Delegates.TextProvider.prototype = {
  addText : Delegates$TextProvider$addText
}

Delegates.TextProvider.registerClass("Delegates.TextProvider");
Delegates.ImageProvider = function (imagePath) {
  this.imagePath = imagePath;
}
Delegates.ImageProvider.prototype = {
  addImage : Delegates$ImageProvider$addImage
}

Delegates.ImageProvider.registerClass("Delegates.ImageProvider");
Delegates.Mover = function () {
  var container = document.getElementById("container1");
  if (!container)
  {
    container = document.createElement("div");
    container.id = "container1";
    container.style.position = "absolute";
    document.body.insertBefore(container, document.forms[0]);
    container.onmousedown = mousedowncb;
  }
}

Delegates.Mover.prototype = {
  invokeAddContentDelegate : Delegates$Mover$invokeAddContentDelegate
}

Delegates.Mover.registerClass("Delegates.Mover");

if(typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();