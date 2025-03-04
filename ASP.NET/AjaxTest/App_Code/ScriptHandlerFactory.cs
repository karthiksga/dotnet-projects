﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
namespace CustomComponents
{

    /// <summary>
    /// Summary description for ScriptHandlerFactory
    /// </summary>
    public class ScriptHandlerFactory : IHttpHandlerFactory
    {        
        private IHttpHandlerFactory _restHandlerFactory;
        private IHttpHandlerFactory _webServiceHandlerFactory;
        public ScriptHandlerFactory()
        {
            this._restHandlerFactory = new RestHandlerFactory();
            this._webServiceHandlerFactory = new WebServiceHandlerFactory();
        }
        public virtual IHttpHandler GetHandler(HttpContext context, string requestType,
        string url, string pathTranslated)
        {
            IHttpHandlerFactory handlerFactory;
            if (RestHandlerFactory.IsRestRequest(context))
                handlerFactory = this._restHandlerFactory;
            else
                handlerFactory = this._webServiceHandlerFactory;
            IHttpHandler handler = handlerFactory.GetHandler(context, requestType,
            url, pathTranslated);
            return new HandlerWrapper(handler, handlerFactory);
        }
        public virtual void ReleaseHandler(IHttpHandler handler)
        {
            ((HandlerWrapper)handler).ReleaseHandler();
        }
    }
}