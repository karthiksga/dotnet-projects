using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Domain;

namespace ServiceLib
{
    public class MyErrorHandler:IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            if (error.GetType() == typeof(InvalidOperationException))
            {
                FaultException<MyFault> ex = new FaultException<MyFault>(new MyFault { Description="this operation is invalid"}, "Invalid operation");
                MessageFault msg = ex.CreateMessageFault();
                fault = Message.CreateMessage(version, msg, ex.Action);
            }
            else if (error.GetType() == typeof(NotImplementedException))
            {
                FaultException<MyFault> ex = new FaultException<MyFault>(new MyFault { Description="this operation is not implemented"}, "Not implemented");
                MessageFault msg = ex.CreateMessageFault();
                fault = Message.CreateMessage(version, msg, ex.Action);
            }

            //if (error.GetType() == typeof(InvalidOperationException))
            //{
            //    FaultException<InvalidOperationException> ex = new FaultException<InvalidOperationException>(error as InvalidOperationException, error.Message, FaultCode.CreateSenderFaultCode(null));
            //    MessageFault msg = ex.CreateMessageFault();
            //    fault = Message.CreateMessage(version, ex.Action, msg);
            //}
        }
    }


}
