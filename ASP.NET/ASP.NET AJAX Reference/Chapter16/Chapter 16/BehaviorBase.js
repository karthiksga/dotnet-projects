Type.registerNamespace("AjaxControlToolkit");

AjaxControlToolkit.BehaviorBase = function(element)
{
  /// <summary>
  /// Base behavior for all extender behaviors
  /// </summary>
  /// <param name=”element” type=”Sys.UI.DomElement” domElement=”true”>
  /// Element the behavior is associated with
  AjaxControlToolkit.BehaviorBase.initializeBase(this,[element]);
  this._clientStateFieldID = null;
  this._pageRequestManager = null;
  this._partialUpdateBeginRequestHandler = null;
  this._partialUpdateEndRequestHandler = null;
}

function AjaxControlToolkit$BehaviorBase$initialize ()
{
  /// <summary>
  /// Initialize the behavior
  /// </summary>
  AjaxControlToolkit.BehaviorBase.callBaseMethod(this, 'initialize');
}

function AjaxControlToolkit$BehaviorBase$get_ClientStateFieldID ()
{
  /// <value type=”String”>
  /// ID of the hidden field used to store client state
  /// </value>
  return this._clientStateFieldID;
}

function AjaxControlToolkit$BehaviorBase$set_ClientStateFieldID (value)
{
  if (this._clientStateFieldID != value)
  {
    this._clientStateFieldID = value;
    this.raisePropertyChanged('ClientStateFieldID');
  }
}

function AjaxControlToolkit$BehaviorBase$get_ClientState ()
{
  /// <value type=”String”>
  /// Client state
  /// </value>
  if (this._clientStateFieldID)
  {
    var input = document.getElementById(this._clientStateFieldID);
    if (input)
      return input.value;
  }
  return null;
}

function AjaxControlToolkit$BehaviorBase$set_ClientState (value)
{
  if (this._clientStateFieldID)
  {
    var input = document.getElementById(this._clientStateFieldID);
    if (input)
      input.value = value;
  }
}

function AjaxControlToolkit$BehaviorBase$registerPartialUpdateEvents ()
{
  /// <summary>
  /// Register for beginRequest and endRequest events on the PageRequestManager,
  /// (which cause _partialUpdateBeginRequest and _partialUpdateEndRequest to be
  /// called when an UpdatePanel refreshes)
  /// </summary>
  if (Sys && Sys.WebForms && Sys.WebForms.PageRequestManager)
  {
    this._pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();
    if (this._pageRequestManager)
    {
      this._partialUpdateBeginRequestHandler = Function.createDelegate(this, this._partialUpdateBeginRequest);
      this._pageRequestManager.add_beginRequest(this._partialUpdateBeginRequestHandler);
      this._partialUpdateEndRequestHandler = Function.createDelegate(this, this._partialUpdateEndRequest);
      this._pageRequestManager.add_endRequest(this._partialUpdateEndRequestHandler);
    }
  }
}

function AjaxControlToolkit$BehaviorBase$_partialUpdateBeginRequest(sender, beginRequestEventArgs)
{
  /// <summary>
  /// Method that will be called when a partial update (via an UpdatePanel) begins,
  /// if registerPartialUpdateEvents() has been called.
  /// </summary>
  /// <param name=”sender” type=”Object”>
  /// Sender
  /// </param>
  /// <param name=”beginRequestEventArgs”
  /// type=”Sys.WebForms.BeginRequestEventArgs”>
  /// Event arguments
  /// </param>
  // Nothing done here; override this method in a child class
}

function AjaxControlToolkit$BehaviorBase$_partialUpdateEndRequest(sender, endRequestEventArgs)
{
  /// <summary>
  /// Method that will be called when a partial update (via an UpdatePanel)
  /// finishes,
  /// if registerPartialUpdateEvents() has been called.
  /// </summary>
  /// <param name=”sender” type=”Object”>
  /// Sender
  /// </param>
  /// <param name=”endRequestEventArgs” type=”Sys.WebForms.EndRequestEventArgs”>
  /// Event arguments
  /// </param>
  // Nothing done here; override this method in a child class
}

function AjaxControlToolkit$BehaviorBase$dispose ()
{
  /// <summary>
  /// Dispose the behavior
  /// </summary>
  AjaxControlToolkit.BehaviorBase.callBaseMethod(this, 'dispose');
  if (this._pageRequestManager)
  {
    if (this._partialUpdateBeginRequestHandler)
    {
      this._pageRequestManager.remove_beginRequest(this._partialUpdateBeginRequestHandler);
      this._partialUpdateBeginRequestHandler = null;
    }
    
    if (this._partialUpdateEndRequestHandler)
    {
      this._pageRequestManager.remove_endRequest(this._partialUpdateEndRequestHandler);
      this._partialUpdateEndRequestHandler = null;
    }
    
    this._pageRequestManager = null;
  }
}

AjaxControlToolkit.BehaviorBase.prototype =
{
  initialize : AjaxControlToolkit$BehaviorBase$initialize,
  dispose : AjaxControlToolkit$BehaviorBase$dispose,
  get_ClientStateFieldID : AjaxControlToolkit$BehaviorBase$get_ClientStateFieldID,
  set_ClientStateFieldID : AjaxControlToolkit$BehaviorBase$set_ClientStateFieldID,
  get_ClientState : AjaxControlToolkit$BehaviorBase$get_ClientState,
  set_ClientState : AjaxControlToolkit$BehaviorBase$set_ClientState,
  registerPartialUpdateEvents : AjaxControlToolkit$BehaviorBase$registerPartialUpdateEvents,
  _partialUpdateBeginRequest : AjaxControlToolkit$BehaviorBase$_partialUpdateBeginRequest,
  _partialUpdateEndRequest : AjaxControlToolkit$BehaviorBase$_partialUpdateBeginRequest
}

AjaxControlToolkit.BehaviorBase.registerClass("AjaxControlToolkit.BehaviorBase", Sys.UI.Behavior);

if(typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();
