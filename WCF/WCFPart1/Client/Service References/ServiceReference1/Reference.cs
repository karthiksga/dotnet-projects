﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.thatindigogirl.com/2007/07", ConfigurationName="ServiceReference1.IHelloIndigoService")]
    public interface IHelloIndigoService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.thatindigogirl.com/2007/07/IHelloIndigoService/HelloIndigo", ReplyAction="http://www.thatindigogirl.com/2007/07/IHelloIndigoService/HelloIndigoResponse")]
        string HelloIndigo(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IHelloIndigoServiceChannel : Client.ServiceReference1.IHelloIndigoService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HelloIndigoServiceClient : System.ServiceModel.ClientBase<Client.ServiceReference1.IHelloIndigoService>, Client.ServiceReference1.IHelloIndigoService {
        
        public HelloIndigoServiceClient() {
        }
        
        public HelloIndigoServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HelloIndigoServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HelloIndigoServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HelloIndigoServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string HelloIndigo(string message) {
            return base.Channel.HelloIndigo(message);
        }
    }
}
