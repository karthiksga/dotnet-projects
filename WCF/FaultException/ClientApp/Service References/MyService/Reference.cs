﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientApp.MyService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MyFault", Namespace="http://schemas.datacontract.org/2004/07/Domain")]
    [System.SerializableAttribute()]
    public partial class MyFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyService.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GoodOperation", ReplyAction="http://tempuri.org/IService/GoodOperationResponse")]
        string GoodOperation();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ThrowException", ReplyAction="http://tempuri.org/IService/ThrowExceptionResponse")]
        void ThrowException();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/ThrowExceptionOneWay")]
        void ThrowExceptionOneWay();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SetCounter1", ReplyAction="http://tempuri.org/IService/SetCounter1Response")]
        [System.ServiceModel.FaultContractAttribute(typeof(ClientApp.MyService.MyFault), Action="http://tempuri.org/IService/SetCounter1MyFaultFault", Name="MyFault", Namespace="http://schemas.datacontract.org/2004/07/Domain")]
        void SetCounter1(int i);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SetCounter2", ReplyAction="http://tempuri.org/IService/SetCounter2Response")]
        [System.ServiceModel.FaultContractAttribute(typeof(ClientApp.MyService.MyFault), Action="http://tempuri.org/IService/SetCounter2MyFaultFault", Name="MyFault", Namespace="http://schemas.datacontract.org/2004/07/Domain")]
        void SetCounter2(int i);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : ClientApp.MyService.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<ClientApp.MyService.IService>, ClientApp.MyService.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GoodOperation() {
            return base.Channel.GoodOperation();
        }
        
        public void ThrowException() {
            base.Channel.ThrowException();
        }
        
        public void ThrowExceptionOneWay() {
            base.Channel.ThrowExceptionOneWay();
        }
        
        public void SetCounter1(int i) {
            base.Channel.SetCounter1(i);
        }
        
        public void SetCounter2(int i) {
            base.Channel.SetCounter2(i);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyService.IWSService")]
    public interface IWSService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWSService/GoodOperationWS", ReplyAction="http://tempuri.org/IWSService/GoodOperationWSResponse")]
        string GoodOperationWS();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWSService/ThrowExceptionWS", ReplyAction="http://tempuri.org/IWSService/ThrowExceptionWSResponse")]
        void ThrowExceptionWS();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWSServiceChannel : ClientApp.MyService.IWSService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WSServiceClient : System.ServiceModel.ClientBase<ClientApp.MyService.IWSService>, ClientApp.MyService.IWSService {
        
        public WSServiceClient() {
        }
        
        public WSServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WSServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GoodOperationWS() {
            return base.Channel.GoodOperationWS();
        }
        
        public void ThrowExceptionWS() {
            base.Channel.ThrowExceptionWS();
        }
    }
}
