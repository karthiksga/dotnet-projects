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
};

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
  var events = {mousemove: this.mousemovecb, mouseup: this.mouseupcb};
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
  Sys.UI.DomElement.setLocation(container, containerLocation.x + deltaClientX,
                                containerLocation.y + deltaClientY);
  this.oldClientX = domEvent.clientX;
  this.oldClientY = domEvent.clientY;
  domEvent.preventDefault();
}

Delegates.UpdatePanelProvider = function (updatePanel)
{
  this.updatePanel = updatePanel;
}

Delegates.UpdatePanelProvider.prototype =
{
  addUpdatePanel : Delegates$UpdatePanelProvider$addUpdatePanel
}

function Delegates$UpdatePanelProvider$addUpdatePanel(containerId)
{
  var container = $get(containerId);
  container.appendChild(this.updatePanel);
}

Delegates.Mover.registerClass("Delegates.Mover");
Delegates.UpdatePanelProvider.registerClass("Delegates.UpdatePanelProvider");

if (typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();