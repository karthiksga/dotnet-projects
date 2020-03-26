Type.registerNamespace("CustomComponents");
CustomComponents.Image2 =
function CustomComponents$Image2(associatedElement) {
    if (Sys.Browser.agent != Sys.Browser.InternetExplorer ||
Sys.Browser.version < 4)
        throw Error.invalidOperation;
    CustomComponents.Image2.initializeBase(this, [associatedElement]);
    associatedElement.style.filter = "revealTrans(duration=0.4, transition=1)";
}
function CustomComponents$Image2$set_imageURL(value) {
    this.mouseOutImageURL = value;
    CustomComponents.Image2.callBaseMethod(this, "set_imageURL", [value]);
}
function CustomComponents$Image2$get_mouseOverImageURL() {
    return this.mouseOverImageURL;
}
function CustomComponents$Image2$set_mouseOverImageURL(value) {
    this.mouseOverImageURL = value;
}
function CustomComponents$Image2$mouseOverCallback() {
    this.get_element().filters["revealTrans"].apply();
    this.get_element().src = this.mouseOverImageURL;
    this.get_element().filters["revealTrans"].play();
}
function CustomComponents$Image2$mouseOutCallback() {
    this.get_element().filters["revealTrans"].apply();
    this.get_element().src = this.mouseOutImageURL;
    this.get_element().filters["revealTrans"].play();
}
function CustomComponents$Image2$get_duration() {
    return this.get_element().filters["revealTrans"].duration;
}
function CustomComponents$Image2$set_duration(value) {
    this.get_element().filters["revealTrans"].duration = value;
    this.get_element().filters["revealTrans"].apply();
}
function CustomComponents$Image2$get_transition() {
    return this.get_element().filters["revealTrans"].transition;
}
function CustomComponents$Image2$set_transition(value) {
    this.get_element().filters["revealTrans"].transition = value;
    this.get_element().filters["revealTrans"].apply();
}
function CustomComponents$Image2$initialize() {
    CustomComponents.Image2.callBaseMethod(this, "initialize");
    this.mouseOverDelegate = Function.createDelegate(this,
this.mouseOverCallback);
    this.mouseOutDelegate = Function.createDelegate(this,
this.mouseOutCallback);
    $addHandler(this.get_element(), "mouseover", this.mouseOverDelegate);
    $addHandler(this.get_element(), "mouseout", this.mouseOutDelegate);
}
function CustomComponents$Image2$dispose() {
    $removeHandler(this.get_element(), "mouseover", this.mouseoverDelegate);
    $removeHandler(this.get_element(), "mouseout", this.mouseOutDelegate);
    CustomComponents.Image2.callBaseMethod(this, "dispose");
}
CustomComponents.Image2.prototype =
{
    set_imageURL: CustomComponents$Image2$set_imageURL,
    get_mouseOverImageURL: CustomComponents$Image2$get_mouseOverImageURL,
    set_mouseOverImageURL: CustomComponents$Image2$set_mouseOverImageURL,
    get_duration: CustomComponents$Image2$get_duration,
    set_duration: CustomComponents$Image2$set_duration,
    get_transition: CustomComponents$Image2$get_transition,
    set_transition: CustomComponents$Image2$set_transition,
    mouseOverCallback: CustomComponents$Image2$mouseOverCallback,
    mouseOutCallback: CustomComponents$Image2$mouseOutCallback,
    initialize: CustomComponents$Image2$initialize
}
CustomComponents.Image2.registerClass('CustomComponents.Image2', Sys.Preview.UI.Image);
CustomComponents.Transition = function CustomComponents$Transition() {
    throw Error.notImplemented();
}
CustomComponents.Transition.prototype =
{
    boxIn: 0,
    boxOut: 1,
    circleIn: 2,
    circleOut: 3,
    wipeUp: 4,
    wipeDown: 5,
    wipeRight: 6,
    wipeLeft: 7,
    verticalBlinds: 8,
    horizontalBlinds: 9,
    checkerboardAcross: 10,
    checkerboardDown: 11,
    randomDissolve: 12,
    splitVerticalIn: 13,
    splitVerticalOut: 14,
    splitHorizontalIn: 15,
    splitHorizontalOut: 16,
    stripsLeftDown: 17,
    stripsLeftUp: 18,
    stripsRightDown: 19,
    stripsRightUp: 20,
    randomBarsHorizontal: 22,
    randomBarsVertical: 23,
    randomTransition: 24
}
CustomComponents.Transition.registerEnum("CustomComponents.Transition");
if (typeof (Sys) !== 'undefined')
    Sys.Application.notifyScriptLoaded();