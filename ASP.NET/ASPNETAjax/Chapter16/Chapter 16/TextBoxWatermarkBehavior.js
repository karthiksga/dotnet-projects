﻿Type.registerNamespace("AjaxControlToolkit");

AjaxControlToolkit.TextBoxWatermarkBehavior = function(element)
{
  /// <summary>
  /// The TextBoxWatermarkBehavior applies a watermark to a textbox
  /// </summary>
  /// <param name="element" type="Sys.UI.DomElement" domElement="true">
  /// Textbox associated with the behavior
  /// </param>
  AjaxControlToolkit.TextBoxWatermarkBehavior.initializeBase(this, [element]);
  
  // Properties
  this._watermarkText = null;
  this._watermarkCssClass = null;
  
  // Member variables
  this._watermarked = null;
  this._focusHandler = null;
  this._blurHandler = null;
  this._keyPressHandler = null;
  this._propertyChangedHandler = null;
  this._oldClassName = null;
  this._clearedForSubmit = null;
  this._maxLength = null;
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$initialize()
{
  /// <summary>
  /// Initialize the behavior
  /// </summary>
  AjaxControlToolkit.TextBoxWatermarkBehavior.callBaseMethod(this, 'initialize');
  var e = this.get_element();
  
  // Determine if this textbox is focused initially
  var hasInitialFocus = false;
  var clientState = AjaxControlToolkit.TextBoxWatermarkBehavior.callBaseMethod(this, 'get_ClientState');
  if (clientState != null && clientState != "")
  {
    hasInitialFocus = (clientState == "Focused");
    AjaxControlToolkit.TextBoxWatermarkBehavior.callBaseMethod(this, 'set_ClientState', null);
  }
  // Capture the initial style so we can toggle back and forth
  // between this and the watermarked style
  this._oldClassName = e.className;
  
  // Create delegates
  this._focusHandler = Function.createDelegate(this, this._onFocus);
  this._blurHandler = Function.createDelegate(this, this._onBlur);
  this._keyPressHandler = Function.createDelegate(this, this._onKeyPress);
  
  // Attach events
  $addHandler(e, 'focus', this._focusHandler);
  $addHandler(e, 'blur', this._blurHandler);
  $addHandler(e, 'keypress', this._keyPressHandler);
  this.registerPropertyChanged();
  
  // Initialize state and simulate a blur to apply the watermark if appropriate
  // Note: The comparison against _watermarkText is undesirable, but seemingly
  // necessary to support the load->Home->Back scenario in IE
  var currentValue = e.value;
  this._watermarked = (("" == currentValue) || (this._watermarkText == currentValue));
  if (hasInitialFocus)
    this._onFocus();
    
  else
  {
    e.blur();
    this._onBlur();
  }
  
  this._clearedForSubmit = false;
  this.registerPartialUpdateEvents();
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$_onFocus(evt)
{
  /// <summary>
  /// Handler for the textbox's focus event
  /// </summary>
  /// <param name="evt" type="Sys.UI.DomEvent">
  /// Event info
  /// </param>
  var e = this.get_element();
  if(this._watermarked)
  {
    // Clear watermark
    this.clearText(evt ? true : false);
  }
  e.className = this._oldClassName;
  this._watermarked = false;
  // Restore the MaxLength on the TextBox when we edit
  // the non-watermarked text
  if (this._maxLength > 0)
  {
    this.get_element().maxLength = this._maxLength;
    this._maxLength = null;
  }
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$clearText(focusing)
{
  /// <summary>
  /// Clear the text from the target
  /// </summary>
  /// <param name="focusing" type="Boolean">
  /// Whether or not we are focusing on the textbox
  /// </param>
  this.get_element().value = "";
  if(focusing)
  {
    // Avoid NS_ERROR_XPC_JS_THREW_STRING error in Firefox
    this.get_element().setAttribute("autocomplete","off");
    // This fix displays the blinking cursor in a focused, empty text box in IE
    this.get_element().select();
  }
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$_onBlur()
{
  /// <summary>
  /// Handle the textbox's blur event
  /// </summary>
  if(("" == this.get_element().value) || this._watermarked)
  {
    // Enlarge the TextBox's MaxLength if it's not big enough
    // to accomodate the watermark
    if (this.get_element().maxLength > 0 && this._watermarkText.length > this.get_element().maxLength)
    {
      this._maxLength = this.get_element().maxLength;
      this.get_element().maxLength = this._watermarkText.length;
    }
    this._applyWatermark();
  }
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$_applyWatermark()
{
  /// <summary>
  /// Apply the watermark to the textbox
  /// </summary>
  this.get_element().value = this._watermarkText;
  if(this._watermarkCssClass)
    this.get_element().className = this._watermarkCssClass;
  this._watermarked = true;
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$_onKeyPress()
{
  /// <summary>
  /// Handle the textbox's keypress event
  /// </summary>
  this._watermarked = false;
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$registerPropertyChanged()
{
  /// <summary>
  /// Method called to hook up to Sys.Preview.UI.TextBox if present
  /// Note: This method must be called manually if the Sys.Preview.UI.TextBox
  /// is added after the TextBoxWatermarkBehavior is initialized.
  /// </summary>
  var e = this.get_element();
  if(e.control && !this._propertyChangedHandler)
  {
    this._propertyChangedHandler =
    Function.createDelegate(this, this._onPropertyChanged);
    e.control.add_propertyChanged(this._propertyChangedHandler);
  }
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$_onPropertyChanged(sender, propertyChangedEventArgs)
{
  /// <summary>
  /// Handler called automatically when a property change event is fired
  /// </summary>
  /// <param name="sender" type="Object">
  /// Sender
  /// </param>
  /// <param name="propertyChangedEventArgs" type="Sys.PropertyChangedEventArgs">
  /// Event arguments
  /// </param>
  if("text" == propertyChangedEventArgs.get_propertyName())
    this.set_Text(this.get_element().value);
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$set_Text(value)
{
  if ("" == value)
  {
    this.get_element().value = "";
    this.get_element().blur();
    this._onBlur(); // onBlur needs to see ""
  }
  
  else
  {
    this._onFocus(); // onFocus sets ""
    this.get_element().value = value;
  }
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$get_Text()
{
  /// <value type="String">
  /// Wrapper for the textbox's text that will ignore or create the
  /// watermark as appropriate
  /// </value>
  return (this._watermarked ? "" : this.get_element().value);
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$_partialUpdateEndRequest(sender, endRequestEventArgs)
{
  /// <summary>
  /// Handler Called automatically when a partial postback ends
  /// </summary>
  /// <param name="sender" type="Object">
  /// Sender
  /// </param>
  /// <param name="endRequestEventArgs" type="Sys.WebForms.EndRequestEventArgs">
  /// Event arguments
  /// </param>
  AjaxControlToolkit.TextBoxWatermarkBehavior.callBaseMethod(this, '_partialUpdateEndRequest', [sender, endRequestEventArgs]);
  if (this.get_element() && this._clearedForSubmit)
  {
    // Restore the cleared watermark (useful when the submit was
    // wrapped in an UpdatePanel)
    this.get_element().blur();
    this._onBlur();
    this._clearedForSubmit = false;
  }
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$_onSubmit()
{
  /// <summary>
  /// Handler Called automatically when a submit happens to clear the
  /// watermark before posting back
  /// </summary>
  if(this._watermarked)
  {
    // Clear watermark text before page is submitted
    this.clearText(false);
    this._clearedForSubmit = true;
  }
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$dispose()
{
  /// <summary>
  /// Dispose the behavior
  /// </summary>
  var e = this.get_element();
  
  // Unhook from Sys.Preview.UI.TextBox if present
  if(e.control && this._propertyChangedHandler)
  {
    e.control.remove_propertyChanged(this._propertyChangedHandler);
    this._propertyChangedHandler = null;
  }
  
  // Detach events
  if (this._focusHandler)
  {
    $removeHandler(e, 'focus', this._focusHandler);
    this._focusHandler = null;
  }

  if (this._blurHandler)
  {
    $removeHandler(e, 'blur', this._blurHandler);
    this._blurHandler = null;
  }
  
  if (this._keyPressHandler)
  {
    $removeHandler(e, 'keypress', this._keyPressHandler);
    this._keyPressHandler = null;
  }
  
  // Clear watermark text to avoid confusion during Refresh/Back/Forward
  if(this._watermarked)
    this.clearText(false);
  AjaxControlToolkit.TextBoxWatermarkBehavior.callBaseMethod(this, 'dispose');
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$get_WatermarkText()
{
  /// <value type="String">
  /// The text to show when the control has no value
  /// </value>
  return this._watermarkText;
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$set_WatermarkText(value)
{
  if (this._watermarkText != value)
  {
    this._watermarkText = value;
    if (this._watermarked)
      this._applyWatermark();
    this.raisePropertyChanged('WatermarkText');
  }
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$get_WatermarkCssClass()
{
  /// <value type="String">
  /// The CSS class to apply to the TextBox when it has no value (e.g. the
  /// watermark text is shown).
  /// </value>
  return this._watermarkCssClass;
}

function AjaxControlToolkit$TextBoxWatermarkBehavior$set_WatermarkCssClass(value)
{
  if (this._watermarkCssClass != value)
  {
    this._watermarkCssClass = value;
    if (this._watermarked)
      this._applyWatermark();
    this.raisePropertyChanged('WatermarkCssClass');
  }
}

AjaxControlToolkit.TextBoxWatermarkBehavior.prototype =
{
  initialize : AjaxControlToolkit$TextBoxWatermarkBehavior$initialize,
  dispose : AjaxControlToolkit$TextBoxWatermarkBehavior$dispose,
  clearText : AjaxControlToolkit$TextBoxWatermarkBehavior$clearText,
  _onFocus : AjaxControlToolkit$TextBoxWatermarkBehavior$_onFocus,
  _onBlur : AjaxControlToolkit$TextBoxWatermarkBehavior$_onBlur,
  _applyWatermark : AjaxControlToolkit$TextBoxWatermarkBehavior$_applyWatermark,
  _onKeyPress : AjaxControlToolkit$TextBoxWatermarkBehavior$_onKeyPress,
  registerPropertyChanged : AjaxControlToolkit$TextBoxWatermarkBehavior$registerPropertyChanged,
  _onPropertyChanged : AjaxControlToolkit$TextBoxWatermarkBehavior$_onPropertyChanged,
  _onSubmit : AjaxControlToolkit$TextBoxWatermarkBehavior$_onSubmit,
  _partialUpdateEndRequest : AjaxControlToolkit$TextBoxWatermarkBehavior$_partialUpdateEndRequest,
  get_WatermarkText : AjaxControlToolkit$TextBoxWatermarkBehavior$get_WatermarkText,
  set_WatermarkText : AjaxControlToolkit$TextBoxWatermarkBehavior$set_WatermarkText,
  get_WatermarkCssClass : AjaxControlToolkit$TextBoxWatermarkBehavior$get_WatermarkCssClass,
  set_WatermarkCssClass : AjaxControlToolkit$TextBoxWatermarkBehavior$set_WatermarkCssClass,
  get_Text : AjaxControlToolkit$TextBoxWatermarkBehavior$get_Text,
  set_Text : AjaxControlToolkit$TextBoxWatermarkBehavior$set_Text
}

AjaxControlToolkit.TextBoxWatermarkBehavior.registerClass('AjaxControlToolkit.TextBoxWatermarkBehavior',
                                                          AjaxControlToolkit.BehaviorBase);
                                                          
if(typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();