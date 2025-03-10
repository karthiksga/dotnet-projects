﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MsmqApp.MsmqServ {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MsmqServ.IOrderProcessor")]
    public interface IOrderProcessor {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IOrderProcessor/SubmitOrder")]
        void SubmitOrder(int order);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderProcessorChannel : MsmqApp.MsmqServ.IOrderProcessor, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderProcessorClient : System.ServiceModel.ClientBase<MsmqApp.MsmqServ.IOrderProcessor>, MsmqApp.MsmqServ.IOrderProcessor {
        
        public OrderProcessorClient() {
        }
        
        public OrderProcessorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderProcessorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderProcessorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderProcessorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SubmitOrder(int order) {
            base.Channel.SubmitOrder(order);
        }
    }
}
