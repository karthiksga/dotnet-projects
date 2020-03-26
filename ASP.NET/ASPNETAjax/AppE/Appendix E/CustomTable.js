Type.registerNamespace("CustomComponents");
CustomComponents.CustomTable =
function CustomComponents$CustomTable(associatedElement)
{
  CustomComponents.CustomTable.initializeBase(this, [associatedElement]);
  this._fields = [];
}

function CustomComponents$CustomTable$get_fields()
{
  return this._fields;
}

function CustomComponents$CustomTable$get_cssClass()
{
  return this._cssClass;
}

function CustomComponents$CustomTable$set_cssClass(value)
{
  this._cssClass = value;
}

function CustomComponents$CustomTable$get_hoverCssClass()
{
  return this._hoverCssClass;
}

function CustomComponents$CustomTable$set_hoverCssClass(value)
{
  this._hoverCssClass = value;
}

function CustomComponents$CustomTable$get_headerCssClass()
{
  return this._headerCssClass;
}

function CustomComponents$CustomTable$set_headerCssClass(value)
{
  this._headerCssClass = value;
}

function CustomComponents$CustomTable$get_itemCssClass()
{
  return this._itemCssClass;
}

function CustomComponents$CustomTable$set_itemCssClass(value)
{
  this._itemCssClass = value;
}

function CustomComponents$CustomTable$get_alternatingItemCssClass()
{
  return this._alternatingItemCssClass;
}

function CustomComponents$CustomTable$set_alternatingItemCssClass(value)
{
  this._alternatingItemCssClass = value;
}

function CustomComponents$CustomTable$render()
{
  var isArray = true;
  var dataSource = this.get_data();
  if (Sys.Preview.Data.IData.isImplementedBy(dataSource))
    isArray = false;
  else if (!Array.isInstanceOfType(dataSource))
    throw Error.createError('Unknown data source type!');
    
  var table = document.createElement("table");
  if (this._cssClass)
    table.className = this._cssClass;
  var length = isArray ? dataSource.length : dataSource.get_length();
  var dataRow;
  var dataItem;
  var dataCell;
  var index = 0;
  var headerRow;
  var headerCell;
  if (this._fields)
  {
    headerRow = table.insertRow(index);
    if (this._headerCssClass)
      headerRow.className = this._headerCssClass;
    index++;
    for (var c in this._fields)
    {
      headerCell = headerRow.insertCell(c);
      headerCell.innerText = this._fields[c].get_headerText();
    }
  }
  
  this._toggleCssClassHandler =
  Function.createDelegate(this, this._toggleCssClass);
  for (var i=0; i<length; i++)
  {
    dataItem = isArray? dataSource[i] : dataSource.getItem(i);
    if (this._fields)
    {
      dataRow = table.insertRow(index + i);
      $addHandler(dataRow, "mouseover", this._toggleCssClassHandler);
      $addHandler(dataRow, "mouseout", this._toggleCssClassHandler);
      if ((i % 2 === 1) && (this._alternatingItemCssClass))
        dataRow.className = this._alternatingItemCssClass;
      else if (this._itemCssClass)
        dataRow.className = this._itemCssClass;
      for (var c in this._fields)
      {
        dataCell = dataRow.insertCell(c);
        this._fields[c].createInstance(dataCell, dataItem);
      }
    }
  }
  this.get_element().appendChild(table);
}

function CustomComponents$CustomTable$_toggleCssClass(evt)
{
  var s = evt.target;
  while (s && (typeof(s.insertCell) === 'undefined'))
  {
    s = s.parentNode;
  }
  Sys.UI.DomElement.toggleCssClass(s, this._hoverCssClass);
}

CustomComponents.CustomTable.prototype =
{
  render : CustomComponents$CustomTable$render,
  get_cssClass : CustomComponents$CustomTable$get_cssClass,
  set_cssClass : CustomComponents$CustomTable$set_cssClass,
  get_hoverCssClass : CustomComponents$CustomTable$get_hoverCssClass,
  set_hoverCssClass : CustomComponents$CustomTable$set_hoverCssClass,
  get_headerCssClass : CustomComponents$CustomTable$get_headerCssClass,
  set_headerCssClass : CustomComponents$CustomTable$set_headerCssClass,
  get_itemCssClass : CustomComponents$CustomTable$get_itemCssClass,
  set_itemCssClass : CustomComponents$CustomTable$set_itemCssClass,
  get_alternatingItemCssClass : CustomComponents$CustomTable$get_alternatingItemCssClass,
  set_alternatingItemCssClass : CustomComponents$CustomTable$set_alternatingItemCssClass,
  _toggleCssClass : CustomComponents$CustomTable$_toggleCssClass,
  get_fields : CustomComponents$CustomTable$get_fields
}

CustomComponents.CustomTable.registerClass("CustomComponents.CustomTable", Sys.Preview.UI.Data.DataControl);

CustomComponents.CustomTable.descriptor =
{
  properties: [{name: "fields", type: Array, readOnly: true},
               {name: 'cssClass', type: String },
               {name: 'hoverCssClass', type: String },
               {name: 'headerCssClass', type: String },
               {name: 'itemCssClass', type: String },
               {name: 'alternatingItemCssClass', type: String }]
}

if(typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();