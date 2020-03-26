Type.registerNamespace("CustomComponents");

CustomComponents.TemplateField = function CustomComponents$TemplateField(layoutElement, scriptNode,
                                                                         parentMarkupContext, headerText)
{
  CustomComponents.TemplateField.initializeBase(this, [layoutElement, scriptNode, parentMarkupContext]);
  this._headerText = headerText;
}

function CustomComponents$TemplateField$get_headerText()
{
  return this._headerText;
}

CustomComponents.TemplateField.prototype =
{
  get_headerText : CustomComponents$TemplateField$get_headerText
}

CustomComponents.TemplateField.registerClass("CustomComponents.TemplateField", Sys.Preview.UI.Template);

CustomComponents.TemplateField.parseFromMarkup =
function Sys$Preview$UI$Template$parseFromMarkup(type, node, markupContext)
{
  var layoutElementAttribute = node.attributes.getNamedItem('layoutElement');
  Sys.Debug.assert(!!(layoutElementAttribute && layoutElementAttribute.nodeValue.length),
                   'Missing layoutElement attribute on template definition');
  var layoutElementID = layoutElementAttribute.nodeValue;
  var layoutElement = markupContext.findElement(layoutElementID);
  Sys.Debug.assert(!!layoutElement, String.format('Could not find the HTML element with ID "{0}" associated with the template', layoutElementID));
  var headerTextAttribute = node.attributes.getNamedItem('headerText');
  var headerText = headerTextAttribute.nodeValue;
  return new CustomComponents.TemplateField(layoutElement, node, markupContext, headerText);
}

if(typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();