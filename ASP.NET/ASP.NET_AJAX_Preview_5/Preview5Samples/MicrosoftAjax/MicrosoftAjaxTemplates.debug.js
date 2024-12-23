﻿// (c) 2009 Microsoft Corporation, All Rights Reserved
// Use of this software is subject to the following license terms: 
// http://go.microsoft.com/fwlink/?LinkId=163454

Type._registerScript("MicrosoftAjaxTemplates.js", ["MicrosoftAjaxComponentModel.js", "MicrosoftAjaxSerialization.js"]);

Type.registerNamespace("Sys.Net");

Sys.Net.WebRequestEventArgs = function Sys$Net$WebRequestEventArgs(executor, error, result) {
    /// <summary locid="M:J#Sys.Net.WebRequestEventArgs.#ctor" />
    /// <param name="executor" type="Sys.Net.WebRequestExecutor" mayBeNull="true"></param>
    /// <param name="error" type="Sys.Net.WebServiceError" optional="true" mayBeNull="true"></param>
    /// <param name="result" optional="true" mayBeNull="true"></param>
    this._executor = executor;
    this._error = error || null;
    this._result = typeof (result) === "undefined" ? null : result;
    Sys.Net.WebRequestEventArgs.initializeBase(this);
}

    function Sys$Net$WebRequestEventArgs$get_error() {
        /// <value type="Sys.Net.WebServiceError" mayBeNull="true" locid="P:J#Sys.Net.WebRequestEventArgs.error"></value>
        return this._error || null;
    }
    function Sys$Net$WebRequestEventArgs$get_executor() {
        /// <value type="Sys.Net.WebRequestExecutor" mayBeNull="true" locid="P:J#Sys.Net.WebRequestEventArgs.executor"></value>
        return this._executor;
    }
    function Sys$Net$WebRequestEventArgs$get_result() {
        /// <value mayBeNull="true" locid="P:J#Sys.Net.WebRequestEventArgs.result"></value>
        return this._result;
    }
Sys.Net.WebRequestEventArgs.prototype = {
    get_error: Sys$Net$WebRequestEventArgs$get_error,
    get_executor: Sys$Net$WebRequestEventArgs$get_executor,
    get_result: Sys$Net$WebRequestEventArgs$get_result
};
Sys.Net.WebRequestEventArgs.registerClass("Sys.Net.WebRequestEventArgs", Sys.EventArgs);

Type.registerNamespace("Sys.Data");

Sys.Data.DataEventArgs = function Sys$Data$DataEventArgs(data) {
    /// <summary locid="M:J#Sys.Data.DataEventArgs.#ctor" />
    /// <param name="data" mayBeNull="true"></param>
    this._data = data;
    Sys.Data.DataEventArgs.initializeBase(this);
}

    function Sys$Data$DataEventArgs$get_data() {
        /// <value mayBeNull="true" locid="P:J#Sys.Data.DataEventArgs.data"></value>
        var d = this._data;
        return (typeof(d) === "undefined" ? null : d);
    }
    function Sys$Data$DataEventArgs$set_data(value) {
        this._data = value;
    }
    function Sys$Data$DataEventArgs$get_itemPlaceholder() {
        /// <value mayBeNull="true" locid="P:J#Sys.Data.DataEventArgs.itemPlaceholder"></value>
        return this._placeholder || null;
    }
    function Sys$Data$DataEventArgs$set_itemPlaceholder(value) {
        this._placeholder = value;
    }
    function Sys$Data$DataEventArgs$get_itemTemplate() {
        /// <value mayBeNull="true" locid="P:J#Sys.Data.DataEventArgs.itemTemplate"></value>
        return this._itemTemplate || null;
    }
    function Sys$Data$DataEventArgs$set_itemTemplate(value) {
        this._itemTemplate = value;
    }
Sys.Data.DataEventArgs.prototype = {
    get_data: Sys$Data$DataEventArgs$get_data,
    set_data: Sys$Data$DataEventArgs$set_data,
    get_itemPlaceholder: Sys$Data$DataEventArgs$get_itemPlaceholder,
    set_itemPlaceholder: Sys$Data$DataEventArgs$set_itemPlaceholder,    
    get_itemTemplate: Sys$Data$DataEventArgs$get_itemTemplate,
    set_itemTemplate: Sys$Data$DataEventArgs$set_itemTemplate
};
Sys.Data.DataEventArgs.registerClass("Sys.Data.DataEventArgs", Sys.CancelEventArgs);

if (!Sys.Data.IDataProvider) {
Sys.Data.IDataProvider = function Sys$Data$IDataProvider() {
    throw Error.notImplemented();
}

    function Sys$Data$IDataProvider$fetchData(operation, parameters, mergeOption, httpVerb, succeededCallback, failedCallback, timeout, userContext) {
        /// <summary locid="M:J#Sys.Data.IDataProvider.fetchData" />
        /// <param name="operation"></param>
        /// <param name="parameters" type="Object" mayBeNull="true" optional="true"></param>
        /// <param name="mergeOption" type="Sys.Data.MergeOption" mayBeNull="true" optional="true"></param>
        /// <param name="httpVerb" type="String" mayBeNull="true" optional="true"></param>
        /// <param name="succeededCallback" type="Function" mayBeNull="true" optional="true"></param>
        /// <param name="failedCallback" type="Function" mayBeNull="true" optional="true"></param>
        /// <param name="timeout" type="Number" integer="true" mayBeNull="true" optional="true"></param>
        /// <param name="userContext" mayBeNull="true" optional="true"></param>
        /// <returns type="Sys.Net.WebRequest"></returns>
        throw Error.notImplemented();
    }
Sys.Data.IDataProvider.prototype = {
    fetchData: Sys$Data$IDataProvider$fetchData
}
Sys.Data.IDataProvider.registerInterface("Sys.Data.IDataProvider");
}
if (!Sys.Data.MergeOption) {
Sys.Data.MergeOption = function Sys$Data$MergeOption() {
    /// <summary locid="M:J#Sys.Data.MergeOption.#ctor" />
    /// <field name="appendOnly" type="Number" integer="true" static="true" locid="F:J#Sys.Data.MergeOption.appendOnly"></field>
    /// <field name="overwriteChanges" type="Number" integer="true" static="true" locid="F:J#Sys.Data.MergeOption.overwriteChanges"></field>
    throw Error.notImplemented();
}




Sys.Data.MergeOption.prototype = {
    appendOnly: 0,
    overwriteChanges: 1
    }
Sys.Data.MergeOption.registerEnum("Sys.Data.MergeOption");

}

Type.registerNamespace("Sys.UI");

Sys.UI.DomElement._oldGetElementById = Sys.UI.DomElement.getElementById;
Sys.UI.DomElement.getElementById = function Sys$UI$DomElement$getElementById(id, element) {
    /// <summary locid="M:J#Sys.UI.DomElement.getElementById" />
    /// <param name="id" type="String"></param>
    /// <param name="element" domElement="true" optional="true" mayBeNull="true"></param>
    /// <returns domElement="true" mayBeNull="true"></returns>
    var el = Sys.UI.DomElement._oldGetElementById(id, element);
    if (!el && !element && Sys.UI.Template._contexts.length) {
                var contexts = Sys.UI.Template._contexts;
        for (var i = 0, l = contexts.length; i < l; i++) {
            var context = contexts[i];
            for (var j = 0, m = context.length; j < m; j++) {
                var c = context[j];
                if (c.nodeType === 1) {
                    if (c.id === id) return c;
                    el = Sys.UI.DomElement._oldGetElementById(id, c);
                    if (el) return el;
                }
            }
        }
    }
    return el;
}
if ($get === Sys.UI.DomElement._oldGetElementById) {
    $get = Sys.UI.DomElement.getElementById;
}
Sys.Application.registerMarkupExtension = function Sys$Application$registerMarkupExtension(extensionName, extension, isExpression) {
    /// <summary locid="M:J#Sys.Application.registerMarkupExtension" />
    /// <param name="extensionName" type="String"></param>
    /// <param name="extension" type="Function"></param>
    /// <param name="isExpression" type="Boolean" optional="true"></param>
    if (!this._extensions) {
        this._extensions = {};
    }
    isExpression = ((typeof (isExpression) === "undefined") || (isExpression === true));
    this._extensions[extensionName] = { expression: isExpression, extension: extension };
}
Sys.Application._getMarkupExtension = function Sys$Application$_getMarkupExtension(name) {
    var extension = this._extensions ? this._extensions[name] : null;
    if (!extension) {
        throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.cannotFindMarkupExtension, name));
    }
    return extension;
}
Sys.Application._caseIndex = {};
Sys.Application._prototypeIndex = {};

Sys.Application.activateElement = function Sys$Application$activateElement(element, bindingContext, recursive) {
    /// <summary locid="M:J#Sys.Application.activateElement" />
    /// <param name="element" domElement="true"></param>
    /// <param name="bindingContext" type="Object" optional="true" mayBeNull="true"></param>
    /// <param name="recursive" optional="true" mayBeNull="true"></param>
    /// <returns type="Sys.UI.TemplateContext"></returns>
    recursive = (recursive !== false);
    return Sys.Application.activateElements([element], bindingContext || null, recursive);
}

Sys.Application.activateElements = function Sys$Application$activateElements(elements, bindingContext, recursive) {
    /// <summary locid="M:J#Sys.Application.activateElements" />
    /// <param name="elements" type="Array" elementDomElement="true"></param>
    /// <param name="bindingContext" optional="true" mayBeNull="true"></param>
    /// <param name="recursive" optional="true" mayBeNull="true"></param>
    /// <returns type="Sys.UI.TemplateContext"></returns>
    var tc = this._context || new Sys.UI.TemplateContext(),
        useDirect = Sys.Browser.agent === Sys.Browser.InternetExplorer;
    tc._global = true;
    tc.dataItem = typeof(bindingContext) === "undefined" ? null : bindingContext;
    tc.components = tc.components || [];
    recursive = (recursive !== false);
    for (var i = 0, l = elements.length; i < l; i++) {
        var element = elements[i];
        Sys.Application._activateElement(element, tc, useDirect, recursive);
    }
    tc.initializeComponents();
    tc._onInstantiated(null, true);
    this._context = tc;
    return tc;
}

Sys.Application._findType = function Sys$Application$_findType(element, prefix, useDirect) {
        var er, err, type, xmlns = "xmlns:" + prefix;
    function getType() {
        var ns;
        try { ns = useDirect ? element[xmlns] : element.getAttribute(xmlns); }
        catch(er) { }
        if (ns && ns.substr(0, 11) === "javascript:") {
            ns = ns.substr(11);
            type = null;          
            try {
                type = Type.parse(ns);
            }
            catch(er) {
                err = String.format(Sys.UI.TemplatesRes.invalidTypeNamespace, ns);
                return;
            }
            if (type && type.__class) {
                                return;
            }
            else {
                                                type = ns; 
            }
        }
    }
    for (; element; element = element.parentNode) {
        getType();
        if (err) {
                                    throw Error.invalidOperation(err);
        }
        if (type) return type;
    }
            element = document.body;
    getType();
    if (err) {
        throw Error.invalidOperation(err);
    }
    return type;
}

Sys.Application._activateElement = function Sys$Application$_activateElement(parent, templateContext, useDirect, recursive) {
                    if (recursive) {
        recursive = !Sys.UI.Template._isTemplate(parent);
    }
        var i = -1,
        allElements = recursive ? (useDirect ? parent.all : parent.getElementsByTagName("*")) : [],
        isTemplate = /(^| )sys\-template($| )/
        expandosAreLast = useDirect && Sys.Browser.version <= 7,
        element = parent;
    do {
        if (element.nodeType !== 1) continue;         ///// this code is inline rather than factored out into a function because
        ///// performance testing has shown that calling the factored method causes activation
        ///// to be measurably slower in IE, due to the overhead of invoking a function.
        ///// begin element activation

                                var err, j, m, typeList = null, entry = null, index = null, fnIndex = null,
            activated = false,
            nodeName = null,
            attributes = element.attributes,
            alength = attributes.length - 1,
            attributes2 = null;
                        if (alength >= 0 && (!expandosAreLast || attributes[alength].expando) && !element.__msajaxactivated) {
            for (j = alength; j >= 0; j--) {
                var attribute = attributes[j];
                                                                                                if (expandosAreLast && !attribute.expando) break;
                if (!attribute.specified || attribute.nodeName.indexOf(':') < 0) continue;
                activated = true;
                nodeName = attribute.nodeName;
                var value = attribute.nodeValue;
                switch (nodeName) {
                case "sys:attach":
                    typeList = value.split(',');
                    break;
                case "sys:key":
                    templateContext.keys[value] = element;
                    break;
                case "sys:command":
                    var command = Sys.Application._getPropertyValue(null, null, null, value, templateContext, null, true);
                    var commandArg = Sys.Application._getCommandAttr(element, "sys:commandargument", useDirect),
                        commandTarget = Sys.Application._getCommandAttr(element, "sys:commandtarget", useDirect);
                    Sys.UI.DomEvent.addHandler(element, 'click', Sys.UI.Template._getCommandHandler(templateContext, command, commandArg, commandTarget));
                    break;
                default:
                                        attributes2 = attributes2 || [];
                    attributes2.push(attribute);
                }
            }             if (activated)  {
                element.__msajaxactivated = true;
                if (typeList) {
                                                            index = {};
                    for (var k = 0, n = typeList.length; k < n; k++) {
                        var typeName = typeList[k].trim();
                        if (index[typeName]) continue;                         var type = Sys.Application._findType(element, typeName, useDirect);
                        if (!type) {
                            throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.invalidAttach, "sys:attach", typeName));
                        }
                        var props = null, isComponent = 0, isControlOrBehavior = 0, isContext = 0,
                            isClass = (typeof(type) !== "string"),
                            sysKey = null, instance = null;
                        try { sysKey = useDirect ? element[typeName + ":sys-key"] : element.getAttribute(typeName + ":sys-key"); }
                        catch(err) { }
                        if (isClass) {
                            isComponent = type.inheritsFrom(Sys.Component);
                            isControlOrBehavior = (isComponent && (type.inheritsFrom(Sys.UI.Behavior) || type.inheritsFrom(Sys.UI.Control)));
                            isContext = type.implementsInterface(Sys.UI.ITemplateContextConsumer);
                            instance = isControlOrBehavior ? new type(element) : new type();
                            if (isComponent) {
                                instance.beginUpdate();
                            }
                            if (!isControlOrBehavior) {
                                Sys.Application._registerComponent(element, instance);
                            }
                            if (sysKey) {
                                templateContext.keys[sysKey] = instance;
                                instance.__tc = [null, null, sysKey];
                            }
                            if (isContext) {
                                instance.set_templateContext(templateContext);
                            }
                            entry = { instance: instance, isClass: true, typeName: typeName, type: type};
                        }
                        else {
                            props = {};
                            instance = Type.parse(type);
                            if (sysKey) {
                                props["sys-key"] = sysKey;
                            }
                            entry = { instance: instance, props: props, typeName: typeName, type: type};
                            if (!fnIndex) {
                                fnIndex = [entry];
                            }
                            else {
                                fnIndex[fnIndex.length] = entry;
                            }
                        }
                        index[typeName] = entry;
                    }             
                }                 if (attributes2) {
                    for (j = attributes2.length - 1; j >= 0; j--) {
                        attribute = attributes2[j];
                        nodeName = attribute.nodeName;
                        value = attribute.nodeValue;
                        var isSelect = (/select/i.test(element.tagName)),
                            attrib = Sys.Application._splitAttribute(nodeName, isSelect, index),
                            atype = attrib.type,
                            ns = attrib.ns,
                            name = attrib.name;
                                                if (atype < 0) continue;
                        if (atype === 4) {
                            entry = attrib.index;
                            var target = entry.instance;
                            value = Sys.Application._getPropertyValue(attrib, target, name, value, templateContext);
                            if (typeof(value) === "undefined") continue;
                            if (entry.isClass) {
                                switch (attrib.map.type) {
                                    case 1:
                                        attrib.map.setter.call(target, value);
                                        break;
                                    case 2:
                                        attrib.map.setter.call(target, typeof(value) === "function" ? value : new Function("sender", "args", value));
                                        break;
                                    default:
                                        target[name] = value;
                                }
                            }
                            else {
                                entry.props[name] = value;
                            }
                        }
                        else if (atype <= 2) {
                                                        if (attrib.textNode || (name === "innerHTML")) {
                                Sys.Application._clearContent(element);
                                if (attrib.textNode) {
                                    element.appendChild(element = document.createTextNode(''));
                                }
                            }
                            value = Sys.Application._getPropertyValue(attrib, element, name, value, templateContext);
                            if (typeof(value) === "undefined") continue;
                            switch(attrib.type) {
                                case 0:                                                                                                                                                                                                                                                             if (/^on/i.test(name)) {
                                                                                element[name] = document.attachEvent ? new Function(value) : new Function("event", value);
                                        break;
                                    }
                                    if (isSelect && (name === "value")) {
                                                                                element.value = value;
                                        break;
                                    }
                                    var booleans = Sys.UI.Template._booleanAttributes,
                                        lowerTag = element.tagName.toLowerCase(),
                                        isbool = (name === "disabled") || (booleans[lowerTag] && booleans[lowerTag][name]);
                                    if (isbool) {
                                        if (name === "selected") {
                                                                                                                                                                                                                                                                                                                                                                element.selected = value;
                                        }
                                        if (!value) {
                                                                                                                                                                                element.removeAttribute(name);
                                            break;
                                        }
                                        else if (name === "checked") {
                                                                                        element.setAttribute(name, name);
                                            break;
                                        }
                                    }
                                    var a = document.createAttribute(name);
                                                                        a.nodeValue = isbool ? name : value;
                                    element.setAttributeNode(a);
                                    break;
                                case 1:                                                                         Sys.Observer.setValue(element, name, value);
                                    break;
                                case 2:                                     value ? Sys.UI.DomElement.addCssClass(element, name) : Sys.UI.DomElement.removeCssClass(element, name);
                                    break;
                            }
                        }                     }                 }                                 if (fnIndex) {  
                    for (j = 0, m = fnIndex.length; j < m; j++) {
                        entry = fnIndex[j];
                        templateContext._registerIf(entry.instance(element, entry.props, templateContext));
                    }      
                }
                if (index) {
                    for (entry in index) {
                        if (index.hasOwnProperty(entry)) {
                                                                                                                entry = index[entry];
                            if (entry && entry.isClass) {
                                templateContext._registerComponent(entry.instance);
                            }
                        }
                    }
                }
            }             ///// end element activatation
        }                         if (recursive) {
            var className = element.className;
            if (className && (className.length >= 12) && ((className === "sys-template") || isTemplate.test(className))) {
                                                var next = element.nextSibling;
                while (next && (next.nodeType !== 1)) {
                    next = next.nextSibling;
                }
                                                while (!next) {                        
                    element = element.parentNode;
                    if (element === parent) {
                        break;
                    }
                    next = element.nextSibling;
                    while (next && (next.nodeType !== 1)) {
                        next = next.nextSibling;
                    }
                }
                if (!next || (next.nodeType !== 1)) {
                                        break;
                }
                                do {
                    element = allElements[i+1];
                    if (element === next) break;
                    i++;
                }
                while (element);
            }
        }
    }             while (!!(element = allElements[++i])); 
}

Sys.Application._clearContent = function Sys$Application$_clearContent(element) {
    var err;
    Sys.Application.disposeElement(element, true);
    try { element.innerHTML = ""; }
    catch(err) {
        while (element.firstChild) {
            element.removeChild(element.firstChild);
        }
    }
}
Sys.Application._getCommandAttr = function Sys$Application$_getCommandAttr(element, name, useDirect) {
    var err, value = null;
    try {
        value = useDirect ? element[name] : element.getAttribute(name);
        value = value ? Sys.Application._getPropertyValue(null, null, null, value, templateContext, null, true) : null;
    }
    catch(err) { }
    return value;
}
Sys.Application._directAttributes = {
    "style": "style.cssText",
    "class": "className",
    "cellpadding" : "cellPadding",
    "cellspacing" : "cellSpacing",
    "colspan" : "colSpan",
    "rowspan" : "rowSpan",
    "contenteditable" : "contentEditable",
    "valign" : "vAlign"
}
Sys.Application._splitAttribute = function Sys$Application$_splitAttribute(attributeName, isSelect, typeIndex) {
                                                        var nameParts = attributeName.split(':'),
            ns = nameParts.length > 1 ? nameParts[0] : null,
            name = nameParts[ns ? 1 : 0],
            type = -1,
            textNode, map, index, isSys = (ns === "sys"),
            lowerName = name.toLowerCase(),
            isNative = !ns;
    if (!ns || isSys) {
        var newName = Sys.Application._directAttributes[lowerName];
        if (newName) {
            type = 1;
            name = newName;
        }
        else if (isSelect) {
                                    if (lowerName === "selectedindex") {
                                name = "selectedIndex";
                type = 1;
            }
            else if (name === "value") {
                type = 1;
            }
            else if (isSys) {
                type = 0;
                ns = null;
            }
        }
        else if (isSys) {
            if ((name === "command") || (name === "commandargument") || (name === "commandtarget")) {
                type = 5;             }
            else if (name.indexOf("style-") === 0) {
                                                                name = "style." + Sys.Application._translateStyleName(name.substr(6));
                type = 1;
            }
            else if (name.indexOf("class-") === 0) {
                name = name.substr(6);
                type = 2;
            }
            else if (name === "innerhtml") {
                type = 1;
                name = "innerHTML";
            }
            else if (name === "innertext") {
                type = 1;
                textNode = true;
                name = "nodeValue";
            }
            else {
                                ns = null;
                type = 0;
            }        
        }
    }
    else if (typeIndex) {
        index = typeIndex[ns];
        if (index) {
            type = 4;
            if (index.isClass) {
                if (name === "sys-key") {
                    type = -2;
                }
                else {
                    map = Sys.Application._translateName(name, index.type);
                    name = map.name;
                }
            }
        }
        else {
                                                name = ns + ":" + name;
            ns = null;
            type = -1;
       }
    }
    else {
        name = ns + ":" + name;
        ns = null;
        type = -1;
    }
    return { ns: ns, name: name, type: type, map: map, index: index, textNode: textNode, isNative: isNative };
}

Sys.Application._translateStyleName = function Sys$Application$_translateStyleName(name) {
        if (name.indexOf('-') === -1) return name;
    var parts = name.toLowerCase().split('-'),
                newName = parts[0];
    for (var i = 1, l = parts.length; i < l; i++) {
        var part = parts[i];
        newName += part.substr(0, 1).toUpperCase() + part.substr(1);
    }
    return newName;
}

Sys.Application._getExtensionCode = function Sys$Application$_getExtensionCode(extension, doEval, templateContext) {
    extension = extension.trim();
    var name, properties, propertyBag = {}, spaceIndex = extension.indexOf(' ');
    if (spaceIndex !== -1) {
        name = extension.substr(0, spaceIndex);
        properties = extension.substr(spaceIndex + 1);
        if (properties) {
            properties = properties.replace(/\\,/g, '\u0000').split(",");
            for (var i = 0, l = properties.length; i < l; i++) {
                var property = properties[i].replace(/\u0000/g, ","),
                        equalIndex = property.indexOf('='),
                        pValue, pName;
                if (equalIndex !== -1) {
                    pName = property.substr(0, equalIndex).trim();
                    pValue = property.substr(equalIndex + 1).trim();
                    if (doEval) {
                                                pValue = this._getPropertyValue(null, null, null, pValue, templateContext, true);
                    }
                }
                else {
                    pName = "$default";
                    pValue = property.trim();
                }
                propertyBag[pName] = pValue;
            }
        }
    }
    else {
        name = extension;
    }
    return { instance: Sys.Application._getMarkupExtension(name), name: name, properties: propertyBag };
}

Sys.Application._getPropertyValue = function Sys$Application$_getPropertyValue(attrib, target, name, value, templateContext, inExtension, suppressExtension) {
    var propertyValue = value;
    if (value.startsWith("{{") && value.endsWith("}}")) {
        propertyValue = this._evaluateExpression(value.slice(2, -2), templateContext);
    }
    else if (!suppressExtension && !inExtension && value.startsWith("{") && value.endsWith("}")) {
        var extension = this._getExtensionCode(value.slice(1, -1), true, templateContext);
        propertyValue = extension.instance.extension(target, (attrib.type === 2 ? "class:" : "") + name, templateContext, extension.properties);
    }
    return propertyValue;
}
Sys.Application._tryName = function Sys$Application$_tryName(name, type) {
    var prototype = type.prototype,
        setterName = "set_" + name, setter = prototype[setterName];
    if (setter) {
        return { name: name, setterName: setterName, setter: setter, type: 1 };
    }
    if (name.startsWith('on')) {
        setterName = "add_" + name.substr(2);
        var adder = prototype[setterName];
        if (adder) {
            return { name: name, setterName: setterName, setter: adder, type: 2 };
        }
    }
    if (typeof(prototype[name]) !== "undefined") {
        return { name: name };
    }
    return null;
}
Sys.Application._translateName = function Sys$Application$_translateName(name, type) {
    if (name && (name !== name.toLowerCase())) {
        throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.invalidAttributeName, name));
    }
    var cache, index = Sys.Application._prototypeIndex[type.__typeName];
    if (index) {
        cache = index[name];
        if (cache) return cache;
    }
    else {
        index = {};
    }    
    type.resolveInheritance();
    cache = Sys.Application._tryName(name, type);
    if (!cache) {
        var casedName = Sys.Application._mapToPrototype(name, type);
        if (casedName && (casedName !== name)) {
            cache = Sys.Application._tryName(casedName, type);
        }
        if (!cache) {
            cache = { name: name };
        }
    }
    index[name] = cache;
    return cache;
}
Sys.Application._mapToPrototype = function Sys$Application$_mapToPrototype(name, type) {
        var fixedName, caseIndex = Sys.Application._caseIndex[type.__typeName];
    if (!caseIndex) {
        caseIndex = {};
        type.resolveInheritance();
        for (var memberName in type.prototype) {
            if (memberName.startsWith("get_") || memberName.startsWith("set_") || memberName.startsWith("add_")) {
                memberName = memberName.substr(4);
            }
            else if (memberName.startsWith("remove_")) {
                memberName = memberName.substr(7);
            }
            caseIndex[memberName.toLowerCase()] = memberName;
        }
        Sys.Application._caseIndex[type.__typeName] = caseIndex;
    }
    name = name.toLowerCase();
        if (name.startsWith('on')) {
        fixedName = caseIndex[name.substr(2)];
                if (fixedName) {
            fixedName = "on" + fixedName;
        }
        else {
            fixedName = caseIndex[name];
        }
    }
    else {
                fixedName = caseIndex[name];
    }
    return fixedName;
}
Sys.Application._doEval = function Sys$Application$_doEval(__expression, $context) {
    with($context.keys) {
        with($context.dataItem || {}) {
            return eval("(" + __expression + ")");
        }
    }
}
Sys.Application._evaluateExpression = function Sys$Application$_evaluateExpression($expression, $templateContext) {
    return Sys.Application._doEval.call($templateContext.dataItem, $expression, $templateContext);
}

Sys.Application._registerComponent = function Sys$Application$_registerComponent(element, component) {
                var components = element._components;
    if (!components) {
        element._components = components = [];
    }
    components[components.length] = component;
}

Sys.Application._activateOnPartial = function Sys$Application$_activateOnPartial(panel, rendering) {
    this._doUpdatePanel(panel, rendering);
    Sys.Application.activateElement(panel);
}

Sys.Application._raiseInit = function Sys$Application$_raiseInit() {
    this.beginCreateComponents();
    var handler = this.get_events().getHandler("init");
    if (handler) {
        handler(this, Sys.EventArgs.Empty);
    }
    Sys.Application.activateElement(document.documentElement);
    if (Sys.WebForms && Sys.WebForms.PageRequestManager) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm._doUpdatePanel = prm._updatePanel;
        prm._updatePanel = Sys.Application._activateOnPartial;
    }
    this.endCreateComponents();
}
Sys.UI.Template = function Sys$UI$Template(element) {
    /// <summary locid="M:J#Sys.UI.Template.#ctor" />
    /// <param name="element" domElement="true"></param>
    this._element = element;
    this._instantiateIn = null;
    this._instanceId = 0;
}

    function Sys$UI$Template$get_element() {
        /// <value domElement="true" locid="P:J#Sys.UI.Template.element"></value>
        return this._element;
    }
    function Sys$UI$Template$dispose() {
        this._element = null;
        this._instantiateIn = null;
    }
    function Sys$UI$Template$_appendTextNode(code, storeElementCode, text) {
        code.push(storeElementCode + "document.createTextNode(" +
                    Sys.Serialization.JavaScriptSerializer.serialize(text) +
                    "));\n");
    }
    function Sys$UI$Template$_appendAttributeSetter(code, typeIndex, attrib, expression, isExpression, booleanValue) {
        var ns = attrib.ns, name = attrib.name, restricted = (!ns && Sys.UI.Template._isRestricted(name));
        if (restricted) {
            expression = "Sys.UI.Template._checkAttribute('" + name + "', " + expression + ")";
        }
        switch (attrib.type) {
            case 1:                 if (isExpression) {
                    if (attrib.textNode) {
                                                code.push(Sys.UI.Template._createTextNode.replace("%1", expression).replace("%2", ""));
                    }
                    else {
                        code.push("  $component = $element;\n  $element." + name + " = " + expression + ";\n;");
                    }
                }
                else {
                    if (attrib.textNode) {
                        code.push(Sys.UI.Template._createTextNode.replace("%1", "''").replace("%2", expression + ";\n"));
                    }
                    else {
                        code.push("  $component = $element;\n  " + expression + ";\n");
                    }
                }
                if (attrib.textNode || name === "innerHTML") return true;
                break;
            case 2: 
                                                if (isExpression) {
                    name = Sys.Serialization.JavaScriptSerializer.serialize(name);
                    code.push("  $component = $element;\n    (" + expression +
                                ") ? Sys.UI.DomElement.addCssClass($element, " + name +
                                ") : Sys.UI.DomElement.removeCssClass($element, " + name + ");\n");
                }
                else {
                    code.push("  $component = $element;\n  " + expression + ";\n");
                }
                break;
            case 3:                 code.push("  $context.keys[" + expression + "] = $component;\n");            
                break;
            case 4:                 var index = typeIndex[ns];
                if (index.isClass) {
                    code.push("  $component = __componentIndex['" + ns + "'];\n");
                    if (isExpression) {
                        var map = attrib.map;
                        if (map.type === 1) {
                            code.push("  $component." + map.setterName + "(" + expression + ");\n");
                        }
                        else if (map.type === 2) {
                            code.push("  __f = " + expression + ";\n");
                            code.push("  $component." + map.setterName + '(typeof(__f) === "function" ? __f : new Function("sender", "args", __f));\n');
                        }
                        else {
                            code.push("  $component." + map.name + " = " + expression + ";\n");
                        }
                    }
                    else {
                        code.push("  " + expression + ";\n");
                    }
                }
                else {
                    if (!isExpression) {
                        throw Error.invalidOperation();                     }
                    var prop = Sys.Serialization.JavaScriptSerializer.serialize(attrib.name) + ": " + expression;
                    if (!index.props) {
                        index.props = prop;
                    }
                    else {
                        index.props += ", " + prop;
                    }
                }
                break;
            case 5:                 this["_" + name] = expression;
                break;
            default:                 if (isExpression) {
                    if (/^on/i.test(name)) {
                                                code.push("  $component = $element;\n  $element." + name + " = new Function(" +
                            (document.attachEvent ? "" : "'event', ") + expression + ");\n");
                    }
                    else {
                        if (booleanValue) {
                                                                                    code.push("  $component = $element;\n  if (" + expression +
                                        ") {\n    __e = document.createAttribute('" + name +
                                        "');\n    __e.nodeValue = \"" + booleanValue + "\";\n    $element.setAttributeNode(__e);\n  }\n");
                        }
                        else {
                            code.push("  $component = $element;\n  __e = document.createAttribute('" + name + "');\n  __e.nodeValue = " +
                                    expression + ";\n  $element.setAttributeNode(__e);\n");
                        }
                    }
                }
                else {
                                                            code.push("  $component = $element;\n  " + expression + ";\n");
                }
                break;
        }
        return false;
    }
    function Sys$UI$Template$_processAttribute(code, typeIndex, attrib, value, booleanValue) {
        value = this._getAttributeExpression(attrib, value);
        if (value) {
            return this._appendAttributeSetter(code, typeIndex, attrib,
                value.code, value.isExpression, booleanValue);
        }
        return false;
    }
    function Sys$UI$Template$_getAttributeExpression(attrib, value, noSerialize) {
        var type = typeof(value);
        if (type === "undefined") return null;
        if (value === null) return { isExpression: true, code: "null" };
        if (!attrib.isNative && (type === "string")) {
            if (value.startsWith("{{") && value.endsWith("}}")) {
                return { isExpression: true, code: value.slice(2, -2).trim() };
            }
            else if (value.startsWith("{") && value.endsWith("}")) {
                var jss = Sys.Serialization.JavaScriptSerializer,
                    ext = Sys.Application._getExtensionCode(value.slice(1, -1)),
                    properties = ext.properties;
                var props = "";
                for (var name in properties) {
                    var subValue = this._getAttributeExpression(attrib, properties[name]);
                    if (subValue && subValue.isExpression) {
                        var prop = jss.serialize(name) + ":" + subValue.code;
                        if (props) {
                            props += "," + prop;
                        }
                        else {
                            props = prop;
                        }
                    }
                }
                                                return { isExpression: ext.instance.expression, 
                    code: "Sys.Application._getMarkupExtension(" + jss.serialize(ext.name) + ").extension($component, " +
                        jss.serialize((attrib.type === 2 ? "class:" : "") + attrib.name) +
                        ", $context, {" + props + "})" };
            }
        }
        return { isExpression: true, code: (noSerialize ? value : 
                                            Sys.Serialization.JavaScriptSerializer.serialize(value)) };
    }
    function Sys$UI$Template$_processBooleanAttribute(element, code, typeIndex, name) {
        var value, isNative, node = element.getAttributeNode("sys:" + name);
        if (!node) {
            isNative = true;
            node = element.getAttributeNode(name);
            if (node && (node.specified || (node.nodeValue === true))) {
                                                value = true;
            }
            else if (element.getAttribute(name) === name) {
                                                value = true;
            }
            else {
                return;
            }
        }
        else {
            value = node.nodeValue;
            if (value === "true") {
                value = true;
            }
            else if (value === "false") {
                return;
            }
        }
        this._processAttribute(code, typeIndex, { name: name, isNative: isNative }, value, name);
    }
    function Sys$UI$Template$_processBooleanAttributes(element, code, typeIndex, attributes) {
        var name, node, value;
        for (var i = 0, l = attributes.length; i < l; i++) {
            this._processBooleanAttribute(element, code, typeIndex, attributes[i]);
        }
    }
    function Sys$UI$Template$_processCodeBlock(name, element, code) {
        var value = this._getExplicitAttribute(null, null, element, name);
        if (value) {
            value = this._getAttributeExpression({ name: name }, value, true).code;
            code.push((name === "sys:if") ? ("  if (" + value + ") {\n")
                        : ("  " + value + "\n"));
        }
        return !!value;
    }
    function Sys$UI$Template$_getExplicitAttribute(code, typeIndex, element, name, processName, isNative) {
        var node;
        try {
            node = element.getAttributeNode(name);
        }
        catch (e) {
            return null;
        }
        if (!node || !node.specified) {
            return null;
        }
        if (processName) {
            var value = (name === "style" ? element.style.cssText : node.nodeValue);
            this._processAttribute(code, typeIndex, { name: processName, type: 1, isNative: isNative }, value);
        }
        return node.nodeValue;
    }
    function Sys$UI$Template$_buildTemplateCode(nestedTemplates, element, code, depth) {
        var i, j, l, m, typeName, isInput,
            expressionRegExp = Sys.UI.Template._expressionRegExp,
            storeElementCode = "  " + (depth ? ("__p[__d-1].appendChild(") : "__topElements.push("),
            isIE = (Sys.Browser.agent === Sys.Browser.InternetExplorer);
            
        code.push("  __d++;\n");
        for (i = 0, l = element.childNodes.length; i < l; i++) {
            var childNode = element.childNodes[i], text = childNode.nodeValue;

            if (childNode.nodeType === 8) {
                code.push(storeElementCode + "document.createComment(" +
                    Sys.Serialization.JavaScriptSerializer.serialize(text) + "));\n");
            }
            else if (childNode.nodeType === 3) {
                                                var trimText = text.trim();
                if (trimText.startsWith("{") && trimText.endsWith("}") && (!trimText.startsWith("{{") || !trimText.endsWith("}}"))) {
                    var attribName, setComponentCode;
                                                            if (element.tagName.toLowerCase() === "textarea") {
                        attribName = "value";
                        setComponentCode = '$component=$element;\n';
                    }
                    else {
                        attribName = "nodeValue";
                        setComponentCode = storeElementCode + '$element=$component=document.createTextNode(""));\n';
                    }
                    var expr = this._getAttributeExpression({name:attribName}, trimText);
                    if (expr.isExpression) {
                        code.push(storeElementCode + "document.createTextNode(" + expr.code + "));\n");
                    }
                    else {
                                                code.push(setComponentCode + '  ' + expr.code + ';\n');
                    }
                }
                else {
                                        var match = expressionRegExp.exec(text), lastIndex = 0;
                    while (match) {
                        var catchUpText = text.substring(lastIndex, match.index);
                        if (catchUpText) {
                            this._appendTextNode(code, storeElementCode, catchUpText);
                        }
                        code.push(storeElementCode + "document.createTextNode(" + match[1] + "));\n");
                        lastIndex = match.index + match[0].length;
                        match = expressionRegExp.exec(text);
                    }
                    if (lastIndex < text.length) {
                        this._appendTextNode(code, storeElementCode, text.substr(lastIndex));
                    }
                }
            }
            else {
                var attributes = childNode.attributes,
                    typeNames = null, sysAttribute = null, typeIndex = {},
                    tagName = childNode.tagName.toLowerCase(),
                    booleanAttributes,  dp1 = depth + 1;
                if (tagName === "script") {
                                                                                                    continue;
                }
                var isCodeIfGenerated = this._processCodeBlock("sys:if", childNode, code);
                this._processCodeBlock("sys:codebefore", childNode, code);
                isInput = (tagName === "input");
                if (isInput) {
                    var typeExpression = this._getAttributeExpression({ name: "type", isNative: true }, childNode.getAttribute("type")) ||
                                         this._getAttributeExpression({ name: "type" }, childNode.getAttribute("sys:type"));
                    var nameExpression = this._getAttributeExpression({ name: "name", isNative: true }, childNode.getAttribute("name")) ||
                                         this._getAttributeExpression({ name: "name" }, childNode.getAttribute("sys:name"))
                    if (!typeExpression.isExpression || !nameExpression.isExpression) {
                        throw Error.invalidOperation(Sys.UI.TemplatesRes.mustSetInputElementsExplicitly);
                    }
                    code.push("  $element=__p[__d]=Sys.UI.Template._createInput(" + typeExpression.code + ", " + nameExpression.code + ");\n");
                    booleanAttributes = Sys.UI.Template._inputBooleanAttributes;
                    this._processBooleanAttributes(childNode, code, typeIndex, booleanAttributes[" list"]);
                }
                else {
                    code.push("  $element=__p[__d]=document.createElement('" + childNode.nodeName + "');\n");
                }

                                typeNames = this._getExplicitAttribute(code, typeIndex, childNode, "sys:attach");
                if (typeNames) {
                    typeNames = typeNames.split(',');
                    code.push("  __componentIndex = {}\n");
                                                            for (j = 0, m = typeNames.length; j < m; j++) {
                        typeName = typeNames[j].trim();
                        if (typeIndex[typeName]) continue;                         var type = Sys.Application._findType(childNode, typeName, isIE);
                        if (!type) {
                            throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.invalidAttach, "sys:attach", typeName));
                        }
                                                                        var isComponent, isControlOrBehavior, isContext,
                            isClass = typeof(type) !== "string";
                        if (isClass) {
                            isComponent = type.inheritsFrom(Sys.Component);
                            isControlOrBehavior = (isComponent && (type.inheritsFrom(Sys.UI.Behavior) || type.inheritsFrom(Sys.UI.Control)));
                            isContext = type.implementsInterface(Sys.UI.ITemplateContextConsumer);
                        }
                        typeIndex[typeName] = { type: type, isClass: isClass, isComponent: isComponent };
                        if (isClass) {
                            code.push("  __componentIndex['" + typeName + "'] = $component = new " + type.getName());
                            if (isControlOrBehavior) {
                                                                code.push("($element);\n");
                            }
                            else {
                                                                                                code.push("();\n  Sys.Application._registerComponent($element, $component);\n");
                            }
                                                                                                                sysAttribute = this._getExplicitAttribute(code, typeIndex, childNode, typeName + ":sys-key");
                            if (sysAttribute) {
                                this._processAttribute(code, typeIndex, { ns: typeName, name: "sys-key", type: 3 }, sysAttribute);
                            }
                            if (isComponent) {
                                                                                                                                code.push("  $component.beginUpdate();\n");
                            }
                            if (isContext) {
                                code.push("  $component.set_templateContext($context);\n");
                            }
                        }
                                                                                            }
                }
                
                                                                sysAttribute = this._getExplicitAttribute(code, typeIndex, childNode, "sys:key");
                if (sysAttribute) {
                    code.push("  $context.keys[" +
                                Sys.Serialization.JavaScriptSerializer.serialize(sysAttribute) + "] = $element;\n");
                }
                                this._getExplicitAttribute(code, typeIndex, childNode, "sys:id", "id");
                                                                this._getExplicitAttribute(code, typeIndex, childNode, "style", "style.cssText", true);
                this._getExplicitAttribute(code, typeIndex, childNode, "class", "className", true);
                
                                if (!isInput) {
                    booleanAttributes = Sys.UI.Template._booleanAttributes[tagName] ||
                        Sys.UI.Template._commonBooleanAttributes;
                    this._processBooleanAttributes(childNode, code, typeIndex, booleanAttributes[" list"]);
                }
                
                var isSelect = (tagName === "select"),
                                        delayedAttributes = null,
                    split = Sys.Application._splitAttribute,
                    skipChildren = false;
                for (j = 0, m = attributes.length; j < m; j++) {
                    var attribute = attributes[j], name = attribute.nodeName, lowerName = name.toLowerCase();
                                                            if (!attribute.specified && (!isInput || lowerName !== "value")) continue;
                                        if ((lowerName === "class") || (lowerName === "style")) continue;
                                        if (booleanAttributes[lowerName]) continue;
                                        if (isInput && (Sys._indexOf(Sys.UI.Template._inputRequiredAttributes, lowerName) !== -1)) continue;
                    var attrib = split(name, isSelect, typeIndex),
                        ns = attrib.ns,
                        value = attribute.nodeValue,
                        atype = attrib.type;
                    name = attrib.name;
                    if (atype === -2) continue;
                    if (atype === 1) {
                        if (isSelect && (!ns || ns === "sys")) {
                                                                                    delayedAttributes = delayedAttributes || [];
                            delayedAttributes.push([attrib,value]);
                            continue;
                        }
                    }
                    else if (atype === 0) {
                                                                        if (Sys._indexOf(Sys.UI.Template._sysAttributes, name) !== -1) continue;
                    }
                    if (this._processAttribute(code, typeIndex, attrib, value)) {
                        skipChildren = true;
                    }
                }
                if (this._command) {
                    code.push(" Sys.UI.DomEvent.addHandler($element, 'click', Sys.UI.Template._getCommandHandler($context, " 
                              +  this._command + ", " + (this._commandargument || 'null') + ", " + (this._commandtarget || 'null') + "));\n");
                    this._command = null;
                }
                this._commandargument = null;
                this._commandtarget = null;

                code.push(storeElementCode + "$element);\n");
                for (typeName in typeIndex) {
                    var index = typeIndex[typeName];
                    if (index.isClass) {
                                                code.push("  $context._registerComponent(__componentIndex['" + typeName + "']);\n");
                    }
                    else {
                                                code.push("  $context._registerIf(");
                        code.push(index.type);
                        code.push("($element, ");
                        code.push("{" + (index.props||"") + "}, $context));\n");
                    }
                }
                                                if (Sys.UI.Template._isTemplate(childNode)) {
                                                                                                                                            var nestedTemplate = new Sys.UI.Template(childNode);
                    nestedTemplate.recompile();
                    nestedTemplates.push(childNode._msajaxtemplate);
                    code.push("  $element._msajaxtemplate = this.get_element()._msajaxtemplate[1][" + (nestedTemplates.length-1) + "];\n");
                }
                else if (!skipChildren) {
                    this._buildTemplateCode(nestedTemplates, childNode, code, dp1);
                                        code.push("  $element=__p[__d];\n");
                }
                if (delayedAttributes) {
                    for (j = 0, m = delayedAttributes.length; j < m; j++) {
                        attribute = delayedAttributes[j];
                        this._processAttribute(code, typeIndex, attribute[0], attribute[1]);
                    }
                }
                this._processCodeBlock("sys:codeafter", childNode, code);
                if (isCodeIfGenerated) {
                    code.push("  }\n");
                }
            }
        }
        code.push("  --__d;\n");
    }
    function Sys$UI$Template$_ensureCompiled() {
        if (!this._instantiateIn) {
            var element = this.get_element();
            if (element._msajaxtemplate) {
                this._instantiateIn = element._msajaxtemplate[0];
            }
            else {
                this.recompile();
            }
        }
    }
    function Sys$UI$Template$recompile() {
        /// <summary locid="M:J#Sys.UI.Template.recompile" />
        var element = this.get_element(),
            code = [" $index = (typeof($index) === 'number' ? $index : __instanceId);\n var $component, __componentIndex, __e, __f, __topElements = [], __d = 0, __p = [__containerElement], $element = __containerElement, $context = new Sys.UI.TemplateContext(), $id = function(prefix) { return $context.getInstanceId(prefix); };\n $context.data = (typeof(__data) === 'undefined' ? null : __data);\n $context.components = [];\n $context.nodes = __topElements;\n $context.dataItem = $dataItem;\n $context.index = $index;\n $context.parentContext = __parentContext;\n $context.containerElement = __containerElement;\n $context.template = this;\n Sys.UI.Template._contexts.push(__topElements);\n with($context.keys) { with($dataItem || {}) {\n"],
            nestedTemplates = [];
        this._buildTemplateCode(nestedTemplates, element, code, 0);
        code.push("} }\n $context._onInstantiated(__referenceNode);\n return $context;");
        code = code.join('');
        element._msajaxtemplate = [this._instantiateIn = new Function("__containerElement", "__data", "$dataItem", "$index", "__referenceNode", "__parentContext", "__instanceId", code), nestedTemplates];
    }
    function Sys$UI$Template$instantiateIn(containerElement, data, dataItem, dataIndex, nodeToInsertTemplateBefore, parentContext) {
        /// <summary locid="M:J#$id" />
        /// <param name="containerElement"></param>
        /// <param name="data" optional="true" mayBeNull="true"></param>
        /// <param name="dataItem" optional="true" mayBeNull="true"></param>
        /// <param name="dataIndex" optional="true" mayBeNull="true" type="Number" integer="true"></param>
        /// <param name="nodeToInsertTemplateBefore" optional="true" mayBeNull="true"></param>
        /// <param name="parentContext" type="Sys.UI.TemplateContext" optional="true" mayBeNull="true"></param>
        /// <returns type="Sys.UI.TemplateContext"></returns>
        containerElement = Sys.UI.DomElement.resolveElement(containerElement);
        nodeToInsertTemplateBefore = (nodeToInsertTemplateBefore ? Sys.UI.DomElement.resolveElement(nodeToInsertTemplateBefore) : null);
        this._ensureCompiled();
        return this._instantiateIn(containerElement, data, dataItem, dataIndex, nodeToInsertTemplateBefore, parentContext, this._instanceId++);
    }
Sys.UI.Template.prototype = {
    get_element: Sys$UI$Template$get_element,
    dispose: Sys$UI$Template$dispose,
    _appendTextNode: Sys$UI$Template$_appendTextNode,
    _appendAttributeSetter: Sys$UI$Template$_appendAttributeSetter,
    _processAttribute: Sys$UI$Template$_processAttribute,
    _getAttributeExpression: Sys$UI$Template$_getAttributeExpression,
    _processBooleanAttribute: Sys$UI$Template$_processBooleanAttribute,
    _processBooleanAttributes: Sys$UI$Template$_processBooleanAttributes,
    _processCodeBlock: Sys$UI$Template$_processCodeBlock,
    _getExplicitAttribute: Sys$UI$Template$_getExplicitAttribute,
    _buildTemplateCode: Sys$UI$Template$_buildTemplateCode,
    _ensureCompiled: Sys$UI$Template$_ensureCompiled,
    recompile: Sys$UI$Template$recompile,
    instantiateIn: Sys$UI$Template$instantiateIn
}
Sys.UI.Template._isRestricted = function Sys$UI$Template$_isRestricted(name) {
    var restricted = Sys.UI.Template._getRestrictedIndex();
    return restricted.attributes[name.toLowerCase()];
}
Sys.UI.Template._checkAttribute = function Sys$UI$Template$_checkAttribute(name, value) {
    if (!value) return value;
    var newValue = value, restricted = Sys.UI.Template._getRestrictedIndex();
    if (restricted.attributes[name.toLowerCase()]) {
                        if (typeof(value) !== "string") {
            value = value.toString();
        }
        var match = Sys.UI.Template._protocolRegExp.exec(value.toLowerCase());
        if (match) {
                        if (!restricted.protocols[match[1]]) {
                newValue = "";
            }
        }
    }
    return newValue;
}
Sys.UI.Template._getCommandHandler = function Sys$UI$Template$_getCommandHandler(tc, name, argument, target) {
    return function() {
        if (target) {
            var control = (typeof(target) === "string") ? (tc.getObjectByKey(target) || Sys.Application.findComponent(target)) : target;
            if (!Sys.UI.Control.isInstanceOfType(control)) {
                throw Error.InvalidOperation(Sys.UI.TemplatesRes.invalidCommandTarget);
            }
            Sys.UI.DomElement._raiseBubbleEventFromControl(control, this, new Sys.CommandEventArgs(name, argument, this));
        }
        else {
            Sys.UI.DomElement.raiseBubbleEvent(this, new Sys.CommandEventArgs(name, argument, this)); 
        }
    }
}
Sys.UI.Template._getIdFunction = function Sys$UI$Template$_getIdFunction(instance) {
    return function(prefix) {
        return prefix + instance;
    }
}
Sys.UI.Template._createInput = function Sys$UI$Template$_createInput(type, name) {
    var element, dynamic = Sys.UI.Template._dynamicInputs;
    if (dynamic === true) {
        element = document.createElement('input');
        if (type) {
            element.type = type;
        }
        if (name) {
            element.name = name;
        }
    }
    else {
        var html = "<input ";
        if (type) {
            html += "type='" + type + "' ";
        }
        if (name) {
            html += "name='" + name + "' ";
        }
        html += "/>";
        try {
            element = document.createElement(html);
        }
        catch (err) {
            Sys.UI.Template._dynamicInputs = true;
            return Sys.UI.Template._createInput(type, name);
        }
        if (dynamic !== false) {
            if (element.tagName.toLowerCase() === "input") {
                Sys.UI.Template._dynamicInputs = false;
            }
            else {
                Sys.UI.Template._dynamicInputs = true;
                return Sys.UI.Template._createInput(type, name);
            }
        }
    }
    return element;
}
Sys.UI.Template._isTemplate = function Sys$UI$Template$_isTemplate(element) {
    var className = element.className;
    return (className && ((className === "sys-template") || /(^| )sys\-template($| )/.test(className)));
}
Sys.UI.Template._contexts = [];
Sys.UI.Template._inputRequiredAttributes = ["type", "name"];
Sys.UI.Template._commonBooleanAttributes = { disabled: true, " list" : ["disabled"] };
Sys.UI.Template._inputBooleanAttributes =
    { disabled: true, checked: true, readonly: true,
        " list": ["disabled", "checked", "readonly"] };
Sys.UI.Template._booleanAttributes = {
    "input": Sys.UI.Template._inputBooleanAttributes,
    "select": { disabled: true, multiple: true, " list": ["disabled", "multiple"] },
    "option": { disabled: true, selected: true, " list": ["disabled", "selected"] },
    "img": { disabled: true, ismap: true, " list": ["disabled", "ismap"] },
    "textarea": { disabled:true, readonly: true, " list": ["disabled", "readonly"] }
};
Sys.UI.Template._sysAttributes = ["attach", "id", "key",
    "disabled", "checked", "readonly", "ismap", "multiple", "selected", "if", "codebefore", "codeafter"];
Sys.UI.Template._expressionRegExp = /\{\{\s*([\w\W]*?)\s*\}\}/g;
Sys.UI.Template.allowedProtocols = [
    "http",
    "https"
];
Sys.UI.Template.restrictedAttributes = [
    "src",
    "href",
    "codebase",
    "cite",
    "background",
    "action",
    "longdesc",
    "profile",
    "usemap",
    "classid",
    "data"
];
Sys.UI.Template._getRestrictedIndex = function Sys$UI$Template$_getRestrictedIndex() {
    var i, l, protocolIndex, attributeIndex,
        protocols = Sys.UI.Template.allowedProtocols || [],
        attributes = Sys.UI.Template.restrictedAttributes || [],
        index = Sys.UI.Template._restrictedIndex;
    if (!index || (index.allowedProtocols !== protocols) || (index.restrictedAttributes !== attributes)) {
        index = { allowedProtocols: protocols, restrictedAttributes: attributes };
        index.protocols = protocolIndex = {};
        for (i = 0, l = protocols.length; i < l; i++) {
            protocolIndex[protocols[i]] = true;
        }
        index.attributes = attributeIndex = {};
        for (i = 0, l = attributes.length; i < l; i++) {
            attributeIndex[attributes[i]] = true;
        }
        Sys.UI.Template._restrictedIndex = index;
    }
    return index;
}
Sys.UI.Template._protocolRegExp = /^\s*([a-zA-Z0-9\+\-\.]+)\:/;
Sys.UI.Template._createTextNode = "  $element.appendChild($component=$element=document.createTextNode(%1));\n  %2$component=$element=$element.parentNode;\n";
Sys.UI.Template.registerClass("Sys.UI.Template", null, Sys.IDisposable);

Sys._Application.prototype.get_templateContext = function Sys$_Application$get_templateContext() {
    /// <summary locid="M:J#Sys._Application.get_templateContext" />
    return this._context || null;
}


Sys._Application.prototype._baseDispose = Sys._Application.prototype.dispose;
Sys._Application.prototype.dispose = function Sys$_Application$dispose() {
    var ctx = this._context;
    if (ctx) ctx.dispose();
    this._context = null;
    this._baseDispose();
}

Sys._Application.prototype.removeComponent = function Sys$_Application$removeComponent(component) {
    /// <summary locid="M:J#Sys._Application.removeComponent" />
    /// <param name="component" type="Sys.Component"></param>
            var id = component.get_id();
    if (id) delete this._components[id];
    var ctx = this._context;
    if (ctx) {
        var tc = component.__tc;
        if (tc && tc[0] === ctx._tcindex) {
            delete ctx.components[tc[1]];
            if (tc[2]) {
                delete ctx.keys[tc[2]];
            }
        }
    }
}

Sys._Application.prototype._disposeElementInternal = function Sys$_Application$_disposeElementInternal(element) {
    var err, key, ctx = this._context;
    if (ctx) {
                try {
            key = element.getAttribute("sys:key");
        }
        catch (err) {}
        if (key) {
            delete ctx.keys[key];
        }
    }
    var d = element.dispose;
    if (d && typeof(d) === "function") {
        element.dispose();
    }
    else {
        var c = element.control;
        if (c && typeof(c.dispose) === "function") {
            c.dispose();
        }
    }
    var list = element._behaviors;
    if (list) {
        this._disposeComponents(list);
    }
    list = element._components;
    if (list) {
        this._disposeComponents(list);
        element._components = null;
    }
}
Sys.UI.TemplateContext = function Sys$UI$TemplateContext() {
    /// <summary locid="M:J#Sys.UI.TemplateContext.#ctor" />
    /// <field name="data" locid="F:J#Sys.UI.TemplateContext.data"></field>
    /// <field name="dataItem" locid="F:J#Sys.UI.TemplateContext.dataItem"></field>
    /// <field name="index" type="Number" integer="true" locid="F:J#Sys.UI.TemplateContext.index"></field>
    /// <field name="getInstanceId" type="Function" locid="F:J#Sys.UI.TemplateContext.getInstanceId"></field>
    /// <field name="parentContext" type="Sys.UI.TemplateContext" locid="F:J#Sys.UI.TemplateContext.parentContext"></field>
    /// <field name="containerElement" domElement="true" locid="F:J#Sys.UI.TemplateContext.containerElement"></field>
    /// <field name="components" type="Array" elementType="Object" locid="F:J#Sys.UI.TemplateContext.components"></field>
    /// <field name="nodes" type="Array" elementDomElement="true" locid="F:J#Sys.UI.TemplateContext.nodes"></field>
    /// <field name="keys" type="Object" locid="F:J#Sys.UI.TemplateContext.keys"></field>
    this.keys = {};
    this._tcindex = Sys.UI.TemplateContext._tcindex++;
}










    function Sys$UI$TemplateContext$dispose() {
        /// <summary locid="M:J#Sys.UI.TemplateContext.dispose" />
        var nodes = this.nodes;
        if (nodes) {
            for (var i = 0, l = nodes.length; i < l; i++) {
                var element = nodes[i];
                if (element.nodeType === 1) {
                    Sys.Application.disposeElement(element, false);
                }
            }
        }
        this.nodes = this.dataItem = this.components = this.getInstanceId =
        this.containerElement = this.parentContext = this.data = null;
        this.keys = {}; 
    }
    function Sys$UI$TemplateContext$getElementById(id) {
        /// <summary locid="M:J#Sys.UI.TemplateContext.getElementById" />
        /// <param name="id" type="String"></param>
        /// <returns domElement="true"></returns>
        if (!this.nodes) return Sys.UI.DomElement.getElementById(id);
        var instanceId = this.getInstanceId(id),
            nodes = this.nodes,
            el, i, l;
        for (i = 0, l = nodes.length; i < l; i++) {
            el = nodes[i];
            if (el.nodeType !== 1) continue;
            if (el.id === instanceId) return el;
            el = Sys.UI.DomElement.getElementById(instanceId, el);
            if (el) return el;
        }
                for (i = 0, l = nodes.length; i < l; i++) {
            el = nodes[i];
            if (el.nodeType !== 1) continue;
            if (el.id === id) return el;
            el = Sys.UI.DomElement.getElementById(id, el);
            if (el) return el;
        }
        return null;
    }
    function Sys$UI$TemplateContext$getInstanceId(prefix) {
        /// <summary locid="M:J#Sys.UI.TemplateContext.getInstanceId" />
        /// <param name="prefix" type="String"></param>
        /// <returns type="String"></returns>
        var s = this.index, ctx = this.parentContext;
        while (ctx && !ctx._global) {
            s = ctx.index + "_" + s;
            ctx = ctx.parentContext;
        }
        return prefix + s;
    }
    function Sys$UI$TemplateContext$getObjectByKey(key) {
        /// <summary locid="M:J#Sys.UI.TemplateContext.getObjectByKey" />
        /// <param name="key" type="String"></param>
        /// <returns></returns>
        var ctx = this;
        while (ctx) {
            var obj = ctx.keys[key];
            if (obj) return obj;
            ctx = ctx.parentContext;
        }
        return null;
    }
    function Sys$UI$TemplateContext$initializeComponents() {
        /// <summary locid="M:J#Sys.UI.TemplateContext.initializeComponents" />
        var components = this.components;
        if (components) {
            var i = components.length - 1, index = this._lastIndex;
            this._lastIndex = i;
                        for (; i > index; i--) {
                var component = components[i];
                if (component && Sys.Component.isInstanceOfType(component)) {
                    if (component.get_isUpdating()) {
                        component.endUpdate();
                    }
                    else if (!component.get_isInitialized()) {
                        component.initialize();
                    }
                }
            }
        }
    }
    function Sys$UI$TemplateContext$_onInstantiated(referenceNode, global) {
        var i, l, nodes = this.nodes;
        if (this.__completedCallbacks) {
            for (i = 0, l = this.__completedCallbacks.length; i < l; i++) {
                this.__completedCallbacks[i]();
            }
            this.__completedCallbacks = null;
        }
        if (!global) {
            for (i = 0, l = nodes.length; i < l; i++) {
                this.containerElement.insertBefore(nodes[i], referenceNode);
            }
            Sys.UI.Template._contexts.pop();
        }
    }
    function Sys$UI$TemplateContext$_registerComponent(component) {
                                                var components = this.components;
        var tc = component.__tc;
                if (!tc) component.__tc = tc = [];
        tc[0] = this._tcindex;
        tc[1] = components.length;
        components.push(component);
        if (Sys.Component.isInstanceOfType(component)) {
                        var app = Sys.Application;
            if (component.get_id()) {
                app.addComponent(component);
            }
            if (app.get_isCreatingComponents()) {
                app._createdComponents[app._createdComponents.length] = component;
            }
        }
    }
    function Sys$UI$TemplateContext$_registerIf(result) {
        if (result instanceof Array) {
            for (i = 0, l = result.length; i < l; i++) {
                this._registerComponent(result[i]);
            }
        }
        else if (result && typeof (result) === 'object') {
            this._registerComponent(result);
        }
    }
Sys.UI.TemplateContext.prototype = {
    data: null,
    dataItem: null,
    index: 0,
    parentContext: null,
    containerElement: null,
    components: null,
    nodes: null,
    keys: null,
    _lastIndex: -1,
    dispose: Sys$UI$TemplateContext$dispose,
    getElementById: Sys$UI$TemplateContext$getElementById,
    getInstanceId: Sys$UI$TemplateContext$getInstanceId,
    getObjectByKey: Sys$UI$TemplateContext$getObjectByKey,
    initializeComponents: Sys$UI$TemplateContext$initializeComponents,
    _onInstantiated: Sys$UI$TemplateContext$_onInstantiated,
    _registerComponent: Sys$UI$TemplateContext$_registerComponent,
    _registerIf: Sys$UI$TemplateContext$_registerIf
}
Sys.UI.TemplateContext.registerClass("Sys.UI.TemplateContext", null, Sys.IDisposable);
Sys.UI.TemplateContext._tcindex = 0;
Sys.UI.ITemplateContextConsumer = function Sys$UI$ITemplateContextConsumer() {
    throw Error.notImplemented();
}

    function Sys$UI$ITemplateContextConsumer$get_templateContext() {
        /// <value type="Sys.UI.TemplateContext" mayBeNull="true" locid="P:J#Sys.UI.ITemplateContextConsumer.templateContext"></value>
        throw Error.notImplemented();
    }
    function Sys$UI$ITemplateContextConsumer$set_templateContext(value) {
        throw Error.notImplemented();
    }
Sys.UI.ITemplateContextConsumer.prototype = {
    get_templateContext: Sys$UI$ITemplateContextConsumer$get_templateContext,
    set_templateContext: Sys$UI$ITemplateContextConsumer$set_templateContext
}
Sys.UI.ITemplateContextConsumer.registerInterface("Sys.UI.ITemplateContextConsumer");
Sys.BindingMode = function Sys$BindingMode() {
}






Sys.BindingMode.prototype = {
    auto: 0,
    oneTime: 1,
    oneWay: 2,
    twoWay: 3,
    oneWayToSource: 4
}
Sys.BindingMode.registerEnum("Sys.BindingMode");
Sys.Binding = function Sys$Binding() {
    Sys.Binding.initializeBase(this);
}

















    function Sys$Binding$get_convert() {
        /// <value mayBeNull="true" locid="P:J#Sys.Binding.convert"></value>
        return this._convert || null;
    }
    function Sys$Binding$set_convert(value) {
       this._convert = value;
       this._convertFn = this._resolveFunction(value);
    }
    function Sys$Binding$get_convertBack() {
        /// <value mayBeNull="true" locid="P:J#Sys.Binding.convertBack"></value>
        return this._convertBack || null;
    }
    function Sys$Binding$set_convertBack(value) {
       this._convertBack = value;
       this._convertBackFn = this._resolveFunction(value);
    }
    function Sys$Binding$get_ignoreErrors() {
        /// <value type="Boolean" mayBeNull="false" locid="P:J#Sys.Binding.ignoreErrors"></value>
        return this._ignoreErrors;
    }
    function Sys$Binding$set_ignoreErrors(value) {
       this._ignoreErrors = value;
    }
    function Sys$Binding$get_mode() {
        /// <value type="Sys.BindingMode" mayBeNull="false" locid="P:J#Sys.Binding.mode"></value>
        return this._mode || Sys.BindingMode.auto;
    }
    function Sys$Binding$set_mode(value) {
        if (this._initializing) {
            throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.commonNotAfterInit, "Binding", "mode"));
        }
        this._mode = value;
    }
    function Sys$Binding$get_defaultValue() {
        /// <value mayBeNull="true" locid="P:J#Sys.Binding.defaultValue"></value>
        return this._defaultValue;
    }
    function Sys$Binding$set_defaultValue(value) {
        this._defaultValue = value;
    }
    function Sys$Binding$get_source() {
        /// <value mayBeNull="true" locid="P:J#Sys.Binding.source"></value>
        return this._source || null;
    }
    function Sys$Binding$set_source(value) {
        if (this._initializing) {
            throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.commonNotAfterInit, "Binding", "source"));
        }
        this._source = value;
    }
    function Sys$Binding$get_templateContext() {
        /// <value mayBeNull="true" type="Sys.UI.TemplateContext" locid="P:J#Sys.Binding.templateContext"></value>
        return this._templateContext || null;
    }
    function Sys$Binding$set_templateContext(value) {
        this._templateContext = value;
    }
    function Sys$Binding$get_path() {
        /// <value type="String" mayBeNull="true" locid="P:J#Sys.Binding.path"></value>
        return this._path || "";
    }
    function Sys$Binding$set_path(value) {
        if (this._initializing) {
            throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.commonNotAfterInit, "Binding", "path"));
        }
        this._path = value;
        this._pathArray = value ? value.split('.') : null;
    }
    function Sys$Binding$get_target() {
        /// <value mayBeNull="true" locid="P:J#Sys.Binding.target"></value>
        return this._target || null;
    }
    function Sys$Binding$set_target(value) {
        if (this._initializing) {
            throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.commonNotAfterInit, "Binding", "target"));
        }
        this._target = value;
    }
    function Sys$Binding$get_targetProperty() {
        /// <value type="String" mayBeNull="true" locid="P:J#Sys.Binding.targetProperty"></value>
        return this._targetProperty || "";
    }
    function Sys$Binding$set_targetProperty(value) {
        if (this._initializing) {
            throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.commonNotAfterInit, 
                                                       "Binding", "targetProperty"));
        }
        this._targetProperty = value;
        this._targetPropertyArray = value ? value.split('.') : null;
    }
    function Sys$Binding$_addBinding(element) {
                        if (element.nodeType === 3) {
                        element = element.parentNode;
                        if (!element) return;
        }
        var bindings = element._msajaxBindings;
        if (!bindings) {
           element._msajaxBindings = [this];
        }
        else {
           bindings.push(this);
        }
                if (typeof(element.dispose) !== "function") {
            element.dispose = Sys.Binding._disposeBindings;
        }
    }
    function Sys$Binding$_disposeHandlers() {
        for (var i = 0, l = this._handlers.length; i < l; i++) {
            var entry = this._handlers[i], object = entry[2];
            switch (entry[0]) {
                case "click":                 case "keyup":                 case "change":                     Sys.UI.DomEvent._removeHandler(object, entry[0], entry[1]);
                    break;
                case "optionsChanged":
                    Sys.Observer._removeEventHandler(object, entry[0], entry[1]);
                    break;
                case "propertyChanged":                                                             if (object.remove_propertyChanged) { 
                        object.remove_propertyChanged(entry[1]);
                    }
                    else {
                                                                                                Sys.Observer._removeEventHandler(object, "propertyChanged", entry[1]);
                    }
                    break;
                case "disposing":                     object.remove_disposing(entry[1]);
                    break;
            }
        }
    }
    function Sys$Binding$dispose() {
                this._disposed = true;
        if (this._handlers) {
            this._disposeHandlers();
            delete this._handlers;
        }
        this._convert = null;
        this._convertBack = null;
        this._convertFn = null;
        this._convertBackFn = null;
        this._lastSource = null;
        this._lastTarget = null;
        this._source = null;
        this._target = null;
        this._pathArray = null;
        this._defaultValue = null;
        this._targetPropertyArray = null;
        this._templateContext = null;
        Sys.Binding.callBaseMethod(this, 'dispose');
    }
    function Sys$Binding$_getDefaultMode(target) {
        if (Sys.UI.DomElement.isDomElement(target)) {
            if (target.nodeType === 1) { 
                var tag = target.tagName ? target.tagName.toLowerCase() : null;
                if ((tag === "input") || (tag === "select") || (tag === "textarea")) {
                    return Sys.BindingMode.twoWay;
                }
            }
        }
        else {
            if (Sys.INotifyPropertyChange.isImplementedBy(target)) { 
                return Sys.BindingMode.twoWay;
            }
        }
        return Sys.BindingMode.oneWay;
    }
    function Sys$Binding$_getPropertyFromIndex(obj, path, startIndex, endIndex) {
                        for (var i = startIndex; i <= endIndex; i++) {
            if (obj === null || typeof(obj) === "undefined") {
                                return null;
            }
            obj = this._getPropertyData(obj, path[i]);
        }
        return obj;
    }
    function Sys$Binding$_getPropertyData(obj, name) {
        if (typeof (obj["get_" + name]) === "function") {
            return obj["get_" + name]();
        }
        else {
            return obj[name];
        }
    }
    function Sys$Binding$_hookEvent(object, handlerMethod, componentHandlerMethod) {
                var thisHander;
        if (Sys.UI.DomElement.isDomElement(object)) {
            thisHandler = Function.createDelegate(this, handlerMethod);
            Array.add(this._handlers, ["propertyChanged", thisHandler, object]);                                                                                if (object.add_propertyChanged) { 
                object.add_propertyChanged(thisHandler);
            }
            else {
                                                                Sys.Observer._addEventHandler(object, "propertyChanged", thisHandler);
            }

            var tag = object.tagName ? object.tagName.toLowerCase() : null;
                        if ((tag === "input") || (tag === "select") || (tag === "textarea")) {
                var type = object.type;
                                if ((tag === "input") && type && 
                    ((type.toLowerCase() === "checkbox") || (type.toLowerCase() === "radio"))) {
                        thisHandler = Function.createDelegate(this, handlerMethod);
                        Array.add(this._handlers, ["click", thisHandler, object]);                         Sys.UI.DomEvent.addHandler(object, "click", thisHandler);
                }
                                if (tag === "select") {
                    thisHandler = Function.createDelegate(this, handlerMethod);
                    Array.add(this._handlers, ["click", thisHandler, object]);                     Sys.UI.DomEvent.addHandler(object, "click", thisHandler);
                }
                                if (tag === "select") {
                    thisHandler = Function.createDelegate(this, handlerMethod);
                    Array.add(this._handlers, ["keyup", thisHandler, object]);                     Sys.UI.DomEvent.addHandler(object, "keyup", thisHandler);
                }
                thisHandler = Function.createDelegate(this, handlerMethod);
                Array.add(this._handlers, ["change", thisHandler, object]);                 Sys.UI.DomEvent.addHandler(object, "change", thisHandler);
                this._addBinding(object);
            }
        }
        else {
                                                thisHandler = Function.createDelegate(this, componentHandlerMethod);
            Array.add(this._handlers, ["propertyChanged", thisHandler, object]);             if (object.add_propertyChanged) { 
                object.add_propertyChanged(thisHandler);
            }
            else {
                                                                Sys.Observer._addEventHandler(object, "propertyChanged", thisHandler);
            }
            
            if (Sys.INotifyDisposing.isImplementedBy(object)) {
                thisHandler = Function.createDelegate(this, this._onDisposing);
                Array.add(this._handlers, ["disposing", thisHandler, object]);                 object.add_disposing(thisHandler);
            }
        }
    }
    function Sys$Binding$_onDisposing() {
                this.dispose();
    }
    function Sys$Binding$_resolveFunction(value) {
        var e, ret;
        if (typeof(value) === 'function') {             ret = value;
        }
        else {
            if (typeof(value) !== "string") {
                throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.invalidFunctionName, value));
            }
            ret = Sys.Binding.converters[value];
            if (!ret) {
                try {
                    ret = Type.parse(value);
                }
                catch (e) {
                    throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.functionNotFound, value));
                }
            }
        }
        return ret;
    }
    function Sys$Binding$update(mode) {
        /// <summary locid="M:J#Sys.Binding.update" />
        /// <param name="mode" optional="true" mayBeNull="false"></param>
                        if (!this._initializing) {
            throw Error.invalidOperation(Sys.UI.TemplatesRes.updateBeforeInit);
        }
        mode = mode || this.get_mode();
        if (mode === Sys.BindingMode.oneWayToSource) {
            this._onTargetPropertyChanged(true);
        }
        else{
            this._onSourcePropertyChanged(true);
        }
    }
    function Sys$Binding$_notSet(name) {
       throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.bindingPropertyNotSet, name));
    }

    function Sys$Binding$initialize() {
                                if (this.get_isInitialized()) {
            return;
        }
        Sys.Binding.callBaseMethod(this, 'initialize');        
        var templateContext = this.get_templateContext(),
            source = this.get_source(), target = this.get_target();
                if (!this.get_targetProperty()) {
            this._notSet("targetProperty");
        }
        if (typeof(source) === "string") {
            source = this._resolveReference(source);
            if (source) {
                this.set_source(source);
            }
        }
        if (typeof(target) === "string") {
            target = this._resolveReference(target);
            if (target) {
               this.set_target(target);
            }
        }
        this._initializing = true;
        if ((!source || !target) && templateContext) {
            var callback = Function.createDelegate(this, this._doInitialize),
                callbacks = templateContext.__completedCallbacks;
            if (!callbacks) {
                templateContext.__completedCallbacks = [callback];
            }
            else {
                Array.add(callbacks, callback);
            }
        }
        else {
            this._doInitialize(true, source, target);
        }
    }
    function Sys$Binding$_isChecked(element, name) {
        return (name === "checked") && (element.tagName.toLowerCase() === "input");
    }
    function Sys$Binding$_doInitialize(direct, source, target) {
        source = direct ? source : this.get_source();
        if (!direct && source && (typeof(source) === "string")) {
            var newSource = this._resolveReference(source, "source");
            if (newSource) {
                source = newSource;
                this._initializing = false;
                this.set_source(newSource);
                this._initializing = true;
            }
        }
        target = direct ? target : this.get_target();
        if (!direct && target && (typeof(target) === "string")) {
            var newTarget = this._resolveReference(target, "target");
            if (newTarget) {
                target = newTarget;
                this._initializing = false;
                this.set_target(target);
                this._initializing = true;
            }
        }
        var mode = this.get_mode();
        if (target && (mode === Sys.BindingMode.auto)) {
            mode = this._getDefaultMode(target);
        }
                this.update(mode);

        if (mode !== Sys.BindingMode.oneTime) {
            this._handlers = [];
            if (source) {
                                if (mode !== Sys.BindingMode.oneWayToSource) {
                    this._hookEvent(source, this._onSourcePropertyChanged, this._onComponentSourceChanged);
                }
                else if (Sys.UI.DomElement.isDomElement(source)) {
                    this._addBinding(source);
                }
            }
            if (target) {
                                if (mode !== Sys.BindingMode.oneWay) {
                    this._hookEvent(target, this._onTargetPropertyChanged, this._onComponentTargetChanged);
                }
                else if (Sys.UI.DomElement.isDomElement(target)) {
                    this._addBinding(target);
                }
            }
        }
                                        var _this = this;
        function listen(e, p) {
            if (Sys.UI.DomElement.isDomElement(e) && /select/i.test(e.tagName) && /selectedIndex|value/.test(p)) {
                var handler = Function.createDelegate(_this, _this._optionsUpdated);
                Sys.Observer.addEventHandler(e, "optionsChanged", handler);
                _this._handlers.push(["optionsChanged", handler, e]);
            }
        }
                if (source && mode >= 3) listen(source, this.get_path());
                if (target && mode <= 3) listen(target, this.get_targetProperty());
    }
    function Sys$Binding$_optionsUpdated(select) {
        if (!this._disposed) {
            this.update(select === this.get_source() ? 4 : 2);
        }
    }
    function Sys$Binding$_onComponentSourceChanged(sender, args) {
                if (this._disposed) return;
        var name = args.get_propertyName();
        if ((name === "") || (!this._pathArray) || (name === this._pathArray[0])) {
            this._onSourcePropertyChanged();
        }
    }
    function Sys$Binding$_onComponentTargetChanged(sender, args) {
                if (this._disposed) return;
        var name = args.get_propertyName();
        if ((name === "") || (name ===  this._targetPropertyArray[0])) {
            this._onTargetPropertyChanged();
        }
    }
    function Sys$Binding$_onSourcePropertyChanged(force) {
                if (this._disposed) return;
                var target = this.get_target(),
            source = this.get_source();
        if (!target) return;
        source = (source && this._pathArray)
                    ? this._getPropertyFromIndex(source, this._pathArray, 0, this._pathArray.length - 1)
                    : source;
        if (!this._updateSource && (force || (source !== this._lastSource))) {
            try {
                this._updateTarget = true;
                this._lastSource = this._lastTarget = source;
                if (this._convertFn) {
                    if (this._ignoreErrors) {
                        try {
                            source = this._convertFn(source, this);
                        }
                        catch (e) {}
                    }
                    else {
                        source = this._convertFn(source, this);
                    }
                }
                if ((source === null) || (typeof(source) === "undefined")) {
                    source = this.get_defaultValue();
                }
                if (this._targetProperty && this._targetProperty.startsWith("class:")) {
                    var className = this._targetProperty.substr(6).trim();
                    source ? Sys.UI.DomElement.addCssClass(target, className) : Sys.UI.DomElement.removeCssClass(target, className);
                }
                else {
                    var targetArrayLength = this._targetPropertyArray.length;
                    target = this._getPropertyFromIndex(target, this._targetPropertyArray, 0, targetArrayLength - 2);
                    if ((target !== null) && (typeof(target) !== "undefined")) {
                        var name = this._targetPropertyArray[targetArrayLength - 1],
                            isElement = Sys.UI.DomElement.isDomElement(target);
                        if (isElement) {
                            source = Sys.UI.Template._checkAttribute(name, source);
                            if (name === "innerHTML") {
                                Sys.Application._clearContent(target);
                            }
                        }
                                                                                                Sys.Observer._setValue(target, name, source);
                        if (source && isElement && this._isChecked(target, name)) {
                                                                                                                var a = document.createAttribute(name);
                            a.nodeValue = name;
                            target.setAttributeNode(a);
                        }
                    }
                }
            }
            finally {
                this._updateTarget = false;
            }
        }
    }
    function Sys$Binding$_onTargetPropertyChanged(force) {
                if (this._disposed) return;
                var target = this.get_target(),
            source = this.get_source();
        if (!source) return;
        target = !target ? null : this._getPropertyFromIndex(target, this._targetPropertyArray, 
                                                0, this._targetPropertyArray.length - 1);
        if (!this._updateTarget && (force || (target !== this._lastTarget))) {
            try {
                this._updateSource = true;
                this._lastTarget = this._lastSource = target;
                if (this._convertBackFn) {
                    if (this._ignoreErrors) {
                        try {
                            target = this._convertBackFn(target, this);
                        }
                        catch (e) {}
                    }
                    else {
                        target = this._convertBackFn(target, this);
                    }
                }
                if (this._pathArray) {
                    var sourceArrayLength = this._pathArray.length,
                        source = this._getPropertyFromIndex(this.get_source(), this._pathArray, 0, sourceArrayLength - 2);
                    if ((source !== null) && (typeof(source) !== "undefined")) {                     
                        var name = this._pathArray[sourceArrayLength - 1],
                            isElement = Sys.UI.DomElement.isDomElement(source);
                        if (isElement) {
                            target = Sys.UI.Template._checkAttribute(name, target);
                            if (name === "innerHTML") {
                                Sys.Application._clearContent(source);
                            }
                        }
                                                                                                Sys.Observer._setValue(source, name, target);
                        if (target && isElement && this._isChecked(source, name)) {
                                                                                                                var a = document.createAttribute(name);
                            a.nodeValue = name;
                            source.setAttributeNode(a);
                        }
                    }
                }
            }
            finally {
                this._updateSource = false;
            }
        }
    }
    function Sys$Binding$_resolveReference(str, propName) {
        var tc = this.get_templateContext(),
            parts = str.split("."),
            key = parts[0],
            ref = (tc && tc.getObjectByKey(key)) ||
                Sys.Application.findComponent(key) ||
                ((tc || Sys.UI.DomElement).getElementById(key));
        if ((ref === null) || (typeof(ref) === "undefined")) {
            if (propName) {
                throw Error.invalidOperation(String.format(Sys.UI.TemplatesRes.unresolvedReference, key));
            }
            return null;
        }
        if (parts.length > 1) {
            ref = this._getPropertyFromIndex(ref, parts, 1, parts.length-1);
            if ((ref === null) || (typeof(ref) === "undefined")) {
                if (propName) {
                    this._notSet(propName);
                }
            }
        }
        return ref;
    }
Sys.Binding.prototype = {
    _convert: null,
    _convertBack: null,
    _convertFn: null,
    _convertBackFn: null,
    _handlers: null,
    _ignoreErrors: false,
    _mode: Sys.BindingMode.auto,
    _path: null,
    _targetProperty: null,
    _source: null,
    _target: null,
    _updateSource: false,
    _updateTarget: false,
    _defaultValue: null,
    _initializing: false,
    _templateContext: null,
    get_convert: Sys$Binding$get_convert,
    set_convert: Sys$Binding$set_convert,
    get_convertBack: Sys$Binding$get_convertBack,
    set_convertBack: Sys$Binding$set_convertBack,
    get_ignoreErrors: Sys$Binding$get_ignoreErrors,
    set_ignoreErrors: Sys$Binding$set_ignoreErrors,
    get_mode: Sys$Binding$get_mode,
    set_mode: Sys$Binding$set_mode,
    get_defaultValue: Sys$Binding$get_defaultValue,
    set_defaultValue: Sys$Binding$set_defaultValue,
    get_source: Sys$Binding$get_source,
    set_source: Sys$Binding$set_source,
    get_templateContext: Sys$Binding$get_templateContext,
    set_templateContext: Sys$Binding$set_templateContext,
    get_path: Sys$Binding$get_path,
    set_path: Sys$Binding$set_path,
    get_target: Sys$Binding$get_target,
    set_target: Sys$Binding$set_target,
    get_targetProperty: Sys$Binding$get_targetProperty,
    set_targetProperty: Sys$Binding$set_targetProperty,
    _addBinding: Sys$Binding$_addBinding,
    _disposeHandlers: Sys$Binding$_disposeHandlers,
    dispose: Sys$Binding$dispose,
    _getDefaultMode: Sys$Binding$_getDefaultMode,
    _getPropertyFromIndex: Sys$Binding$_getPropertyFromIndex,
    _getPropertyData: Sys$Binding$_getPropertyData,
    _hookEvent: Sys$Binding$_hookEvent,
    _onDisposing: Sys$Binding$_onDisposing,
    _resolveFunction: Sys$Binding$_resolveFunction,
    update: Sys$Binding$update,
    _notSet: Sys$Binding$_notSet,
        initialize: Sys$Binding$initialize,
    _isChecked: Sys$Binding$_isChecked,
    _doInitialize: Sys$Binding$_doInitialize,
    _optionsUpdated: Sys$Binding$_optionsUpdated,
    _onComponentSourceChanged: Sys$Binding$_onComponentSourceChanged,
    _onComponentTargetChanged: Sys$Binding$_onComponentTargetChanged,
    _onSourcePropertyChanged: Sys$Binding$_onSourcePropertyChanged,
    _onTargetPropertyChanged: Sys$Binding$_onTargetPropertyChanged,
    _resolveReference: Sys$Binding$_resolveReference
}
Sys.Binding._disposeBindings = function Sys$Binding$_disposeBindings() {
        var bindings = this._msajaxBindings;    
    if (bindings) {
        for(var i = 0, l = bindings.length; i < l; i++) {
            bindings[i].dispose();
        }
    }
    this._msajaxBindings = null;
    
        if (this.control && typeof(this.control.dispose) === "function") {
        this.control.dispose();
    }

    if (this.dispose === Sys.Binding._disposeBindings) {
        this.dispose = null;
    }
}
Sys.Binding.registerClass("Sys.Binding", Sys.Component, Sys.UI.ITemplateContextConsumer);
Sys.Binding.converters = {};

Sys.Application.registerMarkupExtension(
"binding", 
function(component, targetProperty, templateContext, properties) {
    var binding = new Sys.Binding();
    binding.set_source(templateContext.dataItem);
    binding.set_templateContext(templateContext);
    binding.set_target(component);
    binding.set_targetProperty(targetProperty);
            
    for (var p in properties) {
        if (properties.hasOwnProperty(p)) {
            var value = properties[p], isString = (typeof(value) === "string");
            switch (p) {
                case "$default": 
                    binding.set_path(value);
                    break;
                case "mode":
                    Sys.Observer.setValue(binding, p, isString ? Sys.BindingMode.parse(value) : value);
                    break;
                case "ignoreErrors":
                    Sys.Observer.setValue(binding, p, isString ? Boolean.parse(value) : value);
                    break;
                default:
                    Sys.Observer.setValue(binding, p, value);
                    var setter = binding["set_" + p];
                    setter ? setter.call(binding, value) : binding[p] = value;
                    break;
            }
        }
    }
    templateContext.components.push(binding);
    binding.initialize();
}, 
false);

Sys.Application.registerMarkupExtension(
"ref",
function(component, targetProperty, templateContext, properties) {
    var binding = new Sys.Binding();
    binding.set_source(templateContext.dataItem);
    binding.set_templateContext(templateContext);
    binding.set_target(component);
    binding.set_targetProperty(targetProperty);
    binding.set_mode(Sys.BindingMode.oneTime);
    for (var p in properties) {
        if (properties.hasOwnProperty(p)) {
            var value = properties[p], isString = (typeof(value) === "string");
            switch (p) {
                case "source":
                case "$default":
                    if (isString) {
                       var i = value.indexOf('.');
                       if (i >= 0) {
                          binding.set_path(value.substr(i + 1));
                          binding.set_source(value.substr(0, i));
                       }
                       else {
                          binding.set_source(value);
                       }
                    }
                    else {
                        binding.set_source(value);
                    }
                    break;
                case "mode":
                    Sys.Observer.setValue(binding, p, isString ? Sys.BindingMode.parse(value) : value);
                    break;
                case "ignoreErrors":
                    Sys.Observer.setValue(binding, p, isString ? Boolean.parse(value) : value);
                    break;
                default:
                    Sys.Observer.setValue(binding, p, value);
                    break;
            }
        }
    }
    templateContext.components.push(binding);
    binding.initialize();
},
false);
Sys.UI.DataView = function Sys$UI$DataView(element) {
    /// <summary locid="M:J#Sys.UI.DataView.#ctor" />
    /// <param name="element"></param>
    Sys.UI.DataView.initializeBase(this, [element]);
}
























    function Sys$UI$DataView$add_command(handler) {
    /// <summary locid="E:J#Sys.UI.DataView.command" />
        this.get_events().addHandler("command", handler);
    }
    function Sys$UI$DataView$remove_command(handler) {
        this.get_events().removeHandler("command", handler);
    }
    function Sys$UI$DataView$add_rendering(handler) {
    /// <summary locid="E:J#Sys.UI.DataView.rendering" />
        this.get_events().addHandler("rendering", handler);
    }
    function Sys$UI$DataView$remove_rendering(handler) {
        this.get_events().removeHandler("rendering", handler);
    }
    function Sys$UI$DataView$add_rendered(handler) {
    /// <summary locid="E:J#Sys.UI.DataView.rendered" />
        this.get_events().addHandler("rendered", handler);
    }
    function Sys$UI$DataView$remove_rendered(handler) {
        this.get_events().removeHandler("rendered", handler);
    }
    function Sys$UI$DataView$add_itemRendered(handler) {
    /// <summary locid="E:J#Sys.UI.DataView.itemRendered" />
        this.get_events().addHandler("itemRendered", handler);
    }
    function Sys$UI$DataView$remove_itemRendered(handler) {
        this.get_events().removeHandler("itemRendered", handler);
    }
    function Sys$UI$DataView$add_itemRendering(handler) {
    /// <summary locid="E:J#Sys.UI.DataView.itemRendering" />
        this.get_events().addHandler("itemRendering", handler);
    }
    function Sys$UI$DataView$remove_itemRendering(handler) {
        this.get_events().removeHandler("itemRendering", handler);
    }
    function Sys$UI$DataView$add_fetchFailed(handler) {
    /// <summary locid="E:J#Sys.UI.DataView.fetchFailed" />
        this.get_events().addHandler("fetchFailed", handler);
    }
    function Sys$UI$DataView$remove_fetchFailed(handler) {
        this.get_events().removeHandler("fetchFailed", handler);
    }
    function Sys$UI$DataView$add_fetchSucceeded(handler) {
    /// <summary locid="E:J#Sys.UI.DataView.fetchSucceeded" />
        this.get_events().addHandler("fetchSucceeded", handler);
    }
    function Sys$UI$DataView$remove_fetchSucceeded(handler) {
        this.get_events().removeHandler("fetchSucceeded", handler);
    }
    function Sys$UI$DataView$get_viewData() {
        /// <value mayBeNull="true" locid="P:J#Sys.UI.DataView.viewData"></value>
        return this._viewData || null;
    }
    function Sys$UI$DataView$get_data() {
        /// <value mayBeNull="true" locid="P:J#Sys.UI.DataView.data"></value>
        return this._data;
    }
    function Sys$UI$DataView$set_data(value) {
        if (!this._setData || (this._data !== value)) {
            this._loadData(value);
        }
    }
    function Sys$UI$DataView$get_dataProvider() {
        /// <value mayBeNull="true" locid="P:J#Sys.UI.DataView.dataProvider"></value>
        return this._provider || null;
    }
    function Sys$UI$DataView$set_dataProvider(value) {
        this._dataProvider = this._wsp = this._wspClass = null;
        if (Sys.Data.IDataProvider.isImplementedBy(value)) {
            this._dataProvider = value;
        }
        else if (Sys.Net.WebServiceProxy.isInstanceOfType(value)) {
            this._wsp = value;
        }
        else if (Type.isClass(value) && value.inheritsFrom(Sys.Net.WebServiceProxy) && typeof(value.get_path) === "function") {
            this._wspClass = value;
        }
        else if ((value !== null) && (typeof(value) !== "string")) {
            throw Error.argument("dataProvider", Sys.UI.TemplatesRes.invalidDataProviderType);
        }
        this._provider = value;
        if (this.get_autoFetch() && this._isActive()) {
            if (value) {
                this._doAutoFetch();
            }
        }
        else {
            this._stale = true;
        }
    }
    function Sys$UI$DataView$get_autoFetch() {
        /// <value locid="P:J#Sys.UI.DataView.autoFetch"></value>
        return this._autoFetch;
    }
    function Sys$UI$DataView$set_autoFetch(value) {
        var was = this._autoFetch;
        if (typeof(value) === "string") {
            value = Boolean.parse(value);
        }
        else if (typeof(value) !== "boolean") {
            throw Error.invalidOperation(Sys.UI.TemplatesRes.stringOrBoolean);
        }
        this._autoFetch = value;
        if (this._isActive() && this._stale && !was && value) {
                                    this._doAutoFetch();
        }
    }
    function Sys$UI$DataView$get_isFetching() {
        /// <value type="Boolean" locid="P:J#Sys.UI.DataView.isFetching"></value>
        return this._fetching;
    }
    function Sys$UI$DataView$get_httpVerb() {
        /// <value type="String" locid="P:J#Sys.UI.DataView.httpVerb"></value>
        return this._httpVerb || "POST";
    }
    function Sys$UI$DataView$set_httpVerb(value) {
        this._httpVerb = value;
    }
    function Sys$UI$DataView$get_contexts() {
        /// <value type="Array" elementType="Sys.UI.TemplateContext" elementMayBeNull="true" locid="P:J#Sys.UI.DataView.contexts"></value>
        return this._contexts;
    }
    function Sys$UI$DataView$get_fetchParameters() {
        /// <value type="Object" mayBeNull="true" locid="P:J#Sys.UI.DataView.fetchParameters"></value>
        return this._fetchParameters;
    }
    function Sys$UI$DataView$set_fetchParameters(value) {
        if (this._fetchParameters !== value) {
            this._fetchParameters = value;
            if (this.get_autoFetch() && this._isActive()) {
                this._doAutoFetch();
            }
            else {
                this._stale = true;
            }
        }
    }
    function Sys$UI$DataView$get_selectedData() {
        /// <value mayBeNull="true" locid="P:J#Sys.UI.DataView.selectedData"></value>
        var selectedIndex = this.get_selectedIndex();
        if (selectedIndex > -1) {
            var data = this.get_viewData();
            if ((data instanceof Array) && (selectedIndex < data.length)) {
                return data[selectedIndex];
            }
        }
        return null;
    }
    function Sys$UI$DataView$get_selectedIndex() {
        /// <value locid="P:J#Sys.UI.DataView.selectedIndex"></value>
        return this._selectedIndex;
    }
    function Sys$UI$DataView$set_selectedIndex(value) {
        value = this._validateIndexInput(value);
        if (value < -1) {
            throw Error.argumentOutOfRange("value", value);
        }
        if (!this.get_isInitialized() || !this._setData) {
            this._selectedIndex = value;
        }
        else {
            this._applySelectedIndex(value);
        }
    }
    function Sys$UI$DataView$get_initialSelectedIndex() {
        /// <value locid="P:J#Sys.UI.DataView.initialSelectedIndex"></value>
        return this._initialSelectedIndex;
    }
    function Sys$UI$DataView$set_initialSelectedIndex(value) {
        value = this._validateIndexInput(value);
        if (value < -1) {
            throw Error.argumentOutOfRange("value", value);
        }
        if (value !== this.get_initialSelectedIndex()) {
            this._initialSelectedIndex = value;
            this._raiseChanged("initialSelectedIndex");
        }
    }
    function Sys$UI$DataView$get_selectedItemClass() {
        /// <value type="String" locid="P:J#Sys.UI.DataView.selectedItemClass"></value>
        return this._selectedItemClass || "";
    }
    function Sys$UI$DataView$set_selectedItemClass(value) {
        var name = this.get_selectedItemClass();
        if (value !== name) {
            var index = this.get_selectedIndex();
            this._addRemoveCssClass(index, name, Sys.UI.DomElement.removeCssClass);
            this._addRemoveCssClass(index, value, Sys.UI.DomElement.addCssClass);
            this._selectedItemClass = value;
        }
    }
    function Sys$UI$DataView$get_timeout() {
        /// <value type="Number" integer="true" locid="P:J#Sys.UI.DataView.timeout"></value>
        return this._timeout;
    }
    function Sys$UI$DataView$set_timeout(value) {
        this._timeout = value;
    }
    function Sys$UI$DataView$get_fetchOperation() {
        /// <value mayBeNull="true" locid="P:J#Sys.UI.DataView.fetchOperation"></value>
        return this._query || "";
    }
    function Sys$UI$DataView$set_fetchOperation(value) {
        if (this._query !== value) {
            this._query = value;
            if (this.get_autoFetch() && this._isActive()) {
                if (value) {
                    this._doAutoFetch();
                }
            }
            else {
                this._stale = true;
            }
        }
    }
    function Sys$UI$DataView$get_itemPlaceholder() {
        /// <value mayBeNull="true" locid="P:J#Sys.UI.DataView.itemPlaceholder"></value>
        return this._placeholder || null;
    }
    function Sys$UI$DataView$set_itemPlaceholder(value) {
        if (this._placeholder !== value) {
            this._placeholder = value;
            this._dirty = true;
            this._raiseChanged("itemPlaceholder");
        }
    }
    function Sys$UI$DataView$get_templateContext() {
        /// <value mayBeNull="true" type="Sys.UI.TemplateContext" locid="P:J#Sys.UI.DataView.templateContext"></value>
        return this._parentContext || null;
    }
    function Sys$UI$DataView$set_templateContext(value) {
        if (this._parentContext !== value) {
            this._parentContext = value;
            this._dirty = true;
            this._raiseChanged("templateContext");
        }
    }
    function Sys$UI$DataView$get_itemTemplate() {
        /// <value mayBeNull="true" locid="P:J#Sys.UI.DataView.itemTemplate"></value>
        return this._template || null;
    }
    function Sys$UI$DataView$set_itemTemplate(value) {
        if (this._template !== value) {
            this._template = value;
            this._dirty = true;
            if (this._dvTemplate) {
                this._dvTemplate.dispose();
                this._dvTemplate = null;
            }
            if (this._isActive()) {
                this.raisePropertyChanged("itemTemplate");
                this.refresh();
            }
            else {
                this._changed = true;
            }
        }
    }
    function Sys$UI$DataView$_applySelectedIndex(value, force) {
        var currentIndex = this.get_selectedIndex();
        if (force || (value !== currentIndex)) {
            var data = this.get_viewData(); 
            if (!(data instanceof Array)) {
                data = [data];
            }
            var outOfRange = (value < -1) || (value >= data.length);
            if (outOfRange) {
                throw Error.argumentOutOfRange("value", value);
            }
            this._selectedIndex = value;
            this._currentData = ((value === -1) || outOfRange) ? null : data[value];
            var className = this.get_selectedItemClass();
            this._addRemoveCssClass(currentIndex, className, Sys.UI.DomElement.removeCssClass);
            this._addRemoveCssClass(value, className, Sys.UI.DomElement.addCssClass);
            if (!this.get_isUpdating()) {
                if (value !== currentIndex) {
                    this.raisePropertyChanged('selectedIndex');
                }
            }
            else {
                this._changed = true;
            }
        }
        if (!this.get_isUpdating()) {
            this._raiseSelectedData();
        }
        else {
            this._changed = true;
        }
    }
    function Sys$UI$DataView$_addRemoveCssClass(index, className, addRemove) {
        if (className && (index > -1)) {
            var items = this.get_contexts(), l = items ? items.length : -1;
            if (l && (index < l)) {
                var elementsSet = items[index].nodes;
                if (elementsSet) {
                    for (var i = 0, len = elementsSet.length; i < len; i++) {
                        var element = elementsSet[i];
                        if (element.nodeType === 1) {
                            addRemove(element, className);
                        }
                    }
                }
            }
        }
    }
    function Sys$UI$DataView$_collectionChanged(sender, args) {
        var oldSelected = this._currentData,
            changes = args.get_changes(),
            selectedIndex = this.get_selectedIndex(), oldIndex = selectedIndex;
        if (this._isActive()) {
                        this._changing = true;
            this.refresh();
        }
        else {
            this._dirty = true;
            return;
        }
        var data = this.get_viewData();
        if ((selectedIndex !== -1) && (selectedIndex < data.length) &&
            (data[selectedIndex] === oldSelected)) {
                                    return;
        }
                for (var i = 0, l = changes.length; i < l; i++) {
            var change = changes[i];
            if (change.action === Sys.NotifyCollectionChangedAction.add) {
                if (selectedIndex >= change.newStartingIndex) {
                    selectedIndex += change.newItems.length;
                }
            }
            else {
                var index = change.oldStartingIndex, len = change.oldItems.length, lastIndex = index + len - 1;
                if (selectedIndex > lastIndex) {
                                        selectedIndex -= len;
                }
                else if (selectedIndex >= index) {
                                                            selectedIndex = -1;
                    break;
                }
            }
        }
        if (selectedIndex !== oldIndex) {
            this.set_selectedIndex(selectedIndex);
        }
    }
    function Sys$UI$DataView$_elementContains(container, element, excludeSelf) {
        if (container === element) {
            return !excludeSelf;
        }
        do {
            element = element.parentNode;
            if (element === container) return true;
        }
        while (element);
        return false;
    }
    function Sys$UI$DataView$_raiseChanged(name) {
        if (this._isActive()) {
            this.raisePropertyChanged(name);
        }
        else {
            this._changed = true;
        }
    }
    function Sys$UI$DataView$_raiseFailed(request, error) {
	    var args = new Sys.Net.WebRequestEventArgs(request ? request.get_executor() : null, error);
        this.onFetchFailed(args);
        this._raiseEvent("fetchFailed", args);
    }
    function Sys$UI$DataView$_raiseSelectedData() {
        if (this._lastData !== this._currentData) {
            this._lastData = this._currentData;
            this.raisePropertyChanged('selectedData');
        }
    }
    function Sys$UI$DataView$_raiseSucceeded(request, result) {
	    var args = new Sys.Net.WebRequestEventArgs(request ? request.get_executor() : null, null, result);
        this.onFetchSucceeded(args);
        this._raiseEvent("fetchSucceeded", args);
    }
    function Sys$UI$DataView$_ensureTemplate(template) {
        if (!Sys.UI.Template.isInstanceOfType(template)) {
            template = Sys.UI.DomElement.resolveElement(template);
            if (template) {
                return new Sys.UI.Template(template);
            }
        }
        else {
            return template;
        }
        return null;
    }
    function Sys$UI$DataView$_getTemplate() {
                                                if (this._dvTemplate) return this._dvTemplate;
        var template = this.get_itemTemplate();
        if (!template) {
                        var element = this.get_element();
            if (Sys.UI.Template._isTemplate(element)) {
                this._dvTemplate = template = new Sys.UI.Template(element);
            }
        }
        else if (!Sys.UI.Template.isInstanceOfType(template)) {
                        template = Sys.UI.DomElement.resolveElement(template);
            var e = this.get_element();
            if ((e !== template) && this._elementContains(e, template, true)) {
                throw Error.invalidOperation(Sys.UI.TemplatesRes.misplacedTemplate);
            }
            this._dvTemplate = template = new Sys.UI.Template(template);
        }
        else {
                        if (this._elementContains(this.get_element(), template.get_element(), true)) {
                throw Error.invalidOperation(Sys.UI.TemplatesRes.misplacedTemplate);
            }
        }
        return template;
    }
    function Sys$UI$DataView$_loadData(value) {
        this._swapData(this._data, value);
        this._data = value;
        this._setData = true;
        this._stale = false;
        this._dirty = true;
        if (this._isActive()) {
            this.refresh();
            this.raisePropertyChanged("data");
        }
        else {
            this._changed = true;
        }
    }
    function Sys$UI$DataView$_resetSelectedIndex() {
        var data = this.get_viewData(), initialSelectedIndex = this.get_initialSelectedIndex(),
            selectedIndex = this.get_selectedIndex();
        if (!(data instanceof Array) || (initialSelectedIndex >= data.length)) {
            if (selectedIndex !== -1) {
                this.set_selectedIndex(-1);
                return;
            }
        } 
        else if (selectedIndex !== initialSelectedIndex) {
            this.set_selectedIndex(initialSelectedIndex);
            return;
        }
        this._currentData = this.get_selectedData();
        this._raiseSelectedData();
    }
    function Sys$UI$DataView$_initializeResults() {
        for (var i = 0, l = this._contexts.length; i < l; i++) {
            var ctx = this._contexts[i];
            if (ctx) ctx.initializeComponents();
        }
    }
    function Sys$UI$DataView$_isActive() {
        return (this.get_isInitialized() && !this.get_isUpdating());
    }
    function Sys$UI$DataView$_raiseEvent(name, args) {
        var handler = this.get_events().getHandler(name);
        if (handler) {
            handler(this, args);
        }
    }
    function Sys$UI$DataView$_raiseCommand(args) {
        this.onCommand(args);
        this._raiseEvent("command", args);
    }
    function Sys$UI$DataView$_raiseItem(type, args) {
        this['onItem' + type](args);
        this._raiseEvent('item' + type, args);
    }
    function Sys$UI$DataView$abortFetch() {
        /// <summary locid="M:J#Sys.UI.DataView.abortFetch" />
        if (this._request) {
            this._request.get_executor().abort();
            this._request = null;
        }
        if (this._fetching) {
            this._fetching = false;
            this._raiseChanged("isFetching");
        }
    }
    function Sys$UI$DataView$onBubbleEvent(source, args) {
        /// <summary locid="M:J#Sys.UI.DataView.onBubbleEvent" />
        /// <param name="source"></param>
        /// <param name="args" type="Sys.EventArgs"></param>
        /// <returns type="Boolean"></returns>
        if (Sys.CommandEventArgs.isInstanceOfType(args)) {
            this._raiseCommand(args);
            if (args.get_cancel()) {
                return true;
            }
            else {
                var name = args.get_commandName();
                if (name && (name.toLowerCase() === "select")) {
                    var index = this._findContextIndex(source);
                    if (index !== -1) {
                        this.set_selectedIndex(index);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    function Sys$UI$DataView$onRendering(args) {
        /// <summary locid="M:J#Sys.UI.DataView.onRendering" />
        /// <param name="args" type="Sys.Data.DataEventArgs"></param>
    }
    function Sys$UI$DataView$onFetchFailed(args) {
        /// <summary locid="M:J#Sys.UI.DataView.onFetchFailed" />
        /// <param name="args" type="Sys.Net.WebRequestEventArgs"></param>
    }
    function Sys$UI$DataView$onFetchSucceeded(args) {
        /// <summary locid="M:J#Sys.UI.DataView.onFetchSucceeded" />
        /// <param name="args" type="Sys.Net.WebRequestEventArgs"></param>
    }
    function Sys$UI$DataView$_doAutoFetch() {
        var e;
        try {
            if (this._dataProvider || this._provider) {
                this.fetchData();
                this._stale = false;
            }
        }
        catch (e) {
            this._raiseFailed(null, null);
        }
    }
    function Sys$UI$DataView$_findContextIndex(source) {
        var containers = this._containers;
        if (source && containers) {
            var results = this.get_contexts();
            if (results) {
                var element = Sys.UI.DomElement.resolveElement(source);
                if (element) {
                                        var parent = element.parentNode, dvElement = this.get_element(), cindex = -1;
                    while (parent && ((cindex = Sys._indexOf(containers, parent)) < 0) && (parent !== dvElement)) {
                        element = parent;
                        parent = parent.parentNode;
                    }
                    if (cindex > -1) {
                        var container = containers[cindex];
                                                                        for (var i = 0, l = results.length; i < l; i++) {
                            var result = results[i];
                            if ((result.containerElement === container) && (Sys._indexOf(result.nodes, element) > -1)) {
                                return i;
                            }
                        }
                    }
                }
            }
        }
        return -1;
    }
    function Sys$UI$DataView$findContext(source) {
        /// <summary locid="M:J#Sys.UI.DataView.findContext" />
        /// <param name="source"></param>
        /// <returns type="Sys.UI.TemplateContext" mayBeNull="true"></returns>
        var index = this._findContextIndex(source);
        return (index !== -1) ? this.get_contexts()[index] : null;
    }
    function Sys$UI$DataView$_clearContainer(container, placeholder) {
        var count = placeholder ? placeholder.__msajaxphcount : -1;
        if ((count > -1) && placeholder) placeholder.__msajaxphcount = 0;
                                                        if (count < 0) {
            if (placeholder) {
                                                container.removeChild(placeholder);
            }
            Sys.Application.disposeElement(container, true);
            try {
                container.innerHTML = "";
            }
            catch(err) {
                                var child;
                while((child = container.firstChild)) {
                    container.removeChild(child);
                }
            }
            if (placeholder) {
                container.appendChild(placeholder);
            }
        }
        else if (count > 0) {
                                                                                                var i, l, start, children = container.childNodes;
            for (i = 0, l = children.length; i < l; i++) {
                if (children[i] === placeholder) {
                    break;
                }
            }
            start = i - count;
            for (i = 0; i < count; i++) {
                var element = children[start];
                                Sys.Application.disposeElement(element, false);
                container.removeChild(element);
            }
        }
    }
    function Sys$UI$DataView$_clearContainers(placeholders) {
        var err;
        for (var i = 0, l = placeholders.length; i < l; i++) {
                                                var ph = placeholders[i],
                container = ph ? ph.parentNode : this.get_element();
            this._clearContainer(container, ph);
        }
    }
    function Sys$UI$DataView$_isAlone(container, ph) {
                                                                                                                        var childNodes = container.childNodes;
        if (childNodes.length === 1) return true;
                var node = container.firstChild, notWhitespace = /\S/;
        while (node) {
            if (node !== ph) {
                var type = node.nodeType;
                if (type === 3) {
                    if (notWhitespace.test(node.nodeValue)) return false;
                }
                else if (type !== 8) {
                    return false;
                }
            }
            node = node.nextSibling;
        }
        return true;
    }
    function Sys$UI$DataView$refresh() {
        /// <summary locid="M:J#Sys.UI.DataView.refresh" />
        if (!this._setData) return;
        var collectionChange = this._changing;
        this._changing = false;
        var data = this.get_data(),
            renderArgs = new Sys.Data.DataEventArgs(data);
        renderArgs._itemTemplate = this._getTemplate();
        renderArgs._placeholder = Sys.UI.DomElement.resolveElement(this.get_itemPlaceholder());
        this.onRendering(renderArgs);
        this._raiseEvent("rendering", renderArgs);
        if (renderArgs.get_cancel()) return;
        data = renderArgs.get_data();
        this._viewData = data;

        var template = this._ensureTemplate(renderArgs._itemTemplate);
        this._dirty = false;
        var ph = Sys.UI.DomElement.resolveElement(renderArgs._placeholder),
            pctx = this.get_templateContext(),
            element = this.get_element(),
            result, itemTemplate, args;
        if (this._placeholders) {
                                    this._clearContainers(this._placeholders);
        }
        var list = data;
        var len;
        if ((data === null) || (typeof(data) === "undefined")) {
            len = 0;
        }
        else if (!(data instanceof Array)) {
            list = [data];
            len = 1;
        }
        else {
            len = data.length;
        }
        function clearAndShow() {
            if (!this._cleared) {
                if (Sys.UI.Template._isTemplate(element)) {
                    var selfTemplate = new Sys.UI.Template(element);
                    selfTemplate._ensureCompiled();
                    selfTemplate.dispose();
                    Sys.UI.DomElement.removeCssClass(element, "sys-template");
                }
                this._clearContainer(element, null);
                element.__msajaxphcount = -1;
                this._cleared = true;
            }
        }
        if (!len && template && template.get_element() === element) {
                                                            clearAndShow.call(this);
        }
        var currentPh, lastPh, placeholders, container, containers, optionsChanged;
        this._placeholders = placeholders = [];
        this._containers = containers = [];
        this._contexts = new Array(len);
        for (var i = 0; i < len; i++) {
            var item = list[i];
            args = new Sys.UI.DataViewItemEventArgs(item);
            args._itemTemplate = template;
            args._placeholder = ph;
            this._raiseItem("Rendering", args);
                        itemTemplate = this._ensureTemplate(args._itemTemplate);
            currentPh = Sys.UI.DomElement.resolveElement(args._placeholder);
                                    currentPh = currentPh ? (currentPh.__msajaxphoption || currentPh) : null;
            if (currentPh !== lastPh) {
                container = currentPh ? currentPh.parentNode : element;
                if (Sys._indexOf(placeholders, currentPh) < 0) {
                    if (currentPh) {
                        if (/option/i.test(currentPh.tagName) && /select/i.test(container.tagName)) {
                                                                                                                                                                        var newPh = document.createElement('_hiddenPlaceholder');
                            container.replaceChild(newPh, currentPh);
                            currentPh.__msajaxphoption = newPh;
                            newPh.appendChild(currentPh);
                            currentPh = newPh;
                        }
                                                currentPh.style.display = 'none';
                                                                        var phcount = currentPh.__msajaxphcount;
                        if (typeof(phcount) === "undefined" && this._isAlone(container, currentPh)) {
                                                                                                                currentPh.__msajaxphcount = -1;
                            this._clearContainer(container, currentPh);
                        }
                    }
                    else {
                                                                                                                                                clearAndShow.call(this);
                    }
                                                            placeholders.push(currentPh);
                                                            if (Sys._indexOf(containers, container) < 0) {
                        containers.push(container);
                                                                        if (/select/i.test(container.tagName)) {
                            optionsChanged = optionsChanged || [];
                            optionsChanged.push(container);
                        }
                    }
                }
            }
            lastPh = currentPh;
            result = null;
            if (itemTemplate) {
                result = itemTemplate.instantiateIn(container, data, item, i, currentPh, pctx);
                                                                args._ctx = result;
                this._contexts[i] = result;
                this._raiseItem("Rendered", args);
                                                if (currentPh) {
                    var count = currentPh.__msajaxphcount || 0;
                    if (count > -1) {
                                                                        currentPh.__msajaxphcount = count + result.nodes.length;
                    }
                }
            }
            else {
                this._contexts[i] = result;
                this._raiseItem("Rendered", args);
            }
            
        }
        
        if (optionsChanged) {
            for (i = 0; i < optionsChanged.length; i++) {
                Sys.Observer.raiseEvent(optionsChanged[i], "optionsChanged", Sys.EventArgs.Empty);
            }
        }

        if (!collectionChange) {
                                    if (!this._rendered && this.get_selectedIndex() > -1) {
                this._applySelectedIndex(this.get_selectedIndex(), true);
            }
            else {
                                this._resetSelectedIndex();
            }
        }
        this._rendered = true;

        var selectedClass = this.get_selectedItemClass();
        if (selectedClass) {
            var selectedIndex = this.get_selectedIndex();
            if (selectedIndex !== -1) {
                this._addRemoveCssClass(selectedIndex, selectedClass, Sys.UI.DomElement.addCssClass);
            }
        }
        this.raisePropertyChanged("viewData");
        this._raiseEvent("rendered", renderArgs);
        this._initializeResults();
    }
    function Sys$UI$DataView$_swapData(oldData, newData) {
                        if (oldData) {
            switch (this._eventType) {
                case 1:
                    oldData.remove_collectionChanged(this._changedHandler);
                    break;
                case 2:
                    Sys.Observer.removeCollectionChanged(oldData, this._changedHandler);
                    break;
            }
        }
                        this._eventType = 0;
        if (newData) {
            if (!this._changedHandler) {
                this._changedHandler = Function.createDelegate(this, this._collectionChanged);
            }
            if (typeof(newData.add_collectionChanged) === "function") {
                newData.add_collectionChanged(this._changedHandler);
                this._eventType = 1;
            }
            else if (newData instanceof Array) {
                Sys.Observer.addCollectionChanged(newData, this._changedHandler);
                this._eventType = 2;
            }
        }
    }
    function Sys$UI$DataView$_validateIndexInput(value) {
        var type = typeof(value);
        if (type === "string") {
            value = parseInt(value);
            if (isNaN(value)) {
                throw Error.argument(Sys.UI.TemplatesRes.invalidSelectedIndexValue);
            }
        }
        else if (type !== "number") {
            throw Error.argument(Sys.UI.TemplatesRes.invalidSelectedIndexValue);
        }
        return value;
    }
    function Sys$UI$DataView$dispose() {
        /// <summary locid="M:J#clearAndShow" />
                if (this._placeholders && !Sys.Application.get_isDisposing()) {
            this._clearContainers(this._placeholders);
        }
        if (this._dvTemplate) {
            this._dvTemplate.dispose();
        }
        if (this.get_isFetching()) {
            this.abortFetch();
            this._fetching = false;
        }
        this._swapData(this._data, null);
        this._currentData = this._lastData = this._placeholders = this._containers = this._placeholder =
        this._contexts = this._parentContext = this._dvTemplate = this._request = this._dataProvider =
        this._wsp = this._wspClass = this._provider = this._data = this._fetchParameters = this._query = null;
        Sys.UI.DataView.callBaseMethod(this, "dispose")
    }
    function Sys$UI$DataView$initialize() {
        /// <summary locid="M:J#clearAndShow" />
        Sys.UI.DataView.callBaseMethod(this, "initialize");
        this.refresh();
        this.updated();
    }
    function Sys$UI$DataView$fetchData(succeededCallback, failedCallback, mergeOption, userContext) {
        /// <summary locid="M:J#clearAndShow" />
        /// <param name="succeededCallback" type="Function" mayBeNull="true" optional="true"></param>
        /// <param name="failedCallback" type="Function" mayBeNull="true" optional="true"></param>
        /// <param name="mergeOption" type="Sys.Data.MergeOption" mayBeNull="true" optional="true"></param>
        /// <param name="userContext" mayBeNull="true" optional="true"></param>
        /// <returns type="Sys.Net.WebRequest"></returns>
        this._stale = false;
        var request, _this = this;
        function onSuccess(data) {
            _this._loadData(data);
            _this._fetching = false;
            _this._request = null;
            _this._raiseChanged("isFetching");
            _this._raiseSucceeded(request, data);
            if (succeededCallback) {
                succeededCallback(data, userContext, "fetchData");
            }
        }
        function onFail(error) {
            _this._fetching = false;
            _this._request = null;
            _this._raiseChanged("isFetching");
            _this._raiseFailed(request, error);
            if (failedCallback) {
                failedCallback(error, userContext, "fetchData");
            }
        }
        if (this._fetching) {
            this.abortFetch();
        }
        var dataProvider = this._dataProvider,
            wsp = this._wsp,
            wspc =  this._wspClass,
            query = this.get_fetchOperation(),
            parameters = this.get_fetchParameters() || null,
            httpVerb = this.get_httpVerb() || "POST",
            timeout = this.get_timeout() || 0;
        if (typeof(mergeOption) === "undefined") {
            mergeOption = null;
        }
        if (dataProvider) {
            request = dataProvider.fetchData(query, parameters, mergeOption, httpVerb, onSuccess, onFail, timeout, userContext);
        }
        else if (wsp) {
            var path = wsp.get_path();
            if (!path) {
                                var type = Object.getType(wsp);
                if (type && (typeof(type.get_path) === "function")) {
                    path = type.get_path();
                }
            }
            Type._checkDependency("MicrosoftAjaxWebServices.js", "Sys.UI.DataView.fetchData");
            request = Sys.UI.DataView._fetchWSP(null, path, query, parameters, httpVerb, onSuccess, onFail, timeout || wsp.get_timeout());
        }
        else {
            Type._checkDependency("MicrosoftAjaxWebServices.js", "Sys.UI.DataView.fetchData");
            if (wspc) {
                request = Sys.UI.DataView._fetchWSP(null, wspc.get_path(), query, parameters, httpVerb, onSuccess, 
                                                         onFail, timeout || wspc.get_timeout());
            }
            else {
                request = Sys.UI.DataView._fetchWSP(null, this._provider, query, parameters, httpVerb, onSuccess, onFail, timeout);
            }
        }
        this._request = request;
        this._fetching = true;
        this._raiseChanged("isFetching");
        return request;
    }
    function Sys$UI$DataView$onCommand(args) {
        /// <summary locid="M:J#Sys.UI.DataView.onCommand" />
        /// <param name="args" type="Sys.CommandEventArgs"></param>
    }
    function Sys$UI$DataView$onItemRendering(args) {
        /// <summary locid="M:J#Sys.UI.DataView.onItemRendering" />
        /// <param name="args" type="Sys.UI.DataViewItemEventArgs"></param>
    }
    function Sys$UI$DataView$onItemRendered(args) {
        /// <summary locid="M:J#Sys.UI.DataView.onItemRendered" />
        /// <param name="args" type="Sys.UI.DataViewItemEventArgs"></param>
    }
    function Sys$UI$DataView$updated() {
        /// <summary locid="M:J#Sys.UI.DataView.updated" />
        if (this._stale && this.get_autoFetch()) {
            this._doAutoFetch();
        }
        if (this._dirty) {
            this.refresh();
        }
        if (this._changed) {
            this.raisePropertyChanged("");
            this._changed = false;
        }
    }
Sys.UI.DataView.prototype = {
    _autoFetch: false,
    _fetching: false,
    _changed: false,
    _data: null,
    _dataProvider: null,
    _wsp: null,
    _wspClass: null,
    _dirty: false,
    _stale: true,
    _dvTemplate: null,
    _eventType: 0,
    _httpVerb: null,
    _initialSelectedIndex: -1,
    _fetchParameters: null,
    _parentContext: null,
    _placeholder: null,
    _query: null,
    _contexts: null,
    _selectedIndex: -1,
    _selectedItemClass: null,
    _template: null,
    _timeout: 0,
    _request: null,
    add_command: Sys$UI$DataView$add_command,
    remove_command: Sys$UI$DataView$remove_command,
    add_rendering: Sys$UI$DataView$add_rendering,
    remove_rendering: Sys$UI$DataView$remove_rendering,
    add_rendered: Sys$UI$DataView$add_rendered,
    remove_rendered: Sys$UI$DataView$remove_rendered,
    add_itemRendered: Sys$UI$DataView$add_itemRendered,
    remove_itemRendered: Sys$UI$DataView$remove_itemRendered,
    add_itemRendering: Sys$UI$DataView$add_itemRendering,
    remove_itemRendering: Sys$UI$DataView$remove_itemRendering,
    add_fetchFailed: Sys$UI$DataView$add_fetchFailed,
    remove_fetchFailed: Sys$UI$DataView$remove_fetchFailed,
    add_fetchSucceeded: Sys$UI$DataView$add_fetchSucceeded,
    remove_fetchSucceeded: Sys$UI$DataView$remove_fetchSucceeded,
    get_viewData: Sys$UI$DataView$get_viewData,
    get_data: Sys$UI$DataView$get_data,
    set_data: Sys$UI$DataView$set_data,
    get_dataProvider: Sys$UI$DataView$get_dataProvider,
    set_dataProvider: Sys$UI$DataView$set_dataProvider,
    get_autoFetch: Sys$UI$DataView$get_autoFetch,
    set_autoFetch: Sys$UI$DataView$set_autoFetch,
    get_isFetching: Sys$UI$DataView$get_isFetching,
    get_httpVerb: Sys$UI$DataView$get_httpVerb,
    set_httpVerb: Sys$UI$DataView$set_httpVerb,
    get_contexts: Sys$UI$DataView$get_contexts,
    get_fetchParameters: Sys$UI$DataView$get_fetchParameters,
    set_fetchParameters: Sys$UI$DataView$set_fetchParameters,
    get_selectedData: Sys$UI$DataView$get_selectedData,
    get_selectedIndex: Sys$UI$DataView$get_selectedIndex,
    set_selectedIndex: Sys$UI$DataView$set_selectedIndex,
    get_initialSelectedIndex: Sys$UI$DataView$get_initialSelectedIndex,
    set_initialSelectedIndex: Sys$UI$DataView$set_initialSelectedIndex,
    get_selectedItemClass: Sys$UI$DataView$get_selectedItemClass,
    set_selectedItemClass: Sys$UI$DataView$set_selectedItemClass,
    get_timeout: Sys$UI$DataView$get_timeout,
    set_timeout: Sys$UI$DataView$set_timeout,
    get_fetchOperation: Sys$UI$DataView$get_fetchOperation,
    set_fetchOperation: Sys$UI$DataView$set_fetchOperation,    
    get_itemPlaceholder: Sys$UI$DataView$get_itemPlaceholder,
    set_itemPlaceholder: Sys$UI$DataView$set_itemPlaceholder,
    get_templateContext: Sys$UI$DataView$get_templateContext,
    set_templateContext: Sys$UI$DataView$set_templateContext,    
    get_itemTemplate: Sys$UI$DataView$get_itemTemplate,
    set_itemTemplate: Sys$UI$DataView$set_itemTemplate,
    _applySelectedIndex: Sys$UI$DataView$_applySelectedIndex,
    _addRemoveCssClass: Sys$UI$DataView$_addRemoveCssClass,
    _collectionChanged: Sys$UI$DataView$_collectionChanged,
    _elementContains: Sys$UI$DataView$_elementContains,
    _raiseChanged: Sys$UI$DataView$_raiseChanged,
    _raiseFailed: Sys$UI$DataView$_raiseFailed,
    _raiseSelectedData: Sys$UI$DataView$_raiseSelectedData,
    _raiseSucceeded: Sys$UI$DataView$_raiseSucceeded,
    _ensureTemplate: Sys$UI$DataView$_ensureTemplate,
    _getTemplate: Sys$UI$DataView$_getTemplate,
    _loadData: Sys$UI$DataView$_loadData,
    _resetSelectedIndex: Sys$UI$DataView$_resetSelectedIndex,
    _initializeResults: Sys$UI$DataView$_initializeResults,    
    _isActive: Sys$UI$DataView$_isActive,
    _raiseEvent: Sys$UI$DataView$_raiseEvent,
    _raiseCommand: Sys$UI$DataView$_raiseCommand,
    _raiseItem: Sys$UI$DataView$_raiseItem,
    abortFetch: Sys$UI$DataView$abortFetch,    
    onBubbleEvent: Sys$UI$DataView$onBubbleEvent,
    onRendering: Sys$UI$DataView$onRendering,
    onFetchFailed: Sys$UI$DataView$onFetchFailed,
    onFetchSucceeded: Sys$UI$DataView$onFetchSucceeded,
    _doAutoFetch: Sys$UI$DataView$_doAutoFetch,
    _findContextIndex: Sys$UI$DataView$_findContextIndex,
    findContext: Sys$UI$DataView$findContext,
    _clearContainer: Sys$UI$DataView$_clearContainer,
    _clearContainers: Sys$UI$DataView$_clearContainers,
    _isAlone: Sys$UI$DataView$_isAlone,
    refresh: Sys$UI$DataView$refresh,
    _swapData: Sys$UI$DataView$_swapData,
    _validateIndexInput: Sys$UI$DataView$_validateIndexInput,
    dispose: Sys$UI$DataView$dispose, 
    initialize: Sys$UI$DataView$initialize,
    fetchData: Sys$UI$DataView$fetchData,
    onCommand: Sys$UI$DataView$onCommand,
    onItemRendering: Sys$UI$DataView$onItemRendering,
    onItemRendered: Sys$UI$DataView$onItemRendered,
    updated: Sys$UI$DataView$updated    
}
Sys.UI.DataView.registerClass("Sys.UI.DataView", Sys.UI.Control, Sys.UI.ITemplateContextConsumer);
Sys.UI.DataView._fetchWSP = function Sys$UI$DataView$_fetchWSP(dataContext, uri, operation, parameters, httpVerb, succeededCallback, failedCallback, timeout, context) {
    if (!uri) {
        throw Error.invalidOperation(Sys.UI.TemplatesRes.requiredUri);
    }
    return Sys.Net.WebServiceProxy.invoke(
        uri, operation,
        httpVerb === "GET", parameters, succeededCallback,
        failedCallback, context, timeout);
}
Sys.UI.DataViewItemEventArgs = function Sys$UI$DataViewItemEventArgs(dataItem, itemContext) {
    /// <summary locid="M:J#Sys.UI.DataViewItemEventArgs.#ctor" />
    /// <param name="dataItem"></param>
    /// <param name="itemContext" type="Sys.UI.TemplateContext" mayBeNull="true" optional="true"></param>
    Sys.UI.DataViewItemEventArgs.initializeBase(this);
    this._ctx = itemContext;
    this._data = dataItem || null;
}

    function Sys$UI$DataViewItemEventArgs$get_dataItem() {
        /// <value locid="P:J#Sys.UI.DataViewItemEventArgs.dataItem"></value>
        return this._data;
    }
    function Sys$UI$DataViewItemEventArgs$get_itemContext() {
        /// <value type="Sys.UI.TemplateContext" locid="P:J#Sys.UI.DataViewItemEventArgs.itemContext"></value>
        return this._ctx || null;
    }
    function Sys$UI$DataViewItemEventArgs$get_itemPlaceholder() {
        /// <value mayBeNull="true" locid="P:J#Sys.UI.DataViewItemEventArgs.itemPlaceholder"></value>
        return this._placeholder || null;
    }
    function Sys$UI$DataViewItemEventArgs$set_itemPlaceholder(value) {
        this._placeholder = value;
    }
    function Sys$UI$DataViewItemEventArgs$get_itemTemplate() {
        /// <value mayBeNull="true" locid="P:J#Sys.UI.DataViewItemEventArgs.itemTemplate"></value>
        return this._itemTemplate || null;
    }
    function Sys$UI$DataViewItemEventArgs$set_itemTemplate(value) {
        this._itemTemplate = value;
    }
Sys.UI.DataViewItemEventArgs.prototype = {
    get_dataItem: Sys$UI$DataViewItemEventArgs$get_dataItem,
    get_itemContext: Sys$UI$DataViewItemEventArgs$get_itemContext,
    get_itemPlaceholder: Sys$UI$DataViewItemEventArgs$get_itemPlaceholder,
    set_itemPlaceholder: Sys$UI$DataViewItemEventArgs$set_itemPlaceholder,
    get_itemTemplate: Sys$UI$DataViewItemEventArgs$get_itemTemplate,
    set_itemTemplate: Sys$UI$DataViewItemEventArgs$set_itemTemplate
}
Sys.UI.DataViewItemEventArgs.registerClass("Sys.UI.DataViewItemEventArgs", Sys.EventArgs);

Type.registerNamespace('Sys.UI');

Sys.UI.TemplatesRes={
'commonNotAfterInit':'{0} \'{1}\' cannot be set after initialize.',
'unresolvedReference':'Sys.Binding could not resolve the reference to an element or component with the ID or key \"{0}\".',
'stringOrBoolean':'Value must be the string \"true\", \"false\", or a Boolean.',
'elementNotFound':'An element with id \'{0}\' could not be found.',
'updateBeforeInit':'Update cannot be called before initialize.',
'invalidAttributeName':'Invalid attribute name \'{0}\'. Declared attribute names must be in lowercase.',
'invalidFunctionName':'\'{0}\' must be of type Function or the name of a function as a String.',
'invalidCommandTarget':'Command target is not a control id or not an expression that can be evaluated as a control reference.',
'invalidDataProviderType':'Value must be a service URI, an instance of Sys.Net.WebServiceProxy, or class that implements Sys.Data.IDataProvider.',
'requiredUri':'A serviceUri must be set prior to calling fetchData.',
'invalidAttach':'Invalid attribute \'{0}\', the type \'{1}\' is not a registered namespace.',
'cannotActivate':'Could not activate element with id \'{0}\', the element could not be found.',
'misplacedTemplate':'DataView item template must not be a child element of the DataView.',
'functionNotFound':'A function with the name \'{0}\' could not be found.',
'bindingPropertyNotSet':'Binding \'{0}\' must be set prior to initialize.',
'invalidSelectedIndexValue':'Value must be a Number or a String that can be converted to a Number.',
'mustSetInputElementsExplicitly':'Input elements \'type\' and \'name\' attributes must be explicitly set.',
'invalidTypeNamespace':'Invalid type namespace declaration, \'{0}\' is not a valid type.',
'cannotFindMarkupExtension':'A markup extension with the name \'{0}\' could not be found.'
};
