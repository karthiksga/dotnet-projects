Type.registerNamespace("CustomComponents3");

CustomComponents3.AspNetAjaxAmazonSearch =
function CustomComponents3$AspNetAjaxAmazonSearch(associatedElement)
{
  CustomComponents3.AspNetAjaxAmazonSearch.initializeBase(this, [associatedElement]);
}

function CustomComponents3$AspNetAjaxAmazonSearch$get_searchTextBox()
{
  return this._searchTextBox;
}

function CustomComponents3$AspNetAjaxAmazonSearch$set_searchTextBox(value)
{
  this._searchTextBox = value;
}

function CustomComponents3$AspNetAjaxAmazonSearch$get_searchButton()
{
  return this._searchButton;
}

function CustomComponents3$AspNetAjaxAmazonSearch$set_searchButton(value)
{
  this._searchButton = value;
}

function CustomComponents3$AspNetAjaxAmazonSearch$get_htmlGenerator()
{
  return this._htmlGenerator;
}

function CustomComponents3$AspNetAjaxAmazonSearch$set_htmlGenerator(value)
{
  this._htmlGenerator = value;
}

function CustomComponents3$AspNetAjaxAmazonSearch$get_searchResultAreaDiv()
{
  return this._searchResultAreaDiv;
}

function CustomComponents3$AspNetAjaxAmazonSearch$set_searchResultAreaDiv(value)
{
  this._searchResultAreaDiv = value;
}

function CustomComponents3$AspNetAjaxAmazonSearch$get_commandBarAreaDiv()
{
  return this._commandBarAreaDiv;
}

function CustomComponents3$AspNetAjaxAmazonSearch$set_commandBarAreaDiv(value)
{
  this._commandBarAreaDiv = value;
}

function CustomComponents3$AspNetAjaxAmazonSearch$get_nextButton()
{
  return this._nextButton;
}

function CustomComponents3$AspNetAjaxAmazonSearch$set_nextButton(value)
{
  this._nextButton = value;
}

function CustomComponents3$AspNetAjaxAmazonSearch$get_previousButton()
{
  return this._previousButton;
}

function CustomComponents3$AspNetAjaxAmazonSearch$set_previousButton(value)
{
  this._previousButton = value;
}

function CustomComponents3$AspNetAjaxAmazonSearch$get_pageIndex()
{
  return this._pageIndex;
}

function CustomComponents3$AspNetAjaxAmazonSearch$set_pageIndex(value)
{
  this._pageIndex = value;
}

function CustomComponents3$AspNetAjaxAmazonSearch$get_searchMethod()
{
  return this._searchMethod;
}

function CustomComponents3$AspNetAjaxAmazonSearch$set_searchMethod(value)
{
  this._searchMethod = value;
}

function CustomComponents3$AspNetAjaxAmazonSearch$initialize()
{
  CustomComponents3.AspNetAjaxAmazonSearch.callBaseMethod(this, "initialize");
  this._searchButtonClickHandler = Function.createDelegate(this, this._onSearchButtonClick);
  this._nextButtonClickHandler = Function.createDelegate(this, this._onNextButtonClick);
  this._previousButtonClickHandler = Function.createDelegate(this, this._onPreviousButtonClick);
  $addHandler(this._searchButton, "click", this._searchButtonClickHandler);
  $addHandler(this._nextButton, "click", this._nextButtonClickHandler);
  $addHandler(this._previousButton, "click", this._previousButtonClickHandler);
  this._onSuccessHandler = Function.createDelegate(this, this._onSuccess);
  this._onFailureHandler = Function.createDelegate(this, this._onFailure);
}

function CustomComponents3$AspNetAjaxAmazonSearch$_onSearchButtonClick(evt)
{
  this._pageIndex = 1;
  this._searchQuery = this._searchTextBox.value;
  this._searchMethod( {"pageIndex": this._pageIndex, "searchQuery": this._searchQuery},
  this._onSuccessHandler, this._onFailureHandler, null);
}

function CustomComponents3$AspNetAjaxAmazonSearch$_onPreviousButtonClick(evt)
{
  this._pageIndex--;
  if (this._pageIndex < 0)
    this._pageIndex = 1;
  this._searchQuery = this._searchTextBox.value;
  this._searchMethod( {"pageIndex": this._pageIndex, "searchQuery": this._searchQuery},
                      this._onSuccessHandler, this._onFailureHandler, null);
}

function CustomComponents3$AspNetAjaxAmazonSearch$_onNextButtonClick(evt)
{
  this._pageIndex++;
  this._searchQuery = this._searchTextBox.value;
  this._searchMethod( {"pageIndex": this._pageIndex, "searchQuery": this._searchQuery},
                      this._onSuccessHandler, this._onFailureHandler, null);
}

function CustomComponents3$AspNetAjaxAmazonSearch$_onSuccess(items, userContext, methodName)
{
  var html = this._htmlGenerator.generateHtml(items);
  this._searchResultAreaDiv.innerHTML = html;
  this._commandBarAreaDiv.style.display = "block";
  this._searchResultAreaDiv.style.display = "block";
}

function CustomComponents3$AspNetAjaxAmazonSearch$_onFailure(result, userContext, methodName)
{
  var builder = new Sys.StringBuilder();
  builder.append("timedOut: ");
  builder.append(result.get_timedOut());
  builder.appendLine();
  builder.appendLine();
  builder.append("message: ");
  builder.append(result.get_message());
  builder.appendLine();
  builder.appendLine();
  builder.append("stackTrace: ");
  builder.appendLine();
  builder.append(result.get_stackTrace());
  builder.appendLine();
  builder.appendLine();
  builder.append("exceptionType: ");
  builder.append(result.get_exceptionType());
  builder.appendLine();
  builder.appendLine();
  builder.append("statusCode: ");
  builder.append(result.get_statusCode());
  builder.appendLine();
  builder.appendLine();
  builder.append("methodName: ");
  builder.append(methodName);
  alert(builder.toString());
}

CustomComponents3.AspNetAjaxAmazonSearch.prototype =
{
  get_searchTextBox: CustomComponents3$AspNetAjaxAmazonSearch$get_searchTextBox,
  set_searchTextBox: CustomComponents3$AspNetAjaxAmazonSearch$set_searchTextBox,
  get_searchButton: CustomComponents3$AspNetAjaxAmazonSearch$get_searchButton,
  set_searchButton: CustomComponents3$AspNetAjaxAmazonSearch$set_searchButton,
  get_searchResultAreaDiv: CustomComponents3$AspNetAjaxAmazonSearch$get_searchResultAreaDiv,
  set_searchResultAreaDiv: CustomComponents3$AspNetAjaxAmazonSearch$set_searchResultAreaDiv,
  get_commandBarAreaDiv: CustomComponents3$AspNetAjaxAmazonSearch$get_commandBarAreaDiv,
  set_commandBarAreaDiv: CustomComponents3$AspNetAjaxAmazonSearch$set_commandBarAreaDiv,
  get_nextButton: CustomComponents3$AspNetAjaxAmazonSearch$get_nextButton,
  set_nextButton: CustomComponents3$AspNetAjaxAmazonSearch$set_nextButton,
  get_previousButton: CustomComponents3$AspNetAjaxAmazonSearch$get_previousButton,
  set_previousButton: CustomComponents3$AspNetAjaxAmazonSearch$set_previousButton,
  get_pageIndex: CustomComponents3$AspNetAjaxAmazonSearch$get_pageIndex,
  set_pageIndex: CustomComponents3$AspNetAjaxAmazonSearch$set_pageIndex,
  get_htmlGenerator: CustomComponents3$AspNetAjaxAmazonSearch$get_htmlGenerator,
  set_htmlGenerator: CustomComponents3$AspNetAjaxAmazonSearch$set_htmlGenerator,
  get_searchMethod: CustomComponents3$AspNetAjaxAmazonSearch$get_searchMethod,
  set_searchMethod: CustomComponents3$AspNetAjaxAmazonSearch$set_searchMethod,
  initialize: CustomComponents3$AspNetAjaxAmazonSearch$initialize,
  _onSearchButtonClick: CustomComponents3$AspNetAjaxAmazonSearch$_onSearchButtonClick,
  _onPreviousButtonClick: CustomComponents3$AspNetAjaxAmazonSearch$_onPreviousButtonClick,
  _onNextButtonClick: CustomComponents3$AspNetAjaxAmazonSearch$_onNextButtonClick,
  _onSuccess: CustomComponents3$AspNetAjaxAmazonSearch$_onSuccess,
  _onFailure: CustomComponents3$AspNetAjaxAmazonSearch$_onFailure
}

CustomComponents3.AspNetAjaxAmazonSearch.registerClass("CustomComponents3.AspNetAjaxAmazonSearch", Sys.UI.Control);

if (typeof(Sys) !== 'undefined')
  Sys.Application.notifyScriptLoaded();