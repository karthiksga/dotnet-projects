using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Reflection;
using System.Web.Compilation;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Collections;
using System.Web.Services.Protocols;
using System.IO;
namespace CustomComponents
{
    internal class HandlerWrapper : IHttpHandler
    {
        private IHttpHandlerFactory _handlerFactory;
        protected IHttpHandler _handler;
        internal HandlerWrapper(IHttpHandler handler,
        IHttpHandlerFactory handlerFactory)
        {
            this._handlerFactory = handlerFactory;
            this._handler = handler;
        }
        public void ProcessRequest(HttpContext context)
        {
            this._handler.ProcessRequest(context);
        }
        internal void ReleaseHandler()
        {
            this._handlerFactory.ReleaseHandler(this._handler);
        }
        public bool IsReusable
        {
            get
            {
                return this._handler.IsReusable;
            }
        }
    }
}