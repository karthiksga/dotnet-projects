Type.registerNamespace("CustomComponents");
CustomComponents.Address =
function CustomComponents$Address(street, city, state, zip) {
    this._street = street;
    this._city = city;
    this._state = state;
    this._zip = zip;
}
function CustomComponents$Address$convertToString() {
    return this._street + ", " + this._city + ", " +
this._state + " " + this._zip;
}
CustomComponents.Address.prototype =
{
    convertToString: CustomComponents$Address$convertToString
}
CustomComponents.Address.descriptor =
{
    methods: [{ name: 'convertToString'}],
    attributes: [{ name: 'convertToStringMethodName',
        value: 'convertToString'}]
    }
    CustomComponents.Address.registerClass("CustomComponents.Address");
    if (typeof (Sys) !== 'undefined')
        Sys.Application.notifyScriptLoaded();