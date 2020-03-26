Type.registerNamespace("CustomComponents");
CustomComponents.MyType2 =
function CustomComponents$MyType2(param1, param2, param3)
{
this._param1 = param1;
this._param2 = param2;
this._param3 = param3;
}
CustomComponents.MyType2.registerClass("CustomComponents.MyType2");
CustomComponents.MyType2.parse = function (value)
{
var params = value.split(',');
alert("Instantiating a MyType2 object and initializing it with " + params);
return new CustomComponents.MyType2(params[0], params[1], params[2]);
}
///////////////////////////////
CustomComponents.MyEnumeration = function CustomComponents$MyEnumeration()
{
throw Error.invalidOperation();
}
CustomComponents.MyEnumeration.prototype =
{
EnumValue1: 0,
EnumValue2: 1,
EnumValue3: 2
}
CustomComponents.MyEnumeration.registerEnum("CustomComponents.MyEnumeration");
//////////////////////////////
CustomComponents.MyType = function CustomComponents$MyType()
{
}
function CustomComponents$MyType$set_myTypeProperty(value)
{
this._myTypeProperty = value;
alert("myTypeProperty was set to " + value);
}
function CustomComponents$MyType$get_myTypeProperty()
{
return this._myTypeProperty;
}
CustomComponents.MyType.prototype =
{
get_myTypeProperty : CustomComponents$MyType$get_myTypeProperty,
set_myTypeProperty : CustomComponents$MyType$set_myTypeProperty
}
CustomComponents.MyType.registerClass("CustomComponents.MyType");
CustomComponents.MyType.descriptor =
{
properties : [{name : "myTypeProperty", type : String}]
}
//////////////////////////////
CustomComponents.MyCustomType = function CustomComponents$MyCustomType()
{
CustomComponents.MyCustomType.initializeBase(this);
}
function CustomComponents$MyCustomType$add_myEvent(eventHandler)
{
this.get_events().addHandler("myEvent", eventHandler);
alert(eventHandler + " \n\nwas registered as event handler for myEvent event!");
}
function CustomComponents$MyCustomType$remove_myEvent(eventHandler)
{
this.get_events().removeHandler("myEvent", eventHandler);
}
function CustomComponents$MyCustomType$set_myProperty(value)
{
this._myProperty = value;
alert("myProperty was set to the DOM element with id HTML attribute value of " +
value.id);
}
function CustomComponents$MyCustomType$get_myProperty()
{
return this._myProperty;
}
function CustomComponents$MyCustomType$set_myNonReadOnlyStringProperty(value)
{
this._myNonReadOnlyStringProperty = value;
alert("myNonReadOnlyStringProperty was set to " + value);
}
function CustomComponents$MyCustomType$get_myNonReadOnlyStringProperty()
{
return this._myNonReadOnlyStringProperty;
}
function CustomComponents$MyCustomType$set_myProperty2(value)
{
this._myProperty2 = value;
alert("myProperty2 was set to " + value);
}
function CustomComponents$MyCustomType$get_myProperty2()
{
return this._myProperty2;
}
function CustomComponents$MyCustomType$set_myReferenceProperty(value)
{
this._myReferenceProperty = value;
alert("myReferenceProperty was set to the component with the id value of " +
value.get_id());
}
function CustomComponents$MyCustomType$get_myReferenceProperty()
{
return this._myReferenceProperty;
}
function CustomComponents$MyCustomType$set_myArrayProperty(value)
{
this._myArrayProperty = value;
alert("myArrayProperty was set to " + value);
}
function CustomComponents$MyCustomType$get_myArrayProperty()
{
return this._myArrayProperty;
}
function CustomComponents$MyCustomType$get_myReadOnlyArrayProperty()
{
alert("The value of myReadOnlyArrayProperty is being retrieved!");
return this._myReadOnlyArrayProperty;
}
function CustomComponents$MyCustomType$get_myObjectProperty()
{
alert("The value of myObjectProperty is being retrieved!");
return this._myObjectProperty;
}
function CustomComponents$MyCustomType$get_myNonObjectNonArrayProperty()
{
alert("The value of myNonObjectNonArrayProperty is being retrieved!");
if (!this._myNonObjectNonArrayProperty)
this._myNonObjectNonArrayProperty = new CustomComponents.MyType();
return this._myNonObjectNonArrayProperty;
}
function CustomComponents$MyCustomType$set_myEnumProperty(value)
{
this._myEnumProperty = value;
alert("myEnumProperty was set to " + value);
}
function CustomComponents$MyCustomType$get_myEnumProperty()
{
return this._myEnumProperty;
}
CustomComponents.MyCustomType.prototype =
{
_myReadOnlyArrayProperty : [],
_myObjectProperty : {},
set_myProperty : CustomComponents$MyCustomType$set_myProperty,
get_myProperty : CustomComponents$MyCustomType$get_myProperty,
set_myNonReadOnlyStringProperty :
CustomComponents$MyCustomType$set_myNonReadOnlyStringProperty,
get_myNonReadOnlyStringProperty :
CustomComponents$MyCustomType$get_myNonReadOnlyStringProperty,
set_myProperty2 : CustomComponents$MyCustomType$set_myProperty2,
get_myProperty2 : CustomComponents$MyCustomType$get_myProperty2,
set_myReferenceProperty : CustomComponents$MyCustomType$set_myReferenceProperty,
get_myReferenceProperty : CustomComponents$MyCustomType$get_myReferenceProperty,
set_myArrayProperty : CustomComponents$MyCustomType$set_myArrayProperty,
get_myArrayProperty : CustomComponents$MyCustomType$get_myArrayProperty,
set_myEnumProperty : CustomComponents$MyCustomType$set_myEnumProperty,
get_myEnumProperty : CustomComponents$MyCustomType$get_myEnumProperty,
get_myReadOnlyArrayProperty :
CustomComponents$MyCustomType$get_myReadOnlyArrayProperty,
get_myObjectProperty : CustomComponents$MyCustomType$get_myObjectProperty,
get_myNonObjectNonArrayProperty :
CustomComponents$MyCustomType$get_myNonObjectNonArrayProperty,
add_myEvent : CustomComponents$MyCustomType$add_myEvent,
remove_myEvent : CustomComponents$MyCustomType$remove_myEvent
}
CustomComponents.MyCustomType.registerClass("CustomComponents.MyCustomType",
Sys.Component);
CustomComponents.MyCustomType.descriptor =
{
properties : [{name : 'myProperty', type : null, isDomElement : true},
{name : 'myNonReadOnlyStringProperty', type : String},
{name : 'myReferenceProperty',
type : CustomComponents.MyCustomType},
{name : 'myProperty2', type : CustomComponents.MyType2},
{name : 'myArrayProperty', type : Array},
{name : 'myEnumProperty', type : CustomComponents.MyEnumeration},
{name : 'myReadOnlyArrayProperty', type : Array, readOnly : true},
{name : 'myObjectProperty', type : Object, readOnly : true},
{name : 'myNonObjectNonArrayProperty',
type : CustomComponents.MyType, readOnly : true}],
events : [{name : "myEvent"}]
}
if(typeof(Sys)!=='undefined')
Sys.Application.notifyScriptLoaded();